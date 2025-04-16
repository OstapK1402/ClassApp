using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class SubjectRepository : ISubjectRepository
    {
        private readonly DbSet<Subject> _dbSet;
        public SubjectRepository(SchoolDbContext dbContext)
        {
            _dbSet = dbContext.Set<Subject>();
        }

        public async Task CreateAsync(Subject model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(Subject model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<Subject>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<Subject?> GetByIdAsync(int id, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Update(Subject model)
        {
            _dbSet.Update(model);
        }
    }
}
