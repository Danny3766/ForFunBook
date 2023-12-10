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

        // 顯示 book 清單 頁面
        [Route("[controller]/Show")]
        public IActionResult Show()
        {
            var books = _context.Books.OrderByDescending(book => book.BookId).ToList();
            var booksShowModel = books.Select(book => new ForFunBook.Models.Book
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                Category = book.Category,
            }).ToList();

            return View(booksShowModel);
           
        }

        // 新增 book 頁面
        [Route("[controller]/Create")]  // GET: Books/Create  取得空的 表單
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


        // 編輯 book 功能
        [HttpGet]
        [Route("[controller]/Edit/{id}")]  // 取得 book 的編輯頁面
        public IActionResult Edit(long id)
        {
            var book = _context.Books.Find(id);
            var bookEditModel = new ForFunBook.Models.Book
            {
                BookId = book.BookId,
                Title = book.Title,
                Author = book.Author,
                Category = book.Category,
            };
            if (book == null)
            {
                return NotFound();
            }

            return View(bookEditModel);
        }
        // 送出 編輯 book 後的 表單
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // [Route("[controller]/Edit")]
        // public IActionResult Edit(Book book)
        // {


        //     if (book == null)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Books.Update(book);
        //     _context.SaveChanges();

        //     return RedirectToAction(nameof(Show));
        // }



        // 送出編輯book的表單
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/Edit/{id}")]
        public IActionResult Edit(long id, [Bind("BookId, Title, Author, Category")] Book book)
        {
            var book_submit = _context.Books.Find(id);

            if (book_submit == null)
            {
                return BadRequest();
            }
            // 將接收到的 book 對象的更改放到 book_submit 中
            book_submit.Title = book.Title;
            book_submit.Author = book.Author;
            book_submit.Category = book.Category;

            _context.Books.Update(book_submit);
            _context.SaveChanges();

            var bookEditModel = new ForFunBook.Models.Book
            {
                BookId = book_submit.BookId,
                Title = book_submit.Title,
                Author = book_submit.Author,
                Category = book_submit.Category,
            };
            return RedirectToAction(nameof(Show));
        }



        // 刪除 book 功能
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("[controller]/Cancel/{id}")]
        public IActionResult Cancel(long id)
        {
            var book = _context.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Show));
        }

    }


}