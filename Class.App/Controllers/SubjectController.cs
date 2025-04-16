using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Context;

namespace School.App.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [Authorize]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            var allSubjects = await _subjectService.GetAll(token);
            return View("Index", allSubjects);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<ActionResult> Create(SubjectDTO subject, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", subject);
            }
            else if (!await _subjectService.Create(subject, token))
            {
                TempData["Error"] = "Something went wrong while creating subject.";
                return View("Create", subject);
            }

            return RedirectToAction("Index", "Subject");
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Edit(int subjectId, CancellationToken token)
        {
            var subject = await _subjectService.GetById(subjectId, token);

            if (subject == null)
            {
                TempData["Error"] = "Subject not found!";
                return RedirectToAction("Index", "Subject");
            }

            return View("Edit", subject);
        }

        [Authorize(Roles = UserRole.TEACHER)]
        [HttpPost]
        public async Task<IActionResult> Edit(int subjectId, SubjectDTO subjectEdit, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Filed to edit class");
                return View("Edit", subjectEdit);
            }

            var classe = await _subjectService.GetById(subjectId, token);

            if (classe != null)
            {
                classe.Name = subjectEdit.Name;

                await _subjectService.Update(classe, token);
                return RedirectToAction("Index", "Subject");
            }
            else
            {
                TempData["Error"] = "Subject not found!";
                return RedirectToAction("Index", "Subject");
            }
        }


        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> Delete(int subjectId, CancellationToken token)
        {
            var subject = await _subjectService.GetById(subjectId, token);

            if (subject == null)
            {
                TempData["Error"] = "Subject not found!";
                return RedirectToAction("Index", "Subject");
            }

            await _subjectService.Delete(subjectId, token);

            return RedirectToAction("Index", "Subject");
        }
    }
}
