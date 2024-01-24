using Movies.DbOperations;
using Movies.Entities;

namespace TestSetup;

public static class Genres{
    public static void AddGenres(this MovieStoreDbContext context){
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
    }
}