using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Models;
using StudentAttendanceManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using StudentAttendanceManagementSystem.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



namespace StudentAttendanceManagementSystem.Controllers
{
    public class StudentReportController : Controller
    {

          private readonly ApplicationDbContext context;
          private readonly SignInManager<User> signInManager;
          private readonly UserManager <User> _userManager;

        public StudentReportController(SignInManager<User> signInManager, UserManager <User> _userManager, ApplicationDbContext context){

        this.signInManager = signInManager;
        this._userManager = _userManager;
        this.context = context;
      

        }

        

      

          public  IActionResult Index(string Id)
          {
                ViewBag.id = Id;
                return View();
          }

            // role middleware
            public async Task<IActionResult> Create(StudentReportRegistration registration)
            {
        
            if (!ModelState.IsValid)  return View("Index", registration.StudentId);
               
               User student = context.ApplicationUsers.Where( d => d.UserName == registration.StudentId).First();
               StudentReport report = new StudentReport {
                    TimeStamp = DateTime.Now, 
                    Content = registration.Content,
               };

               student.StudentReports = [report];
               context.StudentReports.Add(report);
               context.SaveChanges();
               

            return RedirectToAction("Index", "Student", new {id=student.Email});
        
        }

       public async Task <IActionResult> View(int Id)
        {
            StudentReport report = context.StudentReports.Where( d => d.Id == Id).First();
            ViewBag.studentReport = report;
            ViewBag.id = Id;
            return View();

        }

         public async Task <IActionResult> List(string Id)
         {
            User supervisor = context.ApplicationUsers.Where( d => d.UserName == Id).Include(e => e.StudentReports).First();
            var studentReports = supervisor.StudentReports;
            ViewBag.studentReports = studentReports;
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