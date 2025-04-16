using School.BLL.DTO;

namespace School.BLL.Interfaces
{
    public interface IHomeworkService
    {
        Task<IEnumerable<HomeworkDTO>> GetAll(CancellationToken token);
        Task<HomeworkDTO> GetById(int id, CancellationToken token);
        Task<bool> Create(HomeworkDTO modelDTO, CancellationToken token);
        Task<bool> Update(HomeworkDTO modelDTO, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
        Task<IEnumerable<HomeworkDTO>> GetByClassSubject(int classId, int subjectId, CancellationToken token);
    }
}
