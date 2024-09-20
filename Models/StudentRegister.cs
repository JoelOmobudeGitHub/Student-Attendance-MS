
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;



namespace StudentAttendanceManagementSystem.Models{
    
}
public class StudentRegister{
    
   [Required]
   [StringLength(100)]
    public string CompanyName {get; set;} = null!;

    
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
    public string Website {get; set;} = null!;

    [Required]
    [StringLength(100)]

    public string? UserName {get; set;}
    
    
    [Required]
    
    public string Password {get; set;} = null!;
}