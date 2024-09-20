using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StudentAttendanceManagementSystem.Models
{
    public class Following
    {
         // Primary Key

        public int Id {get; set;}

        public string? Follower {get; set;}

        public string? Follow {get; set;}
       

        // Other properties
    }
}
