using System;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Photo { get; set; }
        public int SexId { get; set; }
        public Sex Sex { get; set; }

        public string SexName
        {
            get
            {

                if (SexId == 1)
                {
                    return "Masculino";
                }
                else if (SexId == 2)
                {
                    return "Femenino";
                }
                else
                {
                    return "indefinido";
                }

            }
        }
    }
}


