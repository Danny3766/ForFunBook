using System;
using System.Collections.Generic;

namespace ForFunBook
{
    public partial class Book
    {
        public long BookId { get; set; } 
        public string? Title {get; set; } = null;
        public string? Category { get; set; } = null;
        public string? Author { get; set; } = null;
    }
}
