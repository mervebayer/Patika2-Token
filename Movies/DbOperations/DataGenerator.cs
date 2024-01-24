using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Movies.Entities;
namespace Movies.DbOperations;

public class DataGenerator{
    //in-memory
    public static void Initialize(IServiceProvider serviceProvider){
        using (var context = new MovieStoreDbContext ( serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>() )){
            if(context.Movies.Any()){
                return;
            }
            context.Genres.AddRange(
                new Genre{
                    Name ="Science Fiction"
                },
                new Genre{
                    Name ="Romance"
                },
                new Genre{
                    Name ="Personal Growth"
                }
            );

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
            context.SaveChanges();
        }
    }
}