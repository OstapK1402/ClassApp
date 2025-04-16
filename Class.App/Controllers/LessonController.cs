using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.App.Models;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Context;

namespace School.App.Controllers
{
    public class LessonController : Controller
    {
        private readonly IHomeworkService _homeworkService;
        private readonly IUserService _userService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly IClassSubjectService _classSubjectService;

        public LessonController(IUserService userService, IClassService classService, ISubjectService subjectService, 
            IClassSubjectService classSubjectService, IHomeworkService homeworkService)
        {
            _userService = userService;
            _classService = classService;
            _subjectService = subjectService;
            _classSubjectService = classSubjectService;
            _homeworkService = homeworkService;
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> TeacherIndex(CancellationToken token)
        {
            var user = await _userService.GetUserByUser(User);
            var lessons = await _classSubjectService.GetAllByTeacherId(user.Id, token);
            return View("Index", lessons);
        }

        [Authorize(Roles = UserRole.STUDENT)]
        public async Task<ActionResult> StudentIndex(CancellationToken token)
        {
            var user = await _userService.GetUserByUser(User);

            if (!user.ClassId.HasValue)
            {
                TempData["Error"] = "ClassId is required.";
            }

            var lessons = await _classSubjectService.GetAllByClassId(user.ClassId.Value, token);
            return View("Index", lessons);
        }

        public async Task<ActionResult> Detail(int classId, int subjectId, CancellationToken token)
        {
            var lesson = await _classSubjectService.GetById(classId, subjectId, token);

            if (lesson == null)
            {
                TempData["Error"] = "Lesson not found!";
                return RedirectToAction("Index", "Class");
            }

            var homeworks = await _homeworkService.GetByClassSubject(classId, subjectId, token);

            var viewModel = new LessonDetailsViewModel
            {
                ClassSubject = lesson,
                Homeworks = homeworks
            };

            return View("Detail", viewModel);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> Create(int classId, CancellationToken token)
        {
            var model = new ClassSubjectDTO 
            { 
                ClassId = classId,
                Teachers = await _userService.GetTechersSelectItem(token),
                Classes = await _classService.GetSelectItem(token),
                Subjects = await _subjectService.GetSelectItem(token) 
            };

            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> Create(ClassSubjectDTO model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                model.Teachers = await _userService.GetTechersSelectItem(token);
                model.Classes = await _classService.GetSelectItem(token);
                model.Subjects = await _subjectService.GetSelectItem(token);

                return View("Create", model);
            }
            else if (!await _classSubjectService.Create(model, token))
            {
                model.Teachers = await _userService.GetTechersSelectItem(token);
                model.Classes = await _classService.GetSelectItem(token);
                model.Subjects = await _subjectService.GetSelectItem(token);

                TempData["Error"] = "Something went wrong while creating lesson.";
                return View("Create", model);
            }

            return RedirectToAction("Detail", "Class", new {classId = model.ClassId});
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Edit(int classId, int subjectId, CancellationToken token)
        {
            var lesson = await _classSubjectService.GetById(classId, subjectId, token);

            if (lesson == null)
            {
                TempData["Error"] = "Lesson not found!";
                return RedirectToAction("Detail", "Class", new { classId = classId });
            }

            lesson.Teachers = await _userService.GetTechersSelectItem(token);

            return View("Edit", lesson);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        [HttpPost]
        public async Task<IActionResult> Edit(int classId, int subjectId, ClassSubjectDTO lessonEdit, CancellationToken token)
        {
            lessonEdit.SubjectId = subjectId;
            lessonEdit.ClassId = classId;
            if (!ModelState.IsValid)
            {
                lessonEdit.Teachers = await _userService.GetTechersSelectItem(token);

                ModelState.AddModelError("", "Filed to edit lesson");
                return View("Edit", lessonEdit);
            }

            var lesson = await _classSubjectService.GetById(classId, subjectId, token);

            if (lesson != null)
            {
                lesson.TeacherId = lessonEdit.TeacherId;

                await _classSubjectService.Update(lesson, token);
                return RedirectToAction("Detail", "Class", new { classId = classId });
            }
            else
            {
                TempData["Error"] = "Lesson not found!";
                return RedirectToAction("Detail", "Class", new { classId = classId });
            }
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Delete(int classId, int subjectId, CancellationToken token)
        {
            var lesson = await _classSubjectService.GetById(classId, subjectId, token);

            if (lesson == null)
            {
                TempData["Error"] = "Lesson not found!";
                return RedirectToAction("Detail", "Class", new { classId = classId });
            }

            await _classSubjectService.Delete(classId, subjectId, token);

            return RedirectToAction("Detail", "Class", new { classId = classId });
        }
    }
}
