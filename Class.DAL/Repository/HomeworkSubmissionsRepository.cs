using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class HomeworkSubmissionsRepository : IHomeworkSubmissionsRepository
    {
        private readonly DbSet<HomeworkSubmission> _dbSet;
        public HomeworkSubmissionsRepository(SchoolDbContext dbContext)
        {
            _dbSet = dbContext.Set<HomeworkSubmission>();
        }

        public async Task CreateAsync(HomeworkSubmission model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(HomeworkSubmission model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<HomeworkSubmission>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<HomeworkSubmission?> GetByHomeworkAndStudentAsync(int homeworkId, int studentId, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.HomeworkId == homeworkId && x.StudentId == studentId, token);
        }

        public async Task<HomeworkSubmission?> GetByIdAsync(int id, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.Student)
                .FirstOrDefaultAsync(x => x.Id == id, token);
        }

        public void Update(HomeworkSubmission model)
        {
            _dbSet.Update(model);
        }
    }
}
