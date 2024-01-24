using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Movies;
using Movies.DbOperations;
using Movies.Entities;

namespace Movies.Application.MovieOperations.Command.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieModel Model { get; set; }
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;
        public CreateMovieCommand(MovieStoreDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var movie =dbContext.Movies.SingleOrDefault(x=> x.Title==Model.Title);
            if(movie is not null)
            {
                throw new InvalidOperationException("Movie is exist.");
            }
            movie = mapper.Map<Movie>(Model);
            // movie = new Movie(){
            //     Title = Model.Title,
            //     PublishDate = Model.PublishDate,
            //     Language = Model.Language,
            //     GenreId = Model.GenreId

            // };
        
            dbContext.Movies.Add(movie);
            dbContext.SaveChanges();
        }

        public class CreateMovieModel
        {
            
            public string Title {get; set;}
            public int GenreId {get; set;}
            public string Language {get; set;}
            public DateTime PublishDate {get; set;}
        }
    }
}