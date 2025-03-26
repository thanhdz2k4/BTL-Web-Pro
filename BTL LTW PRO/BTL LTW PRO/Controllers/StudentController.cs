using BTL_LTW_PRO.Data;
using BTL_LTW_PRO.Models;
using BTL_LTW_PRO.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL_LTW_PRO.Controllers
{
    public class StudentController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult show()
        {
            return View();
        }
    }
}