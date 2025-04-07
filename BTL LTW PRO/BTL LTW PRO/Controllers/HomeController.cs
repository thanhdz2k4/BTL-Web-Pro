using System.Diagnostics;
using BTL_LTW_PRO.Data;
using BTL_LTW_PRO.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW_PRO.Controllers
{
    [RoleAuthorize("Admin", "Teacher")]
    public class HomeController : BaseController
    {
        
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [RoleAuthorize("Admin", "Teacher")]
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            string? userIdString = HttpContext.Session.GetString("UserID");
            int? teacherId = null;

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
            {
                teacherId = parsedId;
            }

            var courses = await _context.Courses.Where(c => c.InstructorID == teacherId).ToListAsync();


            
            return Ok(courses); // Trả về JSON
        }

        //[RoleAuthorize("Admin", "Teacher")]
        public IActionResult Index()
        {
            return View(); // Trả về View
        }

        [HttpPost]
        [Route("Home/Create")]
        public async Task<IActionResult> Create([FromBody] Course course)
        {
            if (course == null || string.IsNullOrEmpty(course.CourseName))
            {
                return BadRequest("Invalid course data.");
            }

            course.CreatedAt = DateTime.Now; // Gán thời gian tạo
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Course created successfully", course });
        }

        public async Task<IActionResult> Create()
        {
            var lastCourse = await _context.Courses
                                  .OrderByDescending(c => c.CourseID)
                                  .FirstOrDefaultAsync();

            // Lấy số từ CourseID (bỏ tiền tố)
            int lastNumber = lastCourse != null
                ? int.Parse(lastCourse.CourseID.Replace("COURSE", ""))
                : 0;

            // Tạo CourseID mới theo trình tự
            int newNumber = lastNumber + 1;
            
            ViewBag.UserID = HttpContext.Session.GetString("UserID");
            ViewBag.CourseID = $"COURSE{newNumber:D4}";
            return View();
        }

        [HttpGet("Home/ListStudent/{courseID}")]
        public IActionResult ListStudent(string courseID)
        {

            ViewData["CourseID"] = courseID;
            
            return View();
        }


        // giáo viên lấy ra danh sách học sinh xin vào lớp
        [HttpGet("Home/PendingRequests/{courseID}")]
        public IActionResult PendingRequests(string courseID)
        {
            string? userIdString = HttpContext.Session.GetString("UserID");
            int? teacherId = null;

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
            {
                teacherId = parsedId;
            }
            var requests = _context.Enrollments
                                   .Include(e => e.User)
                                   .Include(e => e.Course)
                                   .Where(e => e.Sstatus == "Pending" && e.CourseID == courseID && e.Course.InstructorID == teacherId )
                                   .ToList();

            return Json(requests);
        }

        [HttpGet("Home/ApprovedStudents/{courseID}")]
        public  IActionResult ApprovedStudents(string courseID)
        {
            string? userIdString = HttpContext.Session.GetString("UserID");
            int? teacherId = null;

            
            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
            {
                teacherId = parsedId;
            }

            // Lấy danh sách học sinh đã được duyệt trong khóa học
            var approvedStudents = _context.Enrollments
                                            .Include(e => e.User)   
                                            .Include(e => e.Course)  
                                            .Where(e => e.CourseID == courseID && e.Course.InstructorID == teacherId && e.Sstatus == "Approved")
                                            .ToList();

          
        

            return Json(approvedStudents);
        }


        // chuyển trạng thái pending => approved: đồng ý cho học sinh tham gia lớp học
        [HttpPost]
        public IActionResult ApproveStudent(int id)
        {

            var enrollment = _context.Enrollments.Find(id);
            if (enrollment == null) return NotFound();

            enrollment.Sstatus = "Approved";
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // xoá hoặc từ chối học sinh tham gia vào lớp học
        [HttpDelete]
        public IActionResult RejectStudent(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment == null) return NotFound();

            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();

            return Json(new { success = true });
        }




        public IActionResult Privacy()
        {
            return View();
        }



    }
}
