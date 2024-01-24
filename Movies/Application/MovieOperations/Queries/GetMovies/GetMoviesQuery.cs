using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies;
using Movies.Common;
using Movies.DbOperations;
using Movies.Entities;

namespace Movies.Application.MovieOperations.Queries.GetMovies
{
    public class GetMoviesQuery
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetMoviesQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<MoviesViewModel> Handle()
        {
            var movieList = dbContext.Movies.Include(x => x.Genre).OrderBy(x=>x.Id).ToList<Movie>();
            List<MoviesViewModel> vm= mapper.Map<List<MoviesViewModel>>(movieList);
            // List<MoviesViewModel> vm= new List<MoviesViewModel>();
            // foreach(var movie in movieList)
            // {
            //     vm.Add(new MoviesViewModel(){
            //         Title=movie.Title,
            //         Genre=((GenreEnum)movie.GenreId).ToString(),
            //         PublishDate=movie.PublishDate.Date.ToString("dd/MM/yyy"),
            //         Language=movie.Language
            //     });
            // }
            return vm;
        }
    }

    public class MoviesViewModel
    {
        public string Title {get; set;}
        public string Genre {get; set;}
        public string Language {get; set;}
        public string PublishDate {get; set;}
    }
}