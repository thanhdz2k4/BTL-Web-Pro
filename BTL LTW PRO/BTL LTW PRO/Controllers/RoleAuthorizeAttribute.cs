using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BTL_LTW_PRO.Controllers
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        // Nhận danh sách role được phép truy cập
        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("UserRole");

            // Kiểm tra nếu người dùng chưa đăng nhập hoặc không có quyền phù hợp
            if (string.IsNullOrEmpty(role) )
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            if (!_roles.Contains(role))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
