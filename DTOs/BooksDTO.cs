using System.ComponentModel.DataAnnotations;

namespace BooksApp.DTOs
{
    public class BooksDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }
        public string Photo { get; set; }
        public List<GenreDTO> Genres { get; set; }

        public List<CommentDTO> Comments { get; set; }
    }
}