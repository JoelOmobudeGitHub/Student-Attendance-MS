using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendanceManagementSystem.Models;



namespace StudentAttendanceManagementSystem.Data;


public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // public virtual DbSet<Standard> Standards { get; set; }

    public DbSet<User> ApplicationUsers {get; set;} = null!;

    public DbSet<Attendance> Attendances {get; set;} = null!;

    public DbSet<Complaint> Complaints {get; set;} = null!;

    public DbSet<Following> Followings {get; set;} = null!;

    public DbSet<JobReport> JobReports {get; set;} = null!;

    public DbSet<Remark> Remarks {get; set;} = null!;

    public DbSet<StudentReport> StudentReports {get; set;} = null!;
        
    // public virtual DbSet<Organization> Organizations { get; set; }

    // public virtual DbSet<SupervisorViewModel> Supervisors { get; set; }
    
    // public virtual DbSet<StudentViewModel> Students { get; set; }

    // public virtual DbSet<ManageAttendanceViewModel> ManageAttendances { get; set;}

    // public virtual DbSet<ManageJobReportViewModel> ManageJobReports { get; set;}

    // public virtual DbSet<ManageComplaintViewModel> ManageComplaints { get; set; }



    // public virtual DbSet<AddFinalJobReportViewModel> AddFinalJobReport { get; set; }
    
    // public virtual DbSet<ChangePassword> ChangePasswords { get; set; }


    // public virtual DbSet<FillAttendanceViewModel> FillAttendances { get; set; }

    // public virtual DbSet<AddMonthlyJobReportViewModel> AddMonthlyJobReports { get; set; }

    // public virtual DbSet<DailyJobReportViewModel> DailyJobReports { get; set; }

    // public virtual DbSet<MakeComplaint> MakeComplaints { get; set; }

        
    
    // public virtual DbSet<LoginViewModel> Login { get; set; }

 
    
    // public virtual DbSet<RegisterViewModel> Register { get; set; }

    

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
       
        base.OnModelCreating(modelBuilder);
        
    }

}




