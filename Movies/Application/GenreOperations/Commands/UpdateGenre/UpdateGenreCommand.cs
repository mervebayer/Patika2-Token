using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using Movies.DbOperations;

namespace Movies.Application.MovieOperations.Command.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly MovieStoreDbContext dbContext;
        public int GenreId {get; set;}
        public UpdateGenreModel Model {get; set;}
        public UpdateGenreCommand(MovieStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Handle()
        {
            var genre = dbContext.Genres.SingleOrDefault(x=> x.Id==GenreId);
            if(genre is null)
             {
                throw new InvalidOperationException("Movie not found");
            }
           if(dbContext.Genres.Any(x=>x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı isimli film türü mevcut");
            }
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name: Model.Name ;
            genre.IsActive = Model.IsActive;
            dbContext.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}