using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Models;
using StudentAttendanceManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using StudentAttendanceManagementSystem.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



namespace StudentAttendanceManagementSystem.Controllers
{
    public class StudentController : Controller
    {

          private readonly ApplicationDbContext context;
          private readonly SignInManager<User> signInManager;
          private readonly UserManager <User> _userManager;

        public StudentController(SignInManager<User> signInManager, UserManager <User> _userManager, ApplicationDbContext context){

        this.signInManager = signInManager;
        this._userManager = _userManager;
        this.context = context;
      

        }

          public async Task<IActionResult> SupervisorStudentList(string Id)
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


        

        public async Task<IActionResult> Register(string id)
        {
            List<User> users = context.ApplicationUsers.ToList();
            List<User> supervisors = new List<User>();
          
            foreach(var user in users){
                var roles = await _userManager.GetRolesAsync(user);
                if(roles.Contains("Supervisor")){
                    supervisors.Add(user);
                }
            }

            ViewBag.supervisors = supervisors;
            ViewBag.id = id;
            return View();
        }

          public  IActionResult Index(string Id)
          {
               var user = context.ApplicationUsers.Where(d => d.UserName == Id).First();
               
                ViewBag.user = user;
                return View();
          }

        public async Task <IActionResult> List(string Id)
        {
             List<User> admins = context.ApplicationUsers.Where(d => d.UserName == Id).Include(e => e.Followings).ToList();
                var followings = admins[0].Followings;
                List<User> users = new List<User>(); 

                foreach(var following in followings)
                {
                  var _user = context.ApplicationUsers.Where(d => d.Id == following.Follower);
                  if(_user.IsNullOrEmpty()){
                   ModelState.AddModelError("Title", "database error");
                  return View();
                  }

                    User user = _user.First();
                    var roles = await _userManager.GetRolesAsync(user);
                    
                    if(roles.Contains("Student")){
                    users.Add(user);
                    }

                 
                }

                ViewBag.users = users;
                ViewBag.id = Id;
                
                return View();
        }
            public async Task<IActionResult> Create(StudentRegistration student)
            {

            User admin = context.ApplicationUsers.Where(d => d.UserName == student.AdminId).First();
            User supervisor = context.ApplicationUsers.Where(d => d.UserName == student.SupervisorId ).First();
            
        
            if (!ModelState.IsValid)  return View("Index", student);

                var  user = new User{
                
                    Email = student.Email,
                    UserName = student.Email,
                    PhoneNumber = student.Phone,
                    Address = student.Address,
                    Company = student.CompanyName,
                    FirstName = student.FirstName,
                    LastName = student.LastName

                    
                }; //

        
            var result = await _userManager.CreateAsync(user, student.Password);

            await _userManager.AddToRoleAsync(user, "Student");
          
            Following following  = new Following {
                Follower = user.Id,
                Follow = supervisor.Id
            };
            
            supervisor.Followings = [following];

            context.Followings.Add(following);
            context.SaveChanges();

            if (!result.Succeeded) {
                foreach(var item in result.Errors) {
                    ModelState.AddModelError("Password", item.Description);
                    
                }

                return View("Index");
        
            }

            return RedirectToAction("Index", "Admin", new {id=admin.Email});
        
        }

        public string Delete(string Id)
        {
            List<User> student = context.ApplicationUsers.Where(d => d.UserName == Id).Include(e => e.Followings).ToList();
            context.ApplicationUsers.Remove(student.First());
             var follow =  context.Followings.Where(d => d.Follower == student.First().Id).First();
            context.Followings.Remove(follow);
              
            
             if(student.First().Complaints != null){
                foreach(var complaint in student.First().Complaints ){
                context.Complaints.Remove(complaint);
                }

            }

            if(student.First().JobReports != null){
       
                foreach(var report in student.First().JobReports ){
                    context.JobReports.Remove(report);
                
                }
            }

             if(student.First().StudentReports != null){

                foreach(var report in student.First().StudentReports ){
                    context.StudentReports.Remove(report);
                
                }
             }

            if(student.First().Attendances != null){

            foreach(var attendance in student.First().Attendances ){
                context.Attendances.Remove(attendance);
              
            }
            }


            List<Remark> remarks = context.Remarks.Include(e => e.StudentId).ToList();
            foreach(var remark in remarks){
                if(remark.StudentId == student.First()){
                      context.Remarks.Remove(remark);
                }
            }

            context.SaveChanges();
            return "Success";
            
        }
    }
}