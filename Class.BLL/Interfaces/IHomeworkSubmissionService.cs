using School.BLL.DTO;

namespace School.BLL.Interfaces
{
    public interface IHomeworkSubmissionService
    {
        Task<IEnumerable<HomeworkSubmissionDTO>> GetAll(CancellationToken token);
        Task<HomeworkSubmissionDTO> GetById(int id, CancellationToken token);
        Task<bool> Create(HomeworkSubmissionDTO modelDTO, CancellationToken token);
        Task<bool> Update(HomeworkSubmissionDTO modelDTO, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
        Task<HomeworkSubmissionDTO?> GetByHomeworkAndStudent(int homeworkId, int studentId, CancellationToken token);
        Task<IEnumerable<HomeworkSubmissionDTO>> GetByHomework(int homeworkId, CancellationToken token);
    }
}
