using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class ClassService : IClassService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork; 
        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(ClassDTO modelDTO, CancellationToken token)
        {
            modelDTO.Id = 0;
            var classe = _mapper.Map<Class>(modelDTO);

            await _unitOfWork.ClassRepository.CreateAsync(classe, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var classe = await _unitOfWork.ClassRepository.GetByIdAsync(id, token);
            
            if(classe == null)
            {
                throw new KeyNotFoundException("Class not found!");
            }

            _unitOfWork.ClassRepository.Delete(classe);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<ClassDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<ClassDTO>>(await _unitOfWork.ClassRepository.GetAllAsync(token));
        }

        public async Task<ClassDTO> GetById(int id, CancellationToken token)
        {
            var classe = await _unitOfWork.ClassRepository.GetByIdAsync(id, token);
            
            if(classe == null)
            {
                throw new KeyNotFoundException("Class not found!");
            }

            return _mapper.Map<ClassDTO>(classe);
        }

        public async Task<bool> ClassExist(int id, CancellationToken token)
        {
            var classe = await _unitOfWork.ClassRepository.GetByIdAsync(id, token);
            return classe != null;
        }

        public async Task<bool> Update(ClassDTO modelDTO, CancellationToken token)
        {
            var classe = await _unitOfWork.ClassRepository.GetByIdAsyncWithoutInclude(modelDTO.Id, token);

            if (classe == null)
            {
                throw new KeyNotFoundException("Class not found!");
            }

            _mapper.Map(modelDTO, classe);

            _unitOfWork.ClassRepository.Update(classe);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<ClassDTO> GetByIdWithoutInclude(int id, CancellationToken token)
        {
            var classe = await _unitOfWork.ClassRepository.GetByIdAsyncWithoutInclude(id, token);

            if (classe == null)
            {
                throw new KeyNotFoundException("Class not found!");
            }

            return _mapper.Map<ClassDTO>(classe);
        }

        public async Task<List<SelectListItem>> GetSelectItem(CancellationToken token)
        {
            var groups = _mapper.Map<IEnumerable<ClassDTO>>(await _unitOfWork.ClassRepository.GetAllAsync(token));

            return groups.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
        }

        public async Task<bool> RemoveStudentFromClass(int classId, int studentId, CancellationToken token)
        {
            var student = await _unitOfWork.UserRepository.GetByIdAsync(studentId, token);

            if (student == null || student.ClassId != classId)
            {
                return false;
            }

            student.ClassId = null;
            _unitOfWork.UserRepository.Update(student);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<ClassDTO> GetByName(string name, CancellationToken token)
        {
            var classe = (await _unitOfWork.ClassRepository.GetAllAsync(token)).FirstOrDefault(x => x.Name == name);

            if (classe == null)
            {
                throw new KeyNotFoundException("Class not found!");
            }

            return _mapper.Map<ClassDTO>(classe);
        }
    }
}
