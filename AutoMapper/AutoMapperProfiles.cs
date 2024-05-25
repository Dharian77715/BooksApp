
using AutoMapper;
using BooksApp.DTOs;
using BooksApp.Models;

namespace BooksApp.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()

        {

            CreateMap<Book, BooksDTO>()
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.BooksGenres.Select(bg => bg.Genre)))
                .ReverseMap()
                .ForMember(dest => dest.BooksGenres, opt => opt.Ignore());

            CreateMap<CreateBookDTO, Book>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.BooksGenres, opt => opt.MapFrom(MapBooksGenres));


            CreateMap<Author, AuthorsDTO>()
                .ForMember(dest => dest.SexName, opt => opt.MapFrom(src => src.Sex.Name));

            CreateMap<CreateAuthorDTO, Author>()
                .ForMember(a => a.Photo, options => options.Ignore());


            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<CreateGenreDTO, Genre>();

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();

        }

        private List<BooksGenres> MapBooksGenres(CreateBookDTO CreateBookDTO, Book book)
        {
            var result = new List<BooksGenres>();
            if (CreateBookDTO.GenresIds == null) { return result; }
            foreach (var id in CreateBookDTO.GenresIds)
            {
                result.Add(new BooksGenres() { GenreId = id });
            }

            return result;
        }

    }


}


