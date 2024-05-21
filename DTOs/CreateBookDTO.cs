using System.ComponentModel.DataAnnotations;
using BooksApp.Validations;

namespace BooksApp.DTOs
{
    public class CreateBookDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        [ValidateImage(ValidFileTypes: ValidFileTypes.Image)]
        public IFormFile Photo { get; set; }

    }
}