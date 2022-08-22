#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace BeltExam.Models;

public class Hobby
{
    [Key]
    public int HobbyId { get; set; }

    [Required(ErrorMessage = "is required")]
    [Display(Name = "Name:")]
    public string Name { get; set; }

    [Required(ErrorMessage = "is required")]
    [Display(Name = "Description:")]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int UserId { get; set; }
    public User? Person { get; set; }
    public List<Association> Users { get; set; } = new List<Association>();

}