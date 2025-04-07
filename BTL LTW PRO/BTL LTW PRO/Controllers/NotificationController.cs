//using BTL_LTW_PRO.Data;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace BTL_LTW_PRO.Controllers
//{
  
//    public class NotificationController : BaseController
//    {
//        private readonly ApplicationDbContext _context;

//        public NotificationController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetNotifications()
//        {

//            string? userIdString = HttpContext.Session.GetString("UserID");
//            int? userId = null;


//            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
//            {
//                userId = parsedId;
//            }

//            var notifications = await _context.Notifications
//                .Where(n => n.UserId == userId)
//                .OrderByDescending(n => n.CreatedAt)
//                .Take(10)
//                .Select(n => new {
//                    n.Id,
//                    n.Message,
//                    n.IsRead,
//                    Time = n.CreatedAt.ToString("dd/MM/yyyy HH:mm")
//                })
//                .ToListAsync();

//            return Json(notifications);
//        }

//        [HttpPost]
//        public async Task<IActionResult> MarkAllAsRead()
//        {
//            string? userIdString = HttpContext.Session.GetString("UserID");
//            int? userId = null;


//            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int parsedId))
//            {
//                userId = parsedId;
//            }


//            var notis = await _context.Notifications
//                .Where(n => n.UserId == userId && !n.IsRead)
//                .ToListAsync();

//            notis.ForEach(n => n.IsRead = true);
//            await _context.SaveChangesAsync();

//            return Ok();
//        }
//    }

//}
