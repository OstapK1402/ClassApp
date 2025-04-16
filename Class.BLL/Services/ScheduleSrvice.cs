using AutoMapper;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class ScheduleSrvice : IScheduleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleSrvice(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Create(ScheduleDTO modelDTO, CancellationToken token)
        {
            modelDTO.Id = 0;
            var schedule = _mapper.Map<Schedule>(modelDTO);

            await _unitOfWork.ScheduleRepository.CreateAsync(schedule, token);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id, CancellationToken token)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetByIdAsync(id, token);

            if (schedule == null)
            {
                throw new KeyNotFoundException("Schedule not found!");
            }

            _unitOfWork.ScheduleRepository.Delete(schedule);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<ScheduleDTO>> GetAll(CancellationToken token)
        {
            return _mapper.Map<IEnumerable<ScheduleDTO>>(await _unitOfWork.ScheduleRepository.GetAllAsync(token));
        }

        public async Task<ScheduleDTO> GetById(int id, CancellationToken token)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetByIdAsync(id, token);

            if (schedule == null)
            {
                throw new KeyNotFoundException("Schedule not found!");
            }

            return _mapper.Map<ScheduleDTO>(schedule);
        }

        public async Task<bool> Update(ScheduleDTO modelDTO, CancellationToken token)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetByIdAsync(modelDTO.Id, token);

            if (schedule == null)
            {
                throw new KeyNotFoundException("Schedule not found!");
            }

            schedule = _mapper.Map<Schedule>(modelDTO);

            _unitOfWork.ScheduleRepository.Update(schedule);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<IEnumerable<ScheduleDTO>> GetByClass(int classId, CancellationToken token)
        {
            var schedules =  _mapper.Map<IEnumerable<ScheduleDTO>>(await _unitOfWork.ScheduleRepository.GetAllAsync(token));
            return schedules.Where(x => x.ClassId == classId);
        }
    }
}
