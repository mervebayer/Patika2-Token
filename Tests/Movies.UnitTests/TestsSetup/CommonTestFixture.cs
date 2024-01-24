
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Common;
using Movies.DbOperations;
using TestSetup;

namespace Movies.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        public  MovieStoreDbContext Context {get; set;}
        public  IMapper Mapper {get; set;}

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName:"MovieStoreTestDB").Options;
            Context = new MovieStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddMovies();
            Context.AddGenres();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}