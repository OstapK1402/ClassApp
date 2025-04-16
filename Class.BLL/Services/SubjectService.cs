using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(SubjectDTO modelDTO, CancellationToken token)
        {
            modelDTO.Id = 0;
            var subject = _mapper.Map<Subject>(modelDTO);

            await _unitOfWork.SubjectRepository.CreateAsync(subject, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id, token);

            if (subject == null)
            {
                throw new KeyNotFoundException("Subject not found!");
            }

            _unitOfWork.SubjectRepository.Delete(subject);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<SubjectDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<SubjectDTO>>(await _unitOfWork.SubjectRepository.GetAllAsync(token));
        }

        public async Task<SubjectDTO> GetById(int id, CancellationToken token)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id, token);

            if (subject == null)
            {
                throw new KeyNotFoundException("Subject not found!");
            }

            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task<bool> SubjectExist(int id, CancellationToken token)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(id, token);
            return subject != null;
        }

        public async Task<bool> Update(SubjectDTO modelDTO, CancellationToken token)
        {
            var subject = await _unitOfWork.SubjectRepository.GetByIdAsync(modelDTO.Id, token);

            if (subject == null)
            {
                throw new KeyNotFoundException("Subject not found!");
            }

            subject = _mapper.Map<Subject>(modelDTO);

            _unitOfWork.SubjectRepository.Update(subject);

            return await _unitOfWork.SaveAsync() > 0;
        }
        public async Task<List<SelectListItem>> GetSelectItem(CancellationToken token)
        {
            var groups = _mapper.Map<IEnumerable<SubjectDTO>>(await _unitOfWork.SubjectRepository.GetAllAsync(token));

            return groups.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }
    }
}
