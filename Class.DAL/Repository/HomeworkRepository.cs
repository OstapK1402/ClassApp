using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class HomeworkRepository : IHomeworkRepository
    {
        private readonly DbSet<Homework> _dbSet;
        public HomeworkRepository(SchoolDbContext dbContext)
        {
            _dbSet = dbContext.Set<Homework>();
        }

        public async Task CreateAsync(Homework model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(Homework model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<Homework>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<Homework?> GetByIdAsync(int id, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .Include(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Update(Homework model)
        {
            _dbSet.Update(model);
        }

        public async Task<IEnumerable<Homework>> GetByClassSubjectAsync(int classId, int subjectId, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.ClassId == classId && x.SubjectId == subjectId)
                //.Include(x => x.Class)
                //.Include(x => x.Subject)
                .ToListAsync(token);
        }
    }
}
