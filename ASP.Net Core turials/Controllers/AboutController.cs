﻿using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_turials.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
