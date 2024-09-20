using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace StudentAttendanceManagementSystem.Models.Authentication;

public class PasswordForm
{

    
   [Required]
   [StringLength(100)]
    public string CurrentPassword {get; set;} = null!;

    public string NewPassword {get; set;} = null!;

   [Compare("NewPassword", ErrorMessage ="Confirm password doesn't match, Type again !")]
   public string ConfirmPassword {get; set;} = null!;

   public string SupervisorId {get; set;} = null!;

     

   


}

 
 
