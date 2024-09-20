using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Models;
using StudentAttendanceManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceManagementSystem.Models.Authentication;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;





namespace StudentAttendanceManagementSystem.Controllers
{
    public class AttendanceController : Controller
    {

          private readonly ApplicationDbContext context;
          private readonly SignInManager<User> signInManager;
          private readonly UserManager <User> _userManager;

        public AttendanceController(SignInManager<User> signInManager, UserManager <User> _userManager, ApplicationDbContext context){

        this.signInManager = signInManager;
        this._userManager = _userManager;
        this.context = context;
      

        }

        

      

          public async Task<IActionResult> Index(string Id)
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

   
            public async Task<JsonResult> Create(IFormCollection attendanceForm)
            {
              var supervisorId =  JsonConvert.DeserializeObject<string>(attendanceForm["supervisorId"]);
                

              List<User> allStudents = context.ApplicationUsers.Where(d => d.UserName == supervisorId).Include(e => e.Followings).ToList();

               var followings = allStudents[0].Followings;

               List<User> users = new List<User>(); 
               List<User> absents = new List<User>(); 

              foreach(var following in followings)
              {
                User user = context.ApplicationUsers.Where(d => d.Id == following.Follower).First();
                var roles = await _userManager.GetRolesAsync(user);

                if(roles.Contains("Student")){
                  users.Add(user);
                }
              }

              absents = users;

            
              
       
               var usersId = JsonConvert.DeserializeObject<string[]>(attendanceForm["StudentPresent"]);
               List<User> students = new List<User>();

               
               List<User> students2 = new List<User>();

               foreach(var userId in usersId){
                  User user = context.ApplicationUsers.Where(d => d.UserName == userId).First();
                  students.Add(user);
               }


                foreach(User allStudent in users.ToList()){
                   
                    foreach(User student in students){
                         if(allStudent == student){
                           absents.Remove(allStudent);
                        }
                  }
              
               }
              
               Attendance attendance = new Attendance {
                    TimeStamp = DateTime.Now,
                    Status = "Present",
                   
              };

              Attendance absAttendance = new Attendance {
                 TimeStamp = DateTime.Now,
                  Status = "Absent",
              };

              attendance.Users = students;
              context.Attendances.Add(attendance);
              context.SaveChanges();

              absAttendance.Users = absents;
              context.Attendances.Add(absAttendance);
              context.SaveChanges();
               
              return Json("Successful");
        
        }

        public async Task <IActionResult> List(string Id)
        {
            var _student = context.ApplicationUsers.Where( d => d.UserName == Id).Include(e => e.Attendances);
            if(_student.IsNullOrEmpty()){
              return View();
            }
           User student = _student.First();
            List<Attendance> attendances = student.Attendances;
            ViewBag.attendances = attendances;
            return View();

        }

         public async Task<IActionResult> AdminSupervisorList(string Id)
           {
                 ViewBag.id = Id;
                List<User> admins = context.ApplicationUsers.Where(d => d.UserName == Id).Include(e => e.Followings).ToList();
                var followings = admins[0].Followings;
                List<User> users = new List<User>(); 

                foreach(var following in followings)
                {
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