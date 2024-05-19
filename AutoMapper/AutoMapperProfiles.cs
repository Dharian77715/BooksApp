
using AutoMapper;
using BooksApp.DTOs;
using BooksApp.Models;

namespace BooksApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()

        {
            // domain to Dto

            CreateMap<Book, BooksDTO>();
            CreateMap<Author, AuthorsDTO>();
            // CreateMap<MovieGenre, MovieGenreDTO>();



            //Dto to Domain

            CreateMap<BooksDTO, Book>();
            CreateMap<AuthorsDTO, Author>();
            // CreateMap<Actor, MovieActorDTO>();

            // CreateMap<MovieGenreDTO, MovieGenre>().ForMember(m => m.ID, opt => opt.Ignore());

        }

    }


}