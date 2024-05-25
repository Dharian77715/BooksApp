using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models
{
    public class BooksGenres
    {
        public int GenreId { get; set; }

        public int BookId { get; set; }

        public Genre Genre { get; set; }
        public Book Book { get; set; }
    }
}


