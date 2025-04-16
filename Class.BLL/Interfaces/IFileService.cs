using Microsoft.AspNetCore.Http;

namespace School.BLL.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}
