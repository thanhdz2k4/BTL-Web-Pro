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

   
        public IActionResult Index(string id)
        {
            string userRole = HttpContext.Session.GetString("UserRole");
            var lessons = _context.Lessons.Where(p => p.CourseID == id).ToList();
            ViewData["idCourse"] = id;
            ViewData["UserRole"] = userRole;

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


        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var lesson = _context.Lessons.Find(id);
            if (lesson == null)
            {
                return Json(new { success = false, message = "Bài học không tồn tại!" });
            }

            _context.Lessons.Remove(lesson);
            _context.SaveChanges();

            return Json(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }


        public IActionResult Detail(int id)
        {
            var lesson = _context.Lessons.FirstOrDefault(l => l.LessonID == id);

            string userRole = HttpContext.Session.GetString("UserRole");

            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["UserRole"] = userRole;
            return View(lesson); // Truyền đối tượng Lesson vào view
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromBody] Lesson lesson)
        {
            if (id != lesson.LessonID)
            {
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }

            var existingLesson = await _context.Lessons.FindAsync(id);
            if (existingLesson == null)
            {
                return NotFound(new { success = false, message = "Bài học không tồn tại!" });
            }

            try
            {
                existingLesson.Title = lesson.Title;
                existingLesson.Content = lesson.Content;
                existingLesson.VideoURL = lesson.VideoURL;
                existingLesson.BeginTime = lesson.BeginTime;
                existingLesson.EndTime = lesson.EndTime;

                await _context.SaveChangesAsync();

                return Ok(new { success = true, lesson = existingLesson });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Lỗi hệ thống!", error = ex.Message });
            }
        }










    }
}
