using AutoMapper;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GradeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(GradeDTO modelDTO, CancellationToken token)
        {
            modelDTO.Id = 0;
            var grade = _mapper.Map<Grade>(modelDTO);

            await _unitOfWork.GradeRepository.CreateAsync(grade, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var grade = await _unitOfWork.GradeRepository.GetByIdAsync(id, token);

            if (grade == null)
            {
                throw new KeyNotFoundException("Grade not found!");
            }

            _unitOfWork.GradeRepository.Delete(grade);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<GradeDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<GradeDTO>>(await _unitOfWork.GradeRepository.GetAllAsync(token));
        }

        public async Task<GradeDTO> GetById(int id, CancellationToken token)
        {
            var grade = await _unitOfWork.GradeRepository.GetByIdAsync(id, token);

            if (grade == null)
            {
                throw new KeyNotFoundException("Grade not found!");
            }

            return _mapper.Map<GradeDTO>(grade);
        }

        public async Task<bool> Update(GradeDTO modelDTO, CancellationToken token)
        {
            var grade = await _unitOfWork.GradeRepository.GetByIdAsync(modelDTO.Id, token);

            if (grade == null)
            {
                throw new KeyNotFoundException("Grade not found!");
            }

            grade = _mapper.Map<Grade>(modelDTO);

            _unitOfWork.GradeRepository.Update(grade);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<GradeDTO?> GetBySubmission(int submissionId, CancellationToken token)
        {
            var grades = _mapper.Map<IEnumerable<GradeDTO>>(await _unitOfWork.GradeRepository.GetAllAsync(token));
            return grades.FirstOrDefault(x => x.HomeworkSubmissionId == submissionId);
        }
    }
}
