using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IClassSubjectRepository
    {
        Task<IEnumerable<ClassSubject>> GetAllAsync(CancellationToken token);
        Task<ClassSubject?> GetByIdAsync(int classId, int subjectId, CancellationToken token);
        Task CreateAsync(ClassSubject model, CancellationToken token);
        void Update(ClassSubject model);
        void Delete(ClassSubject model);
        Task<ClassSubject?> GetByIdAsyncWithoutInclude(int classId, int subjectId, CancellationToken token);
        Task<IEnumerable<ClassSubject?>> GetAllByTeacherIdAsync(int teacherId, CancellationToken token);
        Task<IEnumerable<ClassSubject?>> GetAllByClassId(int classId, CancellationToken token);
    }
}
