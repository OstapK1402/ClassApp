using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.App.Models;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Context;

namespace School.App.Controllers
{
    public class HomeworkSubmissionController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IHomeworkSubmissionService _homeworkSubmissionService;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeworkSubmissionController(IUserService userService, IHomeworkService homeworkService, 
            IHomeworkSubmissionService homeworkSubmissionService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _homeworkService = homeworkService;
            _homeworkSubmissionService = homeworkSubmissionService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Detail(int submissionId, CancellationToken token)
        {
            var submission = await _homeworkSubmissionService.GetById(submissionId, token);
            if (submission == null)
            {
                TempData["Error"] = "Homework submission not found!";
                return RedirectToAction("Index", "Class");//??
            }

            var user = await _userService.GetUserByUser(User);

            var gradeDto = new GradeDTO
            {
                HomeworkSubmissionId = submissionId,
                TeacherId = user.Id,
                Date = DateTime.Today
            };

            return View("Detail", Tuple.Create(submission, gradeDto));
        }

        public async Task<IActionResult> Download(int submissionId, CancellationToken token)
        {
            var submission = await _homeworkSubmissionService.GetById(submissionId, token);
            if (submission == null || string.IsNullOrEmpty(submission.FilePath))
                return NotFound();

            var relativePath = submission.FilePath.TrimStart('/');

            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, relativePath);

            if (!System.IO.File.Exists(fullPath))
                return NotFound("File not found.");

            var memory = new MemoryStream();
            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;
            var contentType = "application/octet-stream";
            var fileName = Path.GetFileName(fullPath);
            return File(memory, contentType, fileName);
        }


        [HttpPost]
        [Authorize(Roles = UserRole.STUDENT)]
        public async Task<IActionResult> Submission(int homeworkId, IFormFile file, CancellationToken token)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Please select a file.");
                return RedirectToAction("Detail", new { homeworkId = homeworkId });
            }

            var user = await _userService.GetUserByUser(User);
            var existingSubmission = await _homeworkSubmissionService.GetByHomeworkAndStudent(homeworkId, user.Id, token);

            if (existingSubmission != null)
            {
                ModelState.AddModelError("", "You have already submitted this homework.");
                return RedirectToAction("Detail", new { homeworkId = homeworkId });
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/homework");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var submission = new HomeworkSubmissionDTO
            {
                HomeworkId = homeworkId,
                StudentId = user.Id,
                FilePath = $"/uploads/homework/{fileName}",
                SubmittedAt = DateTime.UtcNow
            };

            if (!await _homeworkSubmissionService.Create(submission, token))
            {
                var viewModel = new HomeworkDetailsViewModel
                {
                    Homework = await _homeworkService.GetById(homeworkId, token),
                    Submission = submission
                };

                TempData["Error"] = "Something went wrong while creating homework submission.";
                return View("Create", viewModel);
            }

            return RedirectToAction("Detail", "Homework",new { homeworkId = homeworkId });
        }
    }
}
