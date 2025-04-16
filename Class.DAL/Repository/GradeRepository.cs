using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class GradeRepository : IGradeRepository
    {
        private readonly DbSet<Grade> _dbSet;
        public GradeRepository(SchoolDbContext dbContext)
        {
            _dbSet = dbContext.Set<Grade>();
        }

        public async Task CreateAsync(Grade model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(Grade model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<Grade>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<Grade?> GetByIdAsync(int id, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Update(Grade model)
        {
            _dbSet.Update(model);
        }
    }
}
