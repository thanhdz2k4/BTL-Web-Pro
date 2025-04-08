using Microsoft.AspNetCore.Mvc;
using BTL_LTW_PRO.Data;
using BTL_LTW_PRO.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO; // Thêm dòng này để dùng FileStream
using Microsoft.AspNetCore.Http;
using System;

namespace BTL_LTW_PRO.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> UploadSubmission(int assignmentId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "Vui lòng chọn file." });

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var submission = new Submission
            {
                AssignmentID = assignmentId,
                UserID = 1, // ✅ Tạm thời dùng cứng, sau này lấy từ User.Identity
                FileURL = "/uploads/" + uniqueFileName,
                SubmittedAt = DateTime.Now
            };

            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
