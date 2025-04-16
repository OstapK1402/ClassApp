using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllAsync(CancellationToken token);
        Task<Class?> GetByIdAsync(int id, CancellationToken token);
        Task CreateAsync(Class model, CancellationToken token);
        void Update(Class nodel);
        void Delete(Class model);
        Task<Class?> GetByIdAsyncWithoutInclude(int id, CancellationToken token);
    }
}
