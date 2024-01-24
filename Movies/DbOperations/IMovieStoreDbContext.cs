using Microsoft.EntityFrameworkCore;
using Movies.Entities;

namespace Movies.DbOperations
{
    public interface IMovieStoreDbContext 
    {
        DbSet<Movie> Movies {get;set;}
        DbSet<Genre> Genres {get;set;}
        DbSet<User> Users {get; set;}
        int SaveChanges();
    }
}