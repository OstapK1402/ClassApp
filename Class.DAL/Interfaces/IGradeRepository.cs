using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetAllAsync(CancellationToken token);
        Task<Grade?> GetByIdAsync(int id, CancellationToken token);
        Task CreateAsync(Grade model, CancellationToken token);
        void Update(Grade model);
        void Delete(Grade model);
    }
}
