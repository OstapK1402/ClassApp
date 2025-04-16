using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IHomeworkRepository
    {
        Task<IEnumerable<Homework>> GetAllAsync(CancellationToken token);
        Task<Homework?> GetByIdAsync(int id, CancellationToken token);
        Task CreateAsync(Homework model, CancellationToken token);
        void Update(Homework model);
        void Delete(Homework model);
        Task<IEnumerable<Homework>> GetByClassSubjectAsync(int classId, int subjectId, CancellationToken token);
    }
}
