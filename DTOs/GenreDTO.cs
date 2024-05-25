using System.ComponentModel.DataAnnotations;

namespace BooksApp.DTOs
{
    public class GenreDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


    }
}