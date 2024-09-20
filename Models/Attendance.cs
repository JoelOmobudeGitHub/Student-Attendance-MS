using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentAttendanceManagementSystem.Models
{
    public class Attendance
    {
         // Primary Key
        public int? Id { get; set; }
        public DateTime? TimeStamp { get; set; }
    
        public string Status {get; set;} = "Absent";

        public List<User> Users {get; set;} = null!;
        

        // Other properties
    }
}
