using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IHomeworkSubmissionsRepository
    {
        Task<IEnumerable<HomeworkSubmission>> GetAllAsync(CancellationToken token);
        Task<HomeworkSubmission?> GetByIdAsync(int id, CancellationToken token);
        Task CreateAsync(HomeworkSubmission model, CancellationToken token);
        void Update(HomeworkSubmission model);
        void Delete(HomeworkSubmission model);
        Task<HomeworkSubmission?> GetByHomeworkAndStudentAsync(int homeworkId, int studentId, CancellationToken token);
    }
}
