using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;


namespace Movies.Application.GenreOperations.Command.CreateGenre;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>{

    public CreateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);
    }

}