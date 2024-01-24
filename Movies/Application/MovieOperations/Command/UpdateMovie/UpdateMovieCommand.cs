using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Movies.DbOperations;

namespace Movies.Application.MovieOperations.Command.UpdateMovie
{
    public class UpdateMovieCommand
    {
        private readonly MovieStoreDbContext dbContext;
        public int MovieId {get; set;}
        public UpdateMovieModel Model {get; set;}
        public UpdateMovieCommand(MovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Handle()
        {
            var movie = dbContext.Movies.SingleOrDefault(x=> x.Id==MovieId);
            if(movie is null)
             {
                throw new InvalidOperationException("Movie not found");
            }
            movie.GenreId=Model.GenreId != default ? Model.GenreId : movie.GenreId;
            movie.Title=Model.Title != default ? Model.Title : movie.Title;
            dbContext.SaveChanges();
        }
    }
    public class UpdateMovieModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}