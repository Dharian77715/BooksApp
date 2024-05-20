
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
            CreateMap<Book, CreateBookDTO>();

            CreateMap<Author, AuthorsDTO>().ReverseMap();
            CreateMap<Author, CreateAuthorDTO>();

            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<Genre, CreateGenreDTO>();

            // CreateMap<MovieGenre, MovieGenreDTO>();



            //Dto to Domain

            // CreateMap<BooksDTO, Book>();
            // CreateMap<AuthorsDTO, Author>();
            // CreateMap<Actor, MovieActorDTO>();

            // CreateMap<MovieGenreDTO, MovieGenre>().ForMember(m => m.ID, opt => opt.Ignore());

        }

    }


}