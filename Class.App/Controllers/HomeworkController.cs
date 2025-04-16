using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.App.Models;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Context;

namespace School.App.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IHomeworkSubmissionService _homeworkSubmissionService;
        private readonly IUserService _userService;
        private readonly IClassSubjectService _classSubjectService;
        private readonly IGradeService _gradeService;

        public HomeworkController(IUserService userService,IClassSubjectService classSubjectService, IHomeworkService homeworkService, 
            IHomeworkSubmissionService homeworkSubmissionService, IGradeService gradeService)
        {
            _userService = userService;
            _classSubjectService = classSubjectService;
            _homeworkService = homeworkService;
            _homeworkSubmissionService = homeworkSubmissionService;
            _gradeService = gradeService;
        }

        public async Task<IActionResult> Detail(int homeworkId, CancellationToken token)
        {
            var homework = await _homeworkService.GetById(homeworkId, token);

            if (homework == null)
            {
                TempData["Error"] = "Homework not found!";
                return RedirectToAction("Index", "Class");//??
            }

            var user = await _userService.GetUserByUser(User);
            var homeworkSubmission = await _homeworkSubmissionService.GetByHomeworkAndStudent(homeworkId, user.Id, token);
            GradeDTO? grade = null;

            if (homeworkSubmission != null)
            {
                grade = await _gradeService.GetBySubmission(homeworkSubmission.Id, token);
            }

            var viewModel = new HomeworkDetailsViewModel
            {
                Homework = homework,
                Submission = homeworkSubmission,
                Grade = grade
            };

            return View("Detail", viewModel);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> DetailForTeacher(int homeworkId, CancellationToken token)
        {
            var homework = await _homeworkService.GetById(homeworkId, token);

            if (homework == null)
            {
                TempData["Error"] = "Homework not found!";
                return RedirectToAction("Index", "Class");//??
            }

            var studentsInClass = await _userService.GetUsersByClass(homework.ClassId, token);

            var submissions = await _homeworkSubmissionService.GetByHomework(homeworkId, token);
            var grades = await _gradeService.GetAll(token);

            var viewModel = new HomeworkSubmissionsViewModel
            {
                Homework = homework,
                Students = studentsInClass.Select(s => {
                    var submission = submissions.FirstOrDefault(sub => sub.StudentId == s.Id);
                    var grade = submission != null ? grades.FirstOrDefault(g => g.HomeworkSubmissionId == submission.Id) : null;
                    return new StudentSubmissionStatus
                    {
                        StudentId = s.Id,
                        StudentFullName = s.FullName,
                        HasSubmitted = submission != null,
                        SubmissionId = submission?.Id,
                        SubmittedAt = submission?.SubmittedAt,
                        GradeValue = grade?.GradeValue
                    };
                }).ToList()
            };

            return View("DetailForTeacher", viewModel);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> Create(int classId, int subjectId, int teacherId, CancellationToken token)
        {
            var lesson = await _classSubjectService.GetById(classId, subjectId, token);

            if (lesson == null || lesson.TeacherId != teacherId)
            {
                TempData["Error"] = "Lesson not found!";
                return RedirectToAction("Detail", "Lesson", new { classId = classId, subjectId = subjectId });
            }

            var homework = new HomeworkDTO
            {
                ClassId = classId,
                SubjectId = subjectId,
                TeacherId = teacherId,
                Teacher = lesson.Teacher,
                Class = lesson.Class,
                Subject = lesson.Subject,
                DueDate = DateTime.Today
            };

            return View("Create", homework);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> Create(int classId, int subjectId, int teacherId, HomeworkDTO model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                var lesson = await _classSubjectService.GetById(classId, subjectId, token);

                model.ClassId = classId;
                model.SubjectId = subjectId;
                model.TeacherId = teacherId;
                model.Teacher = lesson.Teacher;
                model.Class = lesson.Class;
                model.Subject = lesson.Subject;

                return View("Create", model);
            }

            model.CreatedAt = DateTime.Now;
            if (!await _homeworkService.Create(model, token))
            {
                var lesson = await _classSubjectService.GetById(classId, subjectId, token);

                model.ClassId = classId;
                model.SubjectId = subjectId;
                model.TeacherId = teacherId;
                model.Teacher = lesson.Teacher;
                model.Class = lesson.Class;
                model.Subject = lesson.Subject;
                model.CreatedAt = DateTime.Now;

                TempData["Error"] = "Something went wrong while creating homework.";
                return View("Create", model);
            }

            return RedirectToAction("Detail", "Lesson", new { classId = classId, subjectId = subjectId });
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Edit(int homeworkId, CancellationToken token)
        {
            var homework = await _homeworkService.GetById(homeworkId, token);

            if (homework == null)
            {
                TempData["Error"] = "Homework not found!";
                return RedirectToAction("TeacherIndex", "Lesson");
            }

            return View("Edit", homework);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        [HttpPost]
        public async Task<IActionResult> Edit(int homeworkId, HomeworkDTO homeworkEdit, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Filed to edit Homework");
                return View("Edit", homeworkEdit);
            }

            var homework = await _homeworkService.GetById(homeworkId, token);

            if (homework != null)
            {
                homework.Title = homeworkEdit.Title;
                homework.Description = homeworkEdit.Description;
                homework.DueDate = homeworkEdit.DueDate;

                await _homeworkService.Update(homework, token);
                return RedirectToAction("Detail", "Lesson", new { classId =  homework.ClassId, subjectId = homework.SubjectId });
            }
            else
            {
                TempData["Error"] = "Homework not found!";
                return RedirectToAction("TeacherIndex", "Lesson");
            }
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Delete(int homeworkId, CancellationToken token)
        {
            var homework = await _homeworkService.GetById(homeworkId, token);

            if (homework == null)
            {
                TempData["Error"] = "Homework not found!";
                return RedirectToAction("TeacherIndex", "Lesson");
            }

            await _homeworkService.Delete(homeworkId, token);

            return RedirectToAction("Detail", "Lesson", new { classId = homework.ClassId, subjectId = homework.SubjectId });
        }


    }
}