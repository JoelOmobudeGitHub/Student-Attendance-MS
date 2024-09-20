using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Models;
using StudentAttendanceManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace StudentAttendanceManagementSystem.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager <User> _userManager;

        public AdminController(SignInManager<User> signInManager, UserManager <User> _userManager, ApplicationDbContext context){

        this.signInManager = signInManager;
        this._userManager = _userManager;
        this.context = context;
      

    }
    

         // GET: Admin
        public  IActionResult Index(string Id)
        {
            var user = context.Users.Where(d => d.UserName == Id).First();
            ViewBag.user = user;
            return View();
    
        }

          
        public IActionResult List(string Id)
        {
           List<User> supervisors = context.ApplicationUsers.Where(d => d.UserName == Id).Include(e => e.Followings).ToList();
           ViewBag.supervisors = supervisors[0].Followings;
           return View();
        }




          // POST: Admin/
          public async Task<IActionResult> Login(LoginViewModel model)
          {
             if(ModelState.IsValid){
            // confirm if employer credential matches the one in the record

                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent:true, lockoutOnFailure:true);
                if(result.Succeeded){  

                    var user = await _userManager.FindByNameAsync(model.Username);
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin", new {id=user.UserName} );
                    }
                       else if (roles.Contains("Supervisor"))
                    {
                        return RedirectToAction("Index", "Supervisor", new {id=user.UserName});
                    }
                    else if (roles.Contains("Student"))
                    {
                        return RedirectToAction("Index", "Student", new {id=user.UserName});
                    }

                }

                    ModelState.AddModelError("Password", "Credentials does not match");
                    
            
          }

                return RedirectToAction("Index", "Home");
          }
            
       
    }
}