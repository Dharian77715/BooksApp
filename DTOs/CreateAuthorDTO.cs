using System.ComponentModel.DataAnnotations;

namespace BooksApp.DTOs
{
    public class CreateAuthorDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}