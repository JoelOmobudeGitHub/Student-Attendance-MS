using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace StudentAttendanceManagementSystem.Models.Authentication;

public class RemarkRegistration
{

    
   [Required]
   [StringLength(100)]
    public string Content {get; set;} = null!;

    public string StudentId {get; set;} = null!;

     public string SupervisorId {get; set;} = null!;

     

   


}

 
 
