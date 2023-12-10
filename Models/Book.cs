using System.ComponentModel.DataAnnotations;

namespace ForFunBook.Models;

public class Book
{
    [Key]
    public long BookId { get; set; }
    public string? Title { get; set; } = null;
    public string? Category { get; set; } = null;
    public string? Author { get; set; } = null;
}