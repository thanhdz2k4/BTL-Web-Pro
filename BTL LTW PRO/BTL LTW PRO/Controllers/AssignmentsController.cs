using Microsoft.AspNetCore.Mvc;
using BTL_LTW_PRO.Data;
using BTL_LTW_PRO.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BTL_LTW_PRO.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hiển thị danh sách bài tập
        public async Task<IActionResult> Index(int LessonID)
        {
            var assignments = await _context.Assignments
                .Where(p => p.LessonID == LessonID)
                .ToListAsync();

            ViewData["UserRole"] = HttpContext.Session.GetString("UserRole");
            ViewBag.Lessons = await _context.Lessons.ToListAsync();

            return View(assignments);
        }



        // POST: Thêm bài tập bằng AJAX
        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Assignments.Add(assignment);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Thêm bài tập thành công",
                    data = assignment
                });
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return Json(new
            {
                success = false,
                message = "Dữ liệu không hợp lệ",
                errors
            });
        }

        // GET: Hiển thị form sửa bài tập
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
                return NotFound();

            ViewBag.Lessons = await _context.Lessons.ToListAsync();
            return View(assignment);
        }

        // POST: Xử lý sửa bài tập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Assignment assignment)
        {
            if (id != assignment.AssignmentID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Assignments.Any(a => a.AssignmentID == id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewBag.Lessons = await _context.Lessons.ToListAsync();
            return View(assignment);
        }

        // GET: Xác nhận xóa bài tập
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var assignment = await _context.Assignments
                .Include(a => a.Lesson)
                .FirstOrDefaultAsync(m => m.AssignmentID == id);

            if (assignment == null)
                return NotFound();

            return View(assignment);
        }

        // POST: Xác nhận xóa bài tập
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
                return NotFound();

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //////////
        public async Task<IActionResult> StudentIndex()
{
    var assignments = await _context.Assignments
        .Include(a => a.Lesson)
        .ToListAsync();

    return View(assignments);
}



    }
}
