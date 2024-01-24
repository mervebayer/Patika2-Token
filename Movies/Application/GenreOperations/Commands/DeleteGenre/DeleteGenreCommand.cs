using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.DbOperations;

namespace Movies.Application.MovieOperations.Command.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly IMovieStoreDbContext dbContext;
        public int GenreId {get; set;}
        public DeleteGenreCommand(IMovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Handle()
        {
            var genre=dbContext.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Movie not found");
            }
            dbContext.Genres.Remove(genre);
            dbContext.SaveChanges();
        }
    }
}