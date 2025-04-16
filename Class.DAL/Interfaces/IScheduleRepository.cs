using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAllAsync(CancellationToken token);
        Task<Schedule?> GetByIdAsync(int id, CancellationToken token);
        Task CreateAsync(Schedule model, CancellationToken token);
        void Update(Schedule model);
        void Delete(Schedule model);
    }
}
