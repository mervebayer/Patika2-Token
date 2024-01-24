using AutoMapper;
using Movies.Entities;
using Movies.Application.MovieOperations.Queries.GetById;
using Movies.Application.MovieOperations.Queries.GetMovies;
using static Movies.Application.MovieOperations.Command.CreateMovie.CreateMovieCommand;
using Movies.Application.GenreOperations.Queries.GetGenres;
using Movies.Application.GenreOperations.Queries.GetGenreDetail;

namespace Movies.Common
{
    public class MappingProfile : Profile{
        public MappingProfile(){
            CreateMap<CreateMovieModel,Movie>();
            CreateMap<Movie, MovieDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Movie,MoviesViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
        }
    }
}