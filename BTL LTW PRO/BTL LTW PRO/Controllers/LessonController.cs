using BTL_LTW_PRO.Data;
using BTL_LTW_PRO.Models;
using BTL_LTW_PRO.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW_PRO.Controllers
{
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LessonController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            int idCourseTest = 1;
            var lessons = _context.Lessons.Where(p => p.CourseID == idCourseTest).ToList();
            ViewData["idCourse"] = idCourseTest;

            return View(lessons);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Lesson lesson)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new { Field = e.Key, Errors = e.Value.Errors.Select(err => err.ErrorMessage) })
                    .ToList();

                return Json(new { success = false, message = "Dữ liệu không hợp lệ", errors });
            }

            _context.Lessons.Add(lesson);
            _context.SaveChanges();

            return Json(new { success = true, lesson });
        }







    }
}
