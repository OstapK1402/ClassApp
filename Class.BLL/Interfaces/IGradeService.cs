using School.BLL.DTO;

namespace School.BLL.Interfaces
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeDTO>> GetAll(CancellationToken token);
        Task<GradeDTO> GetById(int id, CancellationToken token);
        Task<bool> Create(GradeDTO modelDTO, CancellationToken token);
        Task<bool> Update(GradeDTO modelDTO, CancellationToken token);
        Task<bool> Delete(int id, CancellationToken token);
        Task<GradeDTO?> GetBySubmission(int submissionId, CancellationToken token);
    }
}
