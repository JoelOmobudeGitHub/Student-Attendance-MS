using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentAttendanceManagementSystem.Models
{
    public class Complaint
    {
         // Primary Key
        public int? Id { get; set; }
        public DateTime TimeStamp { get; set; }
   
        public string? Message  {get; set;}
        

        // Other properties
    }
}
