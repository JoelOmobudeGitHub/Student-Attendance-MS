using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentAttendanceManagementSystem.Models
{
    public class StudentReport
    {
         // Primary Key
        public int? Id { get; set; }

        public DateTime TimeStamp {get; set;}
        public string? Content { get; set; }
   
        

        // Other properties
    }
}
