using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;

namespace BeltExam.Controllers;

public class HomeController : Controller
{

    public IActionResult Privacy()
    {
        return View();
    }

}
