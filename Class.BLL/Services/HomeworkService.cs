using AutoMapper;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class HomeworkService : IHomeworkService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public HomeworkService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(HomeworkDTO modelDTO, CancellationToken token)
        {
            modelDTO.Id = 0;
            var homework = _mapper.Map<Homework>(modelDTO);

            await _unitOfWork.HomeworkRepository.CreateAsync(homework, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var homework = await _unitOfWork.HomeworkRepository.GetByIdAsync(id, token);

            if (homework == null)
            {
                throw new KeyNotFoundException("Homework not found!");
            }

            _unitOfWork.HomeworkRepository.Delete(homework);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<HomeworkDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<HomeworkDTO>>(await _unitOfWork.HomeworkRepository.GetAllAsync(token));
        }

        public async Task<HomeworkDTO> GetById(int id, CancellationToken token)
        {
            var homework = await _unitOfWork.HomeworkRepository.GetByIdAsync(id, token);

            if (homework == null)
            {
                throw new KeyNotFoundException("Homework not found!");
            }

            return _mapper.Map<HomeworkDTO>(homework);
        }

        public async Task<bool> Update(HomeworkDTO modelDTO, CancellationToken token)
        {
            var homework = await _unitOfWork.HomeworkRepository.GetByIdAsync(modelDTO.Id, token);

            if (homework == null)
            {
                throw new KeyNotFoundException("Homework not found!");
            }

            _mapper.Map(modelDTO, homework);

            _unitOfWork.HomeworkRepository.Update(homework);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<HomeworkDTO>> GetByClassSubject(int classId, int subjectId, CancellationToken token)
        {
            return _mapper.Map<IEnumerable<HomeworkDTO>>(await _unitOfWork.HomeworkRepository.GetByClassSubjectAsync(classId, subjectId, token));
        }
    }
}
