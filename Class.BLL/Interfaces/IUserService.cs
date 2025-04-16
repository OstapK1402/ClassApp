using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.DTO;
using System.Security.Claims;

namespace School.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAll(CancellationToken token);
        Task<UserDTO> GetById(int id, CancellationToken token);
        Task<UserDTO> GetByEmail(string email, CancellationToken token);
        Task<bool> Create(UserDTO modelDTO, CancellationToken token);
        Task<bool> Update(UserDTO modelDTO, CancellationToken token);
        Task<bool> UserEmailCheck(string email, CancellationToken token);
        Task<bool> UserExist(int id, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
        Task<UserDTO> GetUserByUser(ClaimsPrincipal User);
        Task<List<SelectListItem>> GetTechersSelectItem(CancellationToken token);
        Task<List<SelectListItem>> GetStudentsSelectItem(CancellationToken token);
        Task<IEnumerable<UserDTO>> GetUsersByClass(int classId, CancellationToken token);
    }
}
