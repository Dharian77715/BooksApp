using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

    }
}


