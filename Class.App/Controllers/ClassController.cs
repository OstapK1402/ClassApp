using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.App.Models;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Context;
using School.DAL.Entities;

namespace School.App.Controllers
{
    public class ClassController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IClassService _classService;

        public ClassController(UserManager<User> userManager, IUserService userService, IClassService classService)
        {
            _userManager = userManager;
            _userService = userService;
            _classService = classService;
        }

        [Authorize]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var allClasses = await _classService.GetAll(token);
            return View("Index", allClasses);
        }

        public async Task<ActionResult> Detail(int classId, CancellationToken token)
        {
            var classe = await _classService.GetById(classId, token);

            if (classe == null)
            {
                TempData["Error"] = "Class not found!";
                return RedirectToAction("Index", "Class");
            }

            return View("Detail", classe);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> Create(CancellationToken token)
        {
            var model = new ClassDTO { Teachers = await _userService.GetTechersSelectItem(token) };

            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> Create(ClassDTO model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                model.Teachers = await _userService.GetTechersSelectItem(token);

                return View("Create", model);
            }
            else if (!await _classService.Create(model, token))
            {
                TempData["Error"] = "Something went wrong while creating class.";
                return View("Create", model);
            }

            return RedirectToAction("Index", "Class");
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Edit(int classId, CancellationToken token)
        {
            var classes = await _classService.GetById(classId, token);

            if (classes == null)
            {
                TempData["Error"] = "Class not found!";
                return RedirectToAction("Index", "Class");
            }

            classes.Teachers = await _userService.GetTechersSelectItem(token);

            return View("Edit", classes);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        [HttpPost]
        public async Task<IActionResult> Edit(int classId, ClassDTO classEdit, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                classEdit.Teachers = await _userService.GetTechersSelectItem(token);

                ModelState.AddModelError("", "Filed to edit class");
                return View("Edit", classEdit);
            }

            var classe = await _classService.GetByIdWithoutInclude(classId, token);

            if (classe != null)
            {
                classe.Name = classEdit.Name;
                classe.TeacherId = classEdit.TeacherId;

                await _classService.Update(classe, token);
                return RedirectToAction("Index", "Class");
            }
            else
            {
                TempData["Error"] = "Class not found!";
                return RedirectToAction("Index", "Class");
            }
        }


        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Delete(int classId, CancellationToken token)
        {
            var classe = await _classService.GetById(classId, token);

            if (classe == null)
            {
                TempData["Error"] = "Class not found!";
                return RedirectToAction("Index", "Class");
            }

            await _classService.Delete(classId, token);

            return RedirectToAction("Index", "Class");
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> AddStudent(int classId, CancellationToken token)
        {
            var classe = await _classService.GetById(classId, token);
            if (classe == null)
            {
                TempData["Error"] = "Class not found!";
                return RedirectToAction("Index", "Class");
            }

            var model = new AddStudentViewModel
            {
                ClassId = classId,
                Classes = await _classService.GetSelectItem(token),
                Students = await _userService.GetStudentsSelectItem(token)
            };

            return View("AddStudent", model);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentViewModel model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                model.Classes = await _classService.GetSelectItem(token);
                model.Students = await _userService.GetStudentsSelectItem(token);
                return View("AddStudent", model);
            }

            var classe = await _classService.GetById(model.ClassId, token);
            if (classe == null)
            {
                TempData["Error"] = "Class not found!";
                return RedirectToAction("Index", "Class");
            }

            var student = await _userManager.FindByIdAsync(model.StudentId.ToString());
            if (student.ClassId != null)
            {
                model.Classes = await _classService.GetSelectItem(token);
                model.Students = await _userService.GetStudentsSelectItem(token);
                TempData["Error"] = "Student enrolled in class!";
                return View("AddStudent", model);
            }
            if (student == null)
            {
                TempData["Error"] = "Student not found!";
                return RedirectToAction("AddStudent", new { classId = model.ClassId });//??
            }

            student.ClassId = classe.Id;
            await _userManager.UpdateAsync(student);

            return RedirectToAction("Detail", new { classId = classe.Id });
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> RemoveStudent(int classId, int studentId, CancellationToken token)
        {
            if (!await _classService.RemoveStudentFromClass(classId, studentId, token))
            {
                TempData["Error"] = "Failed to remove student from class.";
                return RedirectToAction("Detail", classId);
            }

            return RedirectToAction("Detail", new { classId = classId });
        }
    }
}
