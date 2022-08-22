using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers;

public class HobbyController : Controller
{
    private MyContext _context;

    public HobbyController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("/hobbies/new")]
    public IActionResult NewHobby()
    {
        return View("NewHobby");
    }

    [HttpPost("/hobbies/create")]
    public IActionResult CreateHobby(Hobby newHobby)
    {
        if(ModelState.IsValid == false)
        {
            return NewHobby();
        }

        newHobby.UserId = (int)HttpContext.Session.GetInt32("UUID");
        _context.Hobbies.Add(newHobby);
        _context.SaveChanges();

        return RedirectToAction("Dashboard", "User");
    }

    [HttpGet("/hobbies/{hId}")]
    public IActionResult DetailPage(int hId)
    {
        Hobby? hob = _context.Hobbies
        .Include(h=>h.Users)
        .ThenInclude(h=>h.User)
        .FirstOrDefault(h=>h.HobbyId == hId);

        List<Association> ass = _context.Associations
        .Include(a=>a.User)
        .Where(a=>a.HobbyId == hId)
        .ToList();

        ViewBag.ass = ass;

        if(hob == null)
        {
            return RedirectToAction("Dashboard", "User");
        }

        return View("DetailPage", hob);
    }

    [HttpPost("/hobbies/enthusiast/{hId}")]
    public IActionResult Enthusiast(int hId)
    {
        Association newAss = new Association(){
            UserId = (int)HttpContext.Session.GetInt32("UUID"),
            HobbyId = hId
        };

        _context.Associations.Add(newAss);
        _context.SaveChanges();

        return DetailPage(hId);
    }

    [HttpPost("/hobbies/unlike/{hId}")]
    public IActionResult Unlike(int hId)
    {
        Association? delAss = _context.Associations
        .Include(a=>a.Hobby)
        .FirstOrDefault(a=>a.HobbyId == hId);

        _context.Associations.Remove(delAss);
        _context.SaveChanges();

        return DetailPage(hId);
    }

}