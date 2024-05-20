using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

    }
}


