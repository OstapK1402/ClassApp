using School.DAL.Context;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _dbContext;
        public IClassRepository ClassRepository { get; private set; }

        public IClassSubjectRepository ClassSubjectRepository { get; private set; }

        public IGradeRepository GradeRepository { get; private set; }

        public IHomeworkRepository HomeworkRepository { get; private set; }

        public IHomeworkSubmissionsRepository HomeworkSubmissionsRepository { get; private set; }

        public IScheduleRepository ScheduleRepository { get; private set; }

        public ISubjectRepository SubjectRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(SchoolDbContext dbContext, IClassRepository classRepository, IClassSubjectRepository classSubjectRepository,
            IGradeRepository gradeRepository, IHomeworkRepository homeworkRepository, IHomeworkSubmissionsRepository homeworkSubmissionsRepository,
            IScheduleRepository scheduleRepository, ISubjectRepository subjectRepository, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            ClassRepository = classRepository;
            ClassSubjectRepository = classSubjectRepository;
            GradeRepository = gradeRepository;
            HomeworkRepository = homeworkRepository;
            HomeworkSubmissionsRepository = homeworkSubmissionsRepository;
            ScheduleRepository = scheduleRepository;
            SubjectRepository = subjectRepository;
            UserRepository = userRepository;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync(); 
        }
    }
}
