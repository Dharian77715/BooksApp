
using AutoMapper;
using BooksApp.DTOs;
using BooksApp.Models;

namespace BooksApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()

        {
            CreateMap<Book, BooksDTO>().ReverseMap();
            CreateMap<CreateBookDTO, Book>().ForMember(b => b.Photo, options => options.Ignore());

            CreateMap<Author, AuthorsDTO>().ReverseMap();
            CreateMap<CreateAuthorDTO, Author>().ForMember(a => a.Photo, options => options.Ignore());

            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<CreateGenreDTO, Genre>();


        }

    }


}