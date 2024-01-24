using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Movies.Common;
using Movies.DbOperations;


namespace Movies.Application.MovieOperations.Queries.GetById
{
    public class GetByIdQuery
    {
        private readonly MovieStoreDbContext dbContext;
        public int MovieId {get; set;}
        public readonly IMapper mapper;
        public GetByIdQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public MovieDetailViewModel Handle()
        {
            var movie = dbContext.Movies.Where(x=>x.Id==MovieId).SingleOrDefault();
            if(movie is null)
            {
                throw new InvalidOperationException("Movie not found");
            }
            MovieDetailViewModel vm= mapper.Map<MovieDetailViewModel>(movie);
            // _ = new MovieDetailViewModel();
            //  vm.Title=movie.Title;
            //  vm.Genre=((GenreEnum)movie.GenreId).ToString();
            //  vm.PublishDate=movie.PublishDate.Date.ToString("dd/MM/yyy");
            //  vm.Language=movie.Language;
            return vm;
        }
    }
    public class MovieDetailViewModel
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}