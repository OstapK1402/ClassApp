using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _dbSet;
        public UserRepository(SchoolDbContext dbContext)
        {
            _dbSet = dbContext.Set<User>();
        }

        public async Task CreateAsync(User model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(User model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, token);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken token)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Update(User model)
        {
            _dbSet.Update(model);
        }
    }
}
