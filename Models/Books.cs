using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}


