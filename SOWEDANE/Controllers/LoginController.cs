using Microsoft.AspNetCore.Mvc;
using SOWEDANE.EntityFrameworkContext;
using SOWEDANE.Utils;

namespace SOWEDANE.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public LoginController(ApplicationDbContext applicationDbContext)

        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string LoginName,string Password) 
        {
            var user=this.applicationDbContext.Users.Where(x => x.LoginName == LoginName).FirstOrDefault();
            
            string encryptedPassword=PasswordHasher.HashPassword(user.GeneratedSalt, Password);

            if (user == null)
            {
                var jsonresult = new {success=false,message="User Doesnt exist. Try Register" };
                return new JsonResult(jsonresult);
            }
            if (user.Password == encryptedPassword)
            {
                var jsonresult = new { success = true, message = "Login Success" };
                HttpContext.Session.SetString("isLoggedIn","true");
                HttpContext.Session.SetString("LoggedInUserName", user.LoginName);
                HttpContext.Session.SetInt32("userId", user.Id);
                return new JsonResult(jsonresult);
            }
           

                var result = new { success = false, message = "invalid Credentials" };
                return new JsonResult(result);
          
        }
    }
}
