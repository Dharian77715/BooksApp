using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Photo { get; set; }
    }
}


