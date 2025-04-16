using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync(CancellationToken token);
        Task<Subject?> GetByIdAsync(int id, CancellationToken token);
        Task CreateAsync(Subject model, CancellationToken token);
        void Update(Subject model);
        void Delete(Subject model);
    }
}
