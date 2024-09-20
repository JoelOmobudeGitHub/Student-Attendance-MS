using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace StudentAttendanceManagementSystem.Models.Authentication;

public class StudentRegistration
{
   


   [Required]
   [StringLength(100)]
    public string CompanyName {get; set;} = null!;

    public string AdminId {get; set;} = null!;

    public string SupervisorId {get; set;} = null!;

    
   [Required]
   [StringLength(100)]
    public string FirstName {get; set;} = null!;

   [Required]
   [StringLength(100)]
    public string LastName {get; set;} = null!;

    
   [Required]
   [StringLength(100)]
    public string Email {get; set;} = null!;

    
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "* Part numbers must be 11 character in length.")]
    public string Phone {get; set;} = null!;

    [Required]
    [StringLength(100)]
    public string Address {get; set;} = null!;

  

    [Required]
    [StringLength(100)]

    public string? UserName {get; set;}
    
    
    [Required]
    
    public string Password {get; set;} = null!;
    
   


}

 
 
