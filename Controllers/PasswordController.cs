using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Models;
using StudentAttendanceManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceManagementSystem.Models.Authentication;



namespace StudentAttendanceManagementSystem.Controllers
{
    public class PasswordController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly SignInManager<User> signInManager;
          private readonly UserManager <User> _userManager;

        public PasswordController(SignInManager<User> signInManager, UserManager <User> _userManager, ApplicationDbContext context){

        this.signInManager = signInManager;
        this._userManager = _userManager;
        this.context = context;
      

    }


    

        public async Task<IActionResult> Index(string Id)
        {
            ViewBag.id = Id;
            return View();
        }

        public async Task<IActionResult> Update(PasswordForm model)
        {
            if (!ModelState.IsValid)  return View("Error");
            var user = context.ApplicationUsers.Where(d => d.UserName == model.SupervisorId).First();
           IdentityResult result =await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
           if(result.Succeeded){
             return RedirectToAction("Index", "Supervisor", new {Id = model.SupervisorId});
           }
            return RedirectToAction("Index", "Password", new {});
          
        }
            
       
    }
}