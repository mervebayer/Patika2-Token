using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.DbOperations;

namespace Movies.Application.MovieOperations.Command.DeleteMovie
{
    public class DeleteMovieCommand
    {
        private readonly MovieStoreDbContext dbContext;
        public int MovieId {get; set;}
        public DeleteMovieCommand(MovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Handle()
        {
            var movie=dbContext.Movies.SingleOrDefault(x=>x.Id==MovieId);
            if (movie is null)
            {
                throw new InvalidOperationException("Movie not found");
            }
            dbContext.Movies.Remove(movie);
            dbContext.SaveChanges();
        }
    }
}