using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Movies;
using Movies.DbOperations;
using Movies.Entities;

namespace Movies.Application.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;
        public CreateGenreCommand(MovieStoreDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var genre =dbContext.Genres.SingleOrDefault(x=> x.Name==Model.Name);
            if(genre is not null)
            {
                throw new InvalidOperationException("Movie is exist.");
            }
            genre = mapper.Map<Genre>(Model);
        
            dbContext.Genres.Add(genre);
            dbContext.SaveChanges();
        }

        public class CreateGenreModel
        {
            
            public string Name {get; set;}
            
        }
    }
}