using System.ComponentModel.DataAnnotations;

namespace BooksApp.DTOs
{
    public class CreateGenreDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

    }
}