using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Movies.Application.MovieOperations.Command.CreateMovie;

namespace Movies.Application.MovieOperations.Command.CreateMovie;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>{

    public CreateMovieCommandValidator()
    {
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
        RuleFor(command => command.Model.Title).NotEmpty();
        RuleFor(command => command.Model.Language).NotEmpty().MinimumLength(3);
    }

}