using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.DTO;

namespace School.BLL.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDTO>> GetAll(CancellationToken token);
        Task<SubjectDTO> GetById(int id, CancellationToken token);
        Task<bool> Create(SubjectDTO modelDTO, CancellationToken token);
        Task<bool> Update(SubjectDTO modelDTO, CancellationToken token);
        Task<bool> SubjectExist(int id, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
        Task<List<SelectListItem>> GetSelectItem(CancellationToken token);
    }
}
