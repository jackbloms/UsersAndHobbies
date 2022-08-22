#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BeltExam.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "First name must be at least 2 chars long")]
    [Display(Name = "First Name:")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "Last name must be at least 2 chars long")]
    [Display(Name = "Last Name:")]
    public string LastName { get; set; }

    //do duplicate val in UserController Register Method
    [Required(ErrorMessage = "is required")]
    [MinLength(3, ErrorMessage = "Username must be at least 3 chars long")]
    [MaxLength(15, ErrorMessage = "Username must not exceed 15 chars")]
    [Display(Name = "Username:")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 chars long")]
    [DataType(DataType.Password)]
    [Display(Name = "Password:")]
    public string Password { get; set; }

    [NotMapped]
    [Compare("Password", ErrorMessage = "doesn't match password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm PW:")]
    public string ConfirmPassword { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public List<Association> Hobbies { get; set; } = new List<Association>();

}