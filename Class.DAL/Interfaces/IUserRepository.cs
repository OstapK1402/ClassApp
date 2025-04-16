using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(CancellationToken token);
        Task<User?> GetByIdAsync(int id, CancellationToken token);
        Task<User?> GetByEmailAsync(string email, CancellationToken token);
        Task CreateAsync(User model, CancellationToken token);
        void Update(User model);
        void Delete(User model);
    }
}
