using System;
using System.ComponentModel.DataAnnotations;




namespace BooksApp.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<BooksGenres> BooksGenres { get; set; }


    }
}


