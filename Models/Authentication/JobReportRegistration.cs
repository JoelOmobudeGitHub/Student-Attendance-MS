using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace StudentAttendanceManagementSystem.Models.Authentication;

public class JobReportRegistration
{

    
   [Required]
   [StringLength(100)]
    public string Content {get; set;} = null!;

        
   [Required]
   [StringLength(100)]
    public string SupervisorId {get; set;} = null!;

     

   


}

 
 
