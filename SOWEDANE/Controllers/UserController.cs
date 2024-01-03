using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SOWEDANE.EntityFrameworkContext;
using SOWEDANE.Models;
using SOWEDANE.Utils;

namespace SOWEDANE.Controllers
{
    public class UserController : Controller
    {
       // private readonly UserContext userDbContext;

        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserOtpController userOtpController;
        public UserController(ApplicationDbContext applicationDbContext, UserOtpController userOtpController)
        {
            this.applicationDbContext = applicationDbContext;
            this.userOtpController = userOtpController; 

           
        }

        
        // GET: UserController
        public ActionResult Index()
        {
            var records= applicationDbContext.Users.Count();
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            //UserModel userModel = new UserModel();
            //userModel.Id = 1;
            //userModel.Email = "hello@gmail.com";
            //userDbContext.Users.Add(userModel);
            //userDbContext.SaveChanges();
            var userModel = new UserModel();
            userModel.CityList = GetCitites();
            return View(userModel);
        }

        private List<SelectListItem> GetCitites() 
        {
            var cities=new  List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ();
            cities.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = "Chennai", Value = "Chennai" });
            cities.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = "Hyderabad", Value = "Hyderabad" });
            cities.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem() { Text = "Bangalore", Value = "Bangalore" });
            return cities;
        }

        public ActionResult Login()
        {

            return View();
        }

        public ActionResult LogOut()
        {
            this.HttpContext.Session.Remove("isLoggedIn");
            this.HttpContext.Session.Remove("LoggedInUserName");

            return RedirectToAction("Login");
        }

        public ActionResult Welcome()
        {

            return View();
        }
        // POST: UserController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(UserModel userModel)
        {
            try
            {
                if (userModel.Id==0)
                {
                    userModel.GeneratedSalt= PasswordHasher.GenerateSalt();
                    userModel.Password = PasswordHasher.HashPassword(userModel.GeneratedSalt,userModel.Password);
                    this.applicationDbContext.Add(userModel);
                    this.applicationDbContext.SaveChanges();
                    var returnObject = new { success = true, loginName = userModel.LoginName };
                    this.userOtpController.GenerateOtp(userModel.LoginName);
                    // HttpContext.Session.SetString("loginName",userModel.LoginName);
                    this.Response.Cookies.Append("loginName", userModel.LoginName);
                    return new JsonResult(returnObject);
                }
                else
                {
                    var user=this.applicationDbContext.Users.Where(x => x.Id == userModel.Id).FirstOrDefault();
                    user.FirstName= userModel.FirstName;
                    user.LastName = userModel.LastName;
                    
                    user.LoginName = userModel.LoginName;
                    user.Email = userModel.Email;
                    this.applicationDbContext.SaveChanges();
                    var returnObject = new { success = true, loginName = userModel.LoginName };
                    //this.userOtpController.GenerateOtp(userModel.LoginName);
                    return new JsonResult(returnObject);
                }
               
            }
            catch(Exception ex)
            {
                var returnObject = new { success = false, loginName = userModel.LoginName,Message=ex.Message };
                return new JsonResult(returnObject);
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit()
        {
            this.TempData["Mode"] = "Edit";
            var userId=this.HttpContext.Session.GetInt32("userId");
            var user = this.applicationDbContext.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.CityList = GetCitites();
            return View("Create",user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Dashboard() 
        {
            if (this.HttpContext.Session.GetString("isLoggedIn") != "true")
                return RedirectToAction(nameof(Login));
            var users=this.applicationDbContext.Users.Select(x => x).ToList();
            return View(users);
        
        }
    }
}
