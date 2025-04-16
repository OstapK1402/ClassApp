using School.BLL.DTO;

namespace School.BLL.Interfaces
{
    public interface IClassSubjectService
    {
        Task<IEnumerable<ClassSubjectDTO>> GetAll(CancellationToken token);
        Task<ClassSubjectDTO> GetById(int classId, int subjectId, CancellationToken token);
        Task<bool> Create(ClassSubjectDTO modelDTO, CancellationToken token);
        Task<bool> Update(ClassSubjectDTO modelDTO, CancellationToken token);
        Task<bool> Delete(int classId, int subjectId, CancellationToken token);
        Task<IEnumerable<ClassSubjectDTO>> GetAllByTeacherId(int teacherId, CancellationToken token);
        Task<IEnumerable<ClassSubjectDTO>> GetAllByClassId(int classId, CancellationToken token);
    }
}
