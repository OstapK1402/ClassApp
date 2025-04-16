using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.App.Models;
using School.BLL.DTO;
using School.BLL.Interfaces;
using School.DAL.Context;
using School.DAL.Entities;

namespace School.App.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public ProfileController(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, UserRole.TEACHER))
            {
                return RedirectToAction("TeacherProfile", "Profile");
            }
            else if (await _userManager.IsInRoleAsync(user, UserRole.STUDENT))
            {
                return RedirectToAction("StudentProfile", "Profile");
            }

            return RedirectToAction("Logout", "Acсount");
        }

        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> TeacherProfile()
        {
            var user = await _userService.GetUserByUser(User);

            return View("TeacherProfile", user);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.TEACHER)]
        public async Task<IActionResult> TeacherProfile(int teacherId, UserDTO editTeacher, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Filed to edit profile";
                return View("TeacherProfile", editTeacher);
            }

            var teacher = await _userService.GetById(teacherId, token);

            if (teacher == null)
            {
                TempData["Error"] = "Teacher not found!";
                return RedirectToAction("Logout", "Acсount");
            }

            teacher.FirstName = editTeacher.FirstName;
            teacher.LastName = editTeacher.LastName;
            teacher.DateOfBirth = editTeacher.DateOfBirth;

            if (!await _userService.Update(teacher, token))
            {
                TempData["Error"] = "Something went wrong while updating profile.";
                return View("TeacherProfile", editTeacher);
            }
            return RedirectToAction("TeacherProfile", "Profile");
        }

        [Authorize(Roles = UserRole.STUDENT)]
        public async Task<IActionResult> StudentProfile()
        {
            var user = await _userService.GetUserByUser(User);

            return View("StudentProfile", user);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.STUDENT)]
        public async Task<IActionResult> StudentProfile(int studentId, UserDTO editStudent, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Filed to edit profile";
                return View("StudentProfile", editStudent);
            }

            var student = await _userService.GetById(studentId, token);

            if (student == null)
            {
                TempData["Error"] = "Student not found!";
                return RedirectToAction("Logout", "Acсount");
            }

            student.FirstName = editStudent.FirstName;
            student.LastName = editStudent.LastName;
            student.DateOfBirth = editStudent.DateOfBirth;

            if (!await _userService.Update(student, token))
            {
                TempData["Error"] = "Something went wrong while updating profile.";
                return View("StudentProfile", editStudent);
            }
            return RedirectToAction("StudentProfile", "Profile");
        }

        public IActionResult CreateProfile()
        {
            return View("CreateProfile");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(ProfileViewModel profile, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateProfile", profile);
            }
            var user = await _userService.GetUserByUser(User);

            user.LastName = profile.LastName;
            user.FirstName = profile.FirstName;
            user.DateOfBirth = profile.DateOfBirth;

            if (!await _userService.Update(user, token))
            {
                TempData["Error"] = "Something went wrong while creating profile.";
                return View("CreateProfile", profile);
            }

            if (await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), UserRole.TEACHER))
            {
                return RedirectToAction("TeacherProfile", "Profile");
            }
            else
            {
                return RedirectToAction("StudentProfile", "Profile");
            }
        }
    }
}