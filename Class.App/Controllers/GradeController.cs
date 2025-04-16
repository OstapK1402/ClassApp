using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Context;

namespace School.App.Controllers
{
    public class GradeController : Controller
    {
        private readonly IHomeworkSubmissionService _homeworkSubmissionService;
        private readonly IUserService _userService;
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService, IUserService userService, IHomeworkSubmissionService homeworkSubmissionService)
        {
            _gradeService = gradeService;
            _userService = userService;
            _homeworkSubmissionService = homeworkSubmissionService;
        }

        [HttpPost]
        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Create(int submissionId, GradeDTO model, CancellationToken token)
        {
            var submission = await _homeworkSubmissionService.GetById(submissionId, token);
            model.TeacherId = (await _userService.GetUserByUser(User)).Id;
            model.Date = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Detail", "HomeworkSubmission", Tuple.Create(submission, model));
            }
            else if (!await _gradeService.Create(model, token))
            {
                TempData["Error"] = "Something went wrong while creating grade.";
                return View("Detail", Tuple.Create(submission, model));
            }

            return RedirectToAction("DetailForTeacher", "Homework", new {homeworkId = submission.HomeworkId});
        }
    }
}
