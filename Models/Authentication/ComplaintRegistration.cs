using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace StudentAttendanceManagementSystem.Models.Authentication;

public class ComplaintRegistration
{
   

    
   [Required]
   [StringLength(100)]
   

     public string StudentUserName {get; set;} = null!;

      public string Message {get; set;} = null!;

   


}

 
 
