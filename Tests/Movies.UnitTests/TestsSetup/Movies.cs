using Movies.DbOperations;
using Movies.Entities;

namespace TestSetup;

public static class Movies{
    public static void AddMovies(this MovieStoreDbContext context){
        context.Movies.AddRange(
                new  Movie{
                  //  Id=1,
                    Title="Lord Of The Rings",
                    GenreId=1,
                    Language="English",
                    PublishDate = new DateTime(2000,01,01)
                },
                new Movie{
                  //  Id=2,
                    Title="StarWars",
                    GenreId=1,
                    Language="English",
                    PublishDate = new DateTime(1980,03,04)
                },
                new Movie{
                  //  Id=3,
                    Title="Love101",
                    GenreId=2,
                    Language="Turkish",
                    PublishDate = new DateTime(2010,05,03)
                });
    }
}