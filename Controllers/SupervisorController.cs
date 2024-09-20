using Microsoft.AspNetCore.Mvc;
using StudentAttendanceManagementSystem.Models;
using StudentAttendanceManagementSystem.Data;
using Microsoft.AspNetCore.Identity;
using StudentAttendanceManagementSystem.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;



namespace StudentAttendanceManagementSystem.Controllers
{
    public class SupervisorController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly UserManager <User > _userManager;
        private readonly SignInManager<User> signInManager;

        public SupervisorController(SignInManager<User> signInManager, UserManager <User> _userManager, ApplicationDbContext context){

        this.signInManager = signInManager;
        this._userManager = _userManager;
        this.context = context;
      

    }


        // GET: Register
        public IActionResult Register(string id)
        {
            ViewBag.id = id;
            return View();
        }

          public  IActionResult Index(string Id)
          {
               var user = context.ApplicationUsers.Where(d => d.UserName == Id).First();
                ViewBag.user = user;
                return View();
          }

        public async Task<IActionResult> List(string Id)
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

           public async Task<IActionResult> Create(SupervisorRegistration supervisor)
            {
        
            if (!ModelState.IsValid)  return View("Index", supervisor);
                User admin = context.ApplicationUsers.Where(d => d.UserName == supervisor.AdminId).First();
                
                var  user = new User{
                    Email = supervisor.Email,
                    UserName = supervisor.Email,
                    Address = supervisor.Address,
                    Company = supervisor.Phone,
                    PhoneNumber = supervisor.Phone,
                    FirstName = supervisor.FirstName,
                    LastName = supervisor.LastName
                }; //

          
        
            var result = await _userManager.CreateAsync(user, supervisor.Password);

            await _userManager.AddToRoleAsync(user, "Supervisor");
            var newUser = context.ApplicationUsers.Where(d => d.UserName == user.UserName).First();

            Following following  = new Following {
                Follower = admin.Id,
                Follow = newUser.Id
            };



            admin.Followings = [following];
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
             List<User> supervisor = context.ApplicationUsers.Where(d => d.UserName == Id).ToList();
            context.ApplicationUsers.Remove(supervisor.First());
            var follow =  context.Followings.Where(d => d.Follow == supervisor.First().Id).First();
            context.Followings.Remove(follow);
              
            

           
            if(supervisor.First().Complaints != null){
                foreach(var complaint in supervisor.First().Complaints ){
                 context.Complaints.Remove(complaint);
                }

            }

            if(supervisor.First().JobReports != null){
       
                foreach(var report in supervisor.First().JobReports ){
                    context.JobReports.Remove(report);
                
                }
            }

             if(supervisor.First().StudentReports != null){

                foreach(var report in supervisor.First().StudentReports ){
                    context.StudentReports.Remove(report);
                
                }
             }

            if(supervisor.First().Attendances != null){

            foreach(var attendance in supervisor.First().Attendances ){
                context.Attendances.Remove(attendance);
              
            }
            }

            List<Remark> remarks = context.Remarks.Include(e => e.StudentId).ToList();
            foreach(var remark in remarks){
                if(remark.StudentId == supervisor.First()){
                      context.Remarks.Remove(remark);
                }
            }
          
            context.SaveChanges();
            return "Success";
        }
    }
}