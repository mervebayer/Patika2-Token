using FluentValidation;

namespace Movies.Application.MovieOperations.Command.DeleteMovie;

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>{

    public DeleteMovieCommandValidator()
    {
        RuleFor(command => command.MovieId).GreaterThan(0);
    }

}