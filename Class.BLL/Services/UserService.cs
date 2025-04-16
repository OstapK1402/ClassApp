using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;
using System.Security.Claims;

namespace School.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> Create(UserDTO modelDTO, CancellationToken token)
        {
            modelDTO.Id = 0;

            var user = _mapper.Map<User>(modelDTO);

            await _unitOfWork.UserRepository.CreateAsync(user, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, token);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            _unitOfWork.UserRepository.Delete(user);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<UserDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<UserDTO>>(await _unitOfWork.UserRepository.GetAllAsync(token));
        }

        public async Task<UserDTO> GetById(int id, CancellationToken token)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, token);

            if (user == null)
            {
                throw new KeyNotFoundException("USer not found!");
            }

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetByEmail(string email, CancellationToken token)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, token);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> UserExist(int id, CancellationToken token)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, token);

            return user != null;
        }

        public async Task<bool> UserEmailCheck(string email, CancellationToken token)
        {
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, token);

            return user != null;
        }

        public async Task<bool> Update(UserDTO modelDTO, CancellationToken token)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(modelDTO.Id, token);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            _mapper.Map(modelDTO, user);

            _unitOfWork.UserRepository.Update(user);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<UserDTO> GetUserByUser(ClaimsPrincipal User)
        {
            var user = await _userManager.GetUserAsync(User);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<SelectListItem>> GetTechersSelectItem(CancellationToken token)
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");

            return teachers.Where(x => x.FirstName != null || x.LastName != null)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList();
        }

        public async Task<List<SelectListItem>> GetStudentsSelectItem(CancellationToken token)
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Student");

            return teachers.Where(x => x.FirstName != null || x.LastName != null)
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.FirstName} {u.LastName}"
                }).ToList();
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByClass(int classId, CancellationToken token)
        {
            return _mapper.Map<IEnumerable<UserDTO>>((await _unitOfWork.UserRepository.GetAllAsync(token)).Where(x => x.ClassId == classId));
        }
    }
}
