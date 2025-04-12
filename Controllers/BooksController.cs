using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using book_diplom2.Models;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult GetBooks(int userId)
    {
        var books = _context.UserBooks
            .Where(ub => ub.UserId == userId)
            .Join(_context.Books,
                ub => ub.BookId,
                b => b.BookId,
                (ub, b) => new { b.Title, b.Note, ub.RatingValue })
            .ToList();
        //Response.ContentType = "application/json; charset=utf-8";
        return Ok(books);
    }

    [HttpPost]
    public ActionResult AddBook([FromBody] Book book, int userId)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
        var userBook = new UserBook { UserId = userId, BookId = book.BookId, RatingValue = null };
        _context.UserBooks.Add(userBook);
        _context.SaveChanges();
        return Ok(new { Message = "Книга добавлена" });
    }
}