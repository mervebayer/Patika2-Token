using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Movies.Entities;

public class User
{
    //Auto-Increment
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{get; set;}
    public string Name {get; set;}
    public string Surname {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string RefreshToken {get; set;}
    public DateTime? RefreshTokenExpireDate {get; set;}
}
