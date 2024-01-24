using System;
using Microsoft.EntityFrameworkCore;
using Movies.Entities;
namespace Movies.DbOperations;

public class MovieStoreDbContext : DbContext
{
    public MovieStoreDbContext(DbContextOptions<MovieStoreDbContext> options) : base(options)
    {
        
    }
    public DbSet<Movie> Movies {get; set;}
    public DbSet<Genre> Genres {get; set;}
}