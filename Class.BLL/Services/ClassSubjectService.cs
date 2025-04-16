using AutoMapper;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ClassSubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(ClassSubjectDTO modelDTO, CancellationToken token)
        {
            var lesson = _mapper.Map<ClassSubject>(modelDTO);

            await _unitOfWork.ClassSubjectRepository.CreateAsync(lesson, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int classId, int subjectId, CancellationToken token)
        {
            var lesson = await _unitOfWork.ClassSubjectRepository.GetByIdAsync(classId, subjectId, token);

            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson not found!");
            }

            _unitOfWork.ClassSubjectRepository.Delete(lesson);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<ClassSubjectDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<ClassSubjectDTO>>(await _unitOfWork.ClassSubjectRepository.GetAllAsync(token));
        }

        public async Task<ClassSubjectDTO> GetById(int classId, int subjectId, CancellationToken token)
        {
            var lesson = await _unitOfWork.ClassSubjectRepository.GetByIdAsync(classId, subjectId, token);

            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson not found!");
            }

            return _mapper.Map<ClassSubjectDTO>(lesson);
        }

        public async Task<bool> Update(ClassSubjectDTO modelDTO, CancellationToken token)
        {
            var lesson = await _unitOfWork.ClassSubjectRepository.GetByIdAsyncWithoutInclude(modelDTO.ClassId, modelDTO.SubjectId,token);

            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson not found!");
            }

            lesson.TeacherId = modelDTO.TeacherId;

            _unitOfWork.ClassSubjectRepository.Update(lesson);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<ClassSubjectDTO>> GetAllByTeacherId(int teacherId, CancellationToken token)
        {
            return _mapper.Map<IEnumerable<ClassSubjectDTO>>(await _unitOfWork.ClassSubjectRepository.GetAllByTeacherIdAsync(teacherId, token));
        }

        public async Task<IEnumerable<ClassSubjectDTO>> GetAllByClassId(int classId, CancellationToken token)
        {
            return _mapper.Map<IEnumerable<ClassSubjectDTO>>(await _unitOfWork.ClassSubjectRepository.GetAllByClassId(classId, token));
        }
    }
}
