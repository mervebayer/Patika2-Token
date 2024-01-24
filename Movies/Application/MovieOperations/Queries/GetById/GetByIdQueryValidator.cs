using FluentValidation;

namespace Movies.Application.MovieOperations.Queries.GetById;

public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>{

    public GetByIdQueryValidator()
    {
        RuleFor(query => query.MovieId).GreaterThan(0);
    }

}