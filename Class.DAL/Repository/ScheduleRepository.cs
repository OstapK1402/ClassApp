using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class ScheduleRepository : IScheduleRepository
    {
        private readonly DbSet<Schedule> _dbSet;
        public ScheduleRepository(SchoolDbContext dbContext)
        {
            _dbSet = dbContext.Set<Schedule>();
        }

        public async Task CreateAsync(Schedule model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(Schedule model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<Schedule>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Teacher)
                .Include(x => x.Subject)
                .ToListAsync(token);
        }

        public async Task<Schedule?> GetByIdAsync(int id, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Update(Schedule model)
        {
            _dbSet.Update(model);
        }
    }
}
