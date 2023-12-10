using System.ComponentModel.DataAnnotations;

namespace ForFunBook.Models;

public class Book
{
    [Key]
    public long BookId { get; set; }
    public string Title { get; set; } 
    public string Category { get; set; } 
    public string Author { get; set; } 
}