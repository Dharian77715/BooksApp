using System.ComponentModel.DataAnnotations;
using BooksApp.Validations;

namespace BooksApp.DTOs
{
    public class CreateAuthorDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ValidateImage(ValidFileTypes: ValidFileTypes.Image)]
        public IFormFile Photo { get; set; }

    }
}