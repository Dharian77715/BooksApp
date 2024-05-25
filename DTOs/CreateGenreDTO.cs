using System.ComponentModel.DataAnnotations;

namespace BooksApp.DTOs
{
    public class CreateGenreDTO
    {

        [Required]
        public string Name { get; set; }

    }
}