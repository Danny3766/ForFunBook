using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForFunBook.Models;
using ForFunBook.Context;


namespace ForFunBook.Controllers
{
    [Route("[controller]")]
    public class BooksController : Controller
    {


         private readonly ApplicationDbContext _context;

         public BooksController(ApplicationDbContext context)
         {
             _context = context;
         }


        // private readonly ILogger<HomeController> _logger;

        // public BooksController(ILogger<HomeController> logger)
        // {
        //     _logger = logger;
        // }   

        public IActionResult Show()
        {
            return View();
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (book == null)
            {
                return BadRequest(); 
            }

            if (!ModelState.IsValid)
            {
                return View(book);
            }

            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}