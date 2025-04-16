namespace School.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClassRepository ClassRepository { get; }
        IClassSubjectRepository ClassSubjectRepository { get; }
        IGradeRepository GradeRepository { get; }
        IHomeworkRepository HomeworkRepository { get; }
        IHomeworkSubmissionsRepository HomeworkSubmissionsRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IUserRepository UserRepository { get; }

        void Dispose();
        Task<int> SaveAsync();
    }
}
