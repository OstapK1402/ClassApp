using AutoMapper;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class HomeworkSubmissionService : IHomeworkSubmissionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public HomeworkSubmissionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(HomeworkSubmissionDTO modelDTO, CancellationToken token)
        {
            modelDTO.Id = 0;
            var homework = _mapper.Map<HomeworkSubmission>(modelDTO);

            await _unitOfWork.HomeworkSubmissionsRepository.CreateAsync(homework, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var homework = await _unitOfWork.HomeworkSubmissionsRepository.GetByIdAsync(id, token);

            if (homework == null)
            {
                throw new KeyNotFoundException("Homework Submission not found!");
            }

            _unitOfWork.HomeworkSubmissionsRepository.Delete(homework);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<HomeworkSubmissionDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<HomeworkSubmissionDTO>>(await _unitOfWork.HomeworkSubmissionsRepository.GetAllAsync(token));
        }

        public async Task<HomeworkSubmissionDTO> GetById(int id, CancellationToken token)
        {
            var homework = await _unitOfWork.HomeworkSubmissionsRepository.GetByIdAsync(id, token);

            if (homework == null)
            {
                throw new KeyNotFoundException("Homework Submission not found!");
            }

            return _mapper.Map<HomeworkSubmissionDTO>(homework);
        }

        public async Task<bool> Update(HomeworkSubmissionDTO modelDTO, CancellationToken token)
        {
            var homework = await _unitOfWork.HomeworkSubmissionsRepository.GetByIdAsync(modelDTO.Id, token);

            if (homework == null)
            {
                throw new KeyNotFoundException("Homework Submission not found!");
            }

            homework = _mapper.Map<HomeworkSubmission>(modelDTO);

            _unitOfWork.HomeworkSubmissionsRepository.Update(homework);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<HomeworkSubmissionDTO?> GetByHomeworkAndStudent(int homeworkId, int studentId, CancellationToken token)
        {
            return _mapper.Map<HomeworkSubmissionDTO>(await _unitOfWork.HomeworkSubmissionsRepository.GetByHomeworkAndStudentAsync(homeworkId, studentId, token));
        }

        public async Task<IEnumerable<HomeworkSubmissionDTO>> GetByHomework(int homeworkId, CancellationToken token)
        {
            return _mapper.Map<IEnumerable<HomeworkSubmissionDTO>>((await _unitOfWork.HomeworkSubmissionsRepository.GetAllAsync(token))
                .Where(x => x.HomeworkId == homeworkId));
        }
    }
}
