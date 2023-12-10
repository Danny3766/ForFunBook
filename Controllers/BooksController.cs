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


        [Route("[controller]/Show")]
        public IActionResult Show()
        {
            var books = _context.Books.ToList(); // 从数据库中检索所有书籍数据
            var booksViewModel = books.Select(book => new ForFunBook.Models.Book
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                // 其他属性...
            }).ToList();

            return View(booksViewModel); // 将正确类型的书籍数据传递给视图

            // var books = _context.Books.ToList(); // 从数据库中检索所有书籍数据
            // return View(books); // 将书籍数据传递给视图
            // return View();
        }

        // GET: Books/Create  取得空的 表單
        [Route("[controller]/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // 返回一個空的表單視圖
        }
        // Post: Books/Create 送出表單
        [Route("[controller]/Create")]
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
            return RedirectToAction(nameof(Show));
        }


        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error");
        // }
    }
}