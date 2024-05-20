using System.ComponentModel.DataAnnotations;

namespace BooksApp.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

    }
}