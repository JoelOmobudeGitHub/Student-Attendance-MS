using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace StudentAttendanceManagementSystem.Models.Authentication;

public class AttendanceRegistration
{
   

    
   
     [Required]
    public List<User> StudentPresent {get; set;} = null!;

      

    
    [Required]
    public string SupervisorId {get; set;} = null!;


   


}

 
 
