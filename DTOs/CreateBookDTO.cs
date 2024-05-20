using System.ComponentModel.DataAnnotations;

namespace BooksApp.DTOs
{
    public class CreateBookDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Title { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}