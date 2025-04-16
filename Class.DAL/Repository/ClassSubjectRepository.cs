using Microsoft.EntityFrameworkCore;
using School.DAL.Context;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class ClassSubjectRepository : IClassSubjectRepository
    {
        private readonly DbSet<ClassSubject> _dbSet;

        public ClassSubjectRepository(SchoolDbContext dbContext)
        {
            _dbSet = dbContext.Set<ClassSubject>();
        }

        public async Task CreateAsync(ClassSubject model, CancellationToken token)
        {
            await _dbSet.AddAsync(model, token);
        }

        public void Delete(ClassSubject model)
        {
            _dbSet.Remove(model);
        }

        public async Task<IEnumerable<ClassSubject>> GetAllAsync(CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(token);
        }

        public async Task<ClassSubject?> GetByIdAsync(int classId, int subjectId, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.ClassId == classId && x.SubjectId == subjectId)
                .Include(x => x.Teacher)
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .FirstOrDefaultAsync(token);
        }

        public void Update(ClassSubject model)
        {
            _dbSet.Update(model);
        }

        public async Task<ClassSubject?> GetByIdAsyncWithoutInclude(int classId, int subjectId, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
               .Where(x => x.ClassId == classId && x.SubjectId == subjectId)
               .FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<ClassSubject?>> GetAllByTeacherIdAsync(int teacherId, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.TeacherId == teacherId)
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .ToListAsync(token);
        }

        public async Task<IEnumerable<ClassSubject?>> GetAllByClassId(int classId, CancellationToken token)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => x.ClassId == classId)
                .Include(x => x.Class)
                .Include(x => x.Subject)
                .ToListAsync(token);
        }
    }
}
