using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.DTO;

namespace School.BLL.Interfaces
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDTO>> GetAll(CancellationToken token);
        Task<ClassDTO> GetById(int id, CancellationToken token);
        Task<bool> Create(ClassDTO modelDTO, CancellationToken token);
        Task<bool> Update(ClassDTO modelDTO, CancellationToken token);
        Task<bool> ClassExist(int id, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
        Task<ClassDTO> GetByIdWithoutInclude(int id, CancellationToken token);
        Task<List<SelectListItem>> GetSelectItem(CancellationToken token);
        Task<bool> RemoveStudentFromClass(int classId, int studentId, CancellationToken token);
        Task<ClassDTO> GetByName(string name, CancellationToken token);
    }
}
