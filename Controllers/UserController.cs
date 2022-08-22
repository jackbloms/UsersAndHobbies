using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers;

public class UserController : Controller
{
    private MyContext _context;

    public UserController(MyContext context)
    {
        _context = context;
    }

    [HttpGet("/")]
    public IActionResult LogAndReg()
    {
        return View("LogAndReg");
    }

    [HttpGet("/hobbies")]
    public IActionResult Dashboard()
    {
        List<Hobby> AllHobbies = _context.Hobbies
        .Include(h=>h.Users)
        .ThenInclude(h=>h.User)
        .ToList();

        return View("Dashboard", AllHobbies);
    }

    [HttpPost("/register")]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid)
        {
            if(_context.Users.Any(u => u.UserName == newUser.UserName))
            {
                ModelState.AddModelError("UserName", "is taken");
            }
        }

        if(ModelState.IsValid == false)
        {
            return LogAndReg();
        }

        PasswordHasher<User> hashBrowns = new PasswordHasher<User>();
        newUser.Password = hashBrowns.HashPassword(newUser, newUser.Password);

        _context.Users.Add(newUser);
        _context.SaveChanges();

        HttpContext.Session.SetInt32("UUID", newUser.UserId);

        return RedirectToAction("Dashboard");
    }

    [HttpPost("/login")]
    public IActionResult Login(LoginUser loginUser)
    {
        User? dbUser = _context.Users.FirstOrDefault(u=>u.UserName == loginUser.LoginUsername);

        if(dbUser == null)
        {
            ModelState.AddModelError("LoginUsername", "Not found");
            return LogAndReg();
        }

        PasswordHasher<LoginUser> hashBrowns = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwResult = hashBrowns.VerifyHashedPassword
        (loginUser, dbUser.Password, loginUser.LoginPassword);

        if(pwResult == 0)
        {
            ModelState.AddModelError("LoginPassword", "is not correct");
            return LogAndReg();
        }

        HttpContext.Session.SetInt32("UUID", dbUser.UserId);
        return Dashboard();
    }

    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return View("LogAndReg");
    }

}