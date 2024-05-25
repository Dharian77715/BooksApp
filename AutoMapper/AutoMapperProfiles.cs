
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
            CreateMap<CreateBookDTO, Book>()
            .ForMember(b => b.Photo, options => options.Ignore())
            .ForMember(b => b.BooksGenres, options => options.MapFrom(MapBooksGenres));

            CreateMap<Author, AuthorsDTO>().ReverseMap();
            CreateMap<CreateAuthorDTO, Author>().ForMember(a => a.Photo, options => options.Ignore());

            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<CreateGenreDTO, Genre>();

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();

        }

        private List<BooksGenres> MapBooksGenres(CreateBookDTO CreateBookDTO, Book book)
        {
            var result = new List<BooksGenres>();
            // if (CreateBookDTO.GenresIds == null) { return result; }
            foreach (var id in CreateBookDTO.GenresIds)
            {
                result.Add(new BooksGenres() { GenreId = id });
            }

            return result;
        }

    }


}