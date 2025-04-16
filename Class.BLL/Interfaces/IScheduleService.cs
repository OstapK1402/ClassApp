using School.BLL.DTO;

namespace School.BLL.Interfaces
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetAll(CancellationToken token);
        Task<ScheduleDTO> GetById(int id, CancellationToken token);
        Task<bool> Create(ScheduleDTO modelDTO, CancellationToken token);
        Task<bool> Update(ScheduleDTO modelDTO, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
        Task<IEnumerable<ScheduleDTO>> GetByClass(int classId, CancellationToken token);
    }
}
