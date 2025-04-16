using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class ClassRepository : IClassRepository
    {
        private readonly SchoolDbContext _dbContext;
        private readonly DbSet<Class> _dbSet;

        public ClassRepository(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Class>();
        }
        public async Task CreateAsync(Class model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(Class model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<Class>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Teacher)
                .ToListAsync(token);
        }

        public async Task<Class?> GetByIdAsync(int id, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Lessons)
                    .ThenInclude(x => x.Subject)
                .Include(x => x.Lessons)
                    .ThenInclude(x => x.Teacher)
                .Include(x => x.Teacher)
                .Include(x => x.Students)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Update(Class model)
        {
            _dbSet.Update(model);
        }

        public async Task<Class?> GetByIdAsyncWithoutInclude(int id, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }
    }
}
