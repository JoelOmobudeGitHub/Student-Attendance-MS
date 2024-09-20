using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Models;
using StudentAttendanceManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using StudentAttendanceManagementSystem.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



namespace StudentAttendanceManagementSystem.Controllers
{
    public class RemarkController : Controller
    {

          private readonly ApplicationDbContext context;
          private readonly SignInManager<User> signInManager;
          private readonly UserManager <User> _userManager;

        public RemarkController(SignInManager<User> signInManager, UserManager <User> _userManager, ApplicationDbContext context){

        this.signInManager = signInManager;
        this._userManager = _userManager;
        this.context = context;
      

        }


        public async Task<IActionResult> StudentView(string Id)
        {
           List<User> students = context.ApplicationUsers.Where(d => d.UserName == Id).Include(e => e.Followings).ToList();
               var followings = students[0].Followings;
               List<User> users = new List<User>(); 

              foreach(var following in followings)
              {
                User user = context.ApplicationUsers.Where(d => d.Id == following.Follower).First();
                var roles = await _userManager.GetRolesAsync(user);

                if(roles.Contains("Student")){
                  users.Add(user);
                }
              }

              ViewBag.users = users;
              ViewBag.id = Id;
              
               return View();
        }

        

      

          public async Task<IActionResult> Index(string Id)
          {
              User student = context.ApplicationUsers.Where(d => d.UserName == Id).First();
              Following following = context.Followings.Where( d => d.Follower == student.Id).First();
              var supervisorId = following.Follow;
              User user = context.ApplicationUsers.Where(d => d.Id == supervisorId).First();

            
               ViewBag.user = user;
               ViewBag.id = Id;
                return View();
          }

            // role middleware
            public async Task<IActionResult> Create(RemarkRegistration registration)
            {
        
            if (!ModelState.IsValid)   return RedirectToAction("Index", "StudentView", new {id=registration.StudentId});
               
               User student = context.ApplicationUsers.Where( d => d.UserName == registration.StudentId).First();
              
               Remark remark = new Remark {
                    TimeStamp = DateTime.Now,
                    Content = registration.Content,
               };

               remark.StudentId= student;
               context.Remarks.Add(remark);
               context.SaveChanges();
               

            return RedirectToAction("Index", "Supervisor", new {id=registration.SupervisorId});
        
        }

      public async Task <IActionResult> View(string Id)
        {
            User student = context.ApplicationUsers.Where(d => d.UserName == Id).First();
            List<Remark> remarks = context.Remarks.ToList();
            foreach(var remark in remarks ){
              if(remark.StudentId == student){
                ViewBag.remark = remark;
                break;
              }
            }

            ViewBag.id = Id;
           
            return View();

        }

       

           public async Task<IActionResult> AdminSupervisorList(string Id)
           {
                List<User> admins = context.ApplicationUsers.Where(d => d.UserName == Id).Include(e => e.Followings).ToList();
                var followings = admins[0].Followings;
                List<User> users = new List<User>(); 

                foreach(var following in followings)
                {
                  ViewBag.id = Id;
                  var _user = context.ApplicationUsers.Where(d => d.Id == following.Follow);
                  if(_user.IsNullOrEmpty()){

                    ViewBag.users = users;
                    return View();
                  }
                  User user = _user.First();
                  var roles = await _userManager.GetRolesAsync(user);

                  if(roles.Contains("Supervisor")){
                    users.Add(user);
                  }
                }
                ViewBag.users = users;
              
                
                return View();
            }

              public async Task<IActionResult> AdminStudentList(string Id)
              {
                 ViewBag.id = Id;
                List<User> admins = context.ApplicationUsers.Where(d => d.UserName == Id).Include(e => e.Followings).ToList();
                var followings = admins[0].Followings;
                List<User> users = new List<User>(); 

                foreach(var following in followings)
                {
                  var _user = context.ApplicationUsers.Where(d => d.Id == following.Follower);
                  if(_user.IsNullOrEmpty()){

                    ViewBag.users = users;
                    return View();
                  }
                  User user = _user.First();
                  var roles = await _userManager.GetRolesAsync(user);

                  if(roles.Contains("Student")){
                    users.Add(user);
                  }
                }

                ViewBag.users = users;
               
                
                return View();
            }
    }
}