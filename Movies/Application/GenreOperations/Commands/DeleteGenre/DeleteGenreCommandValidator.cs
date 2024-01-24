using FluentValidation;

namespace Movies.Application.MovieOperations.Command.DeleteGenre;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>{

    public DeleteGenreCommandValidator()
    {
        RuleFor(command => command.GenreId).GreaterThan(0);
    }

}