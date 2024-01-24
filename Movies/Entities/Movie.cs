using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Movies.Entities;

public class Movie
{
    //Auto-Increment
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{get; set;}
    public string Title {get; set;}
    public int GenreId {get; set;}
    public Genre Genre {get; set;}
    public string Language {get; set;}
    public DateTime PublishDate {get; set;}
}
