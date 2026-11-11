using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsWithRouting.Models;
using System.Diagnostics;
using ProductsWithRouting.Services;

namespace ProductsWithRouting.Controllers
{
    public class UsersController : Controller
    {
        private List<User> myUsers;

        public UsersController(Data data)
        {
            myUsers = data.Users;
        }

        [HttpPost]
        [Route("users/{id?}")]
        public IActionResult Index(int id, [FromForm] string token)
        {
            if(token == "df2323eoT")
            {
                ViewBag.Id = id;
                return View(myUsers);
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("users/{id?}")]
        public IActionResult Index()
        {
            return View("Admin");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
