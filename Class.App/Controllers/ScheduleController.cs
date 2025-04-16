using Microsoft.AspNetCore.Mvc;
using School.App.Models;
using School.BLL.DTO;
using School.BLL.Interfaces;

namespace School.App.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IUserService _userService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService, IUserService userService, IClassService classService, 
            ISubjectService subjectService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _classService = classService;
            _subjectService = subjectService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(string className, CancellationToken token)
        {
            var classe = await _classService.GetByName(className, token);
            var model = new ScheduleDTO
            {
                ClassId = classe.Id,
                Teachers = await _userService.GetTechersSelectItem(token),
                Subjects = await _subjectService.GetSelectItem(token)
            };

            return View("Create", model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ScheduleDTO model, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                model.Subjects = await _subjectService.GetSelectItem(token);
                model.Teachers = await _userService.GetTechersSelectItem(token);
                return View("Create", model);
            }

            else if (!await _scheduleService.Create(model, token))
            {
                TempData["Error"] = "Something went wrong while creating schedule.";
                return View("Create", model);
            }

            return RedirectToAction("ClassSchedule", new { classId = model.ClassId });
        }

        public async Task<IActionResult> ClassSchedule(int classId, CancellationToken token)
        {
            var scheduleData = await _scheduleService.GetByClass(classId, token);
            var className = (await _classService.GetById(classId, token)).Name;

            var weekDays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

            var timeSlots = scheduleData.Select(x => x.TimeSlot).Distinct().OrderBy(x => x).ToList();

            var scheduleDict = new Dictionary<TimeSpan, Dictionary<DayOfWeek, ScheduleCellViewModel>>();

            foreach (var time in timeSlots)
            {
                scheduleDict[time] = new Dictionary<DayOfWeek, ScheduleCellViewModel>();
                foreach (DayOfWeek day in weekDays)
                {
                    var lesson = scheduleData.FirstOrDefault(x => x.TimeSlot == time && x.DayOfWeek == day);
                    if (lesson != null)
                    {
                        scheduleDict[time][day] = new ScheduleCellViewModel
                        {
                            SubjectName = lesson.Subject.Name,
                            TeacherName = lesson.Teacher.FullName
                        };
                    }
                }
            }

            var viewModel = new ScheduleTableViewModel
            {
                ClassName = className,
                TimeSlots = timeSlots,
                Schedule = scheduleDict
            };

            return View("ClassSchedule", viewModel);
        }
    }
}
