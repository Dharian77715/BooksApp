using System.ComponentModel.DataAnnotations;
using BooksApp.Helpers;
using BooksApp.Validations;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.DTOs
{
    public class CreateBookDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        [ValidateImage(ValidFileTypes: ValidFileTypes.Image)]
        public IFormFile Photo { get; set; }

        // [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }

    }
}