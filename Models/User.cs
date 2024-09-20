using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentAttendanceManagementSystem.Models
{
    public class User : IdentityUser
    {
         // Primary Key
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address {get; set;}

        public string? Company {get; set;} = null!;

        public List<Attendance> Attendances {get; set;} = null!;

        public List<Complaint> Complaints {get; set;}

        public List<Following> Followings {get; set;} = null!;

        public List<JobReport> JobReports {get; set;} = null!;

    
        public List<StudentReport> StudentReports {get; set;} = null!;



       


        


        // Other properties
    }
}
