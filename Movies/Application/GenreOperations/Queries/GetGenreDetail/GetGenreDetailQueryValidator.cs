using AutoMapper;
using FluentValidation;
using Movies.DbOperations;

namespace Movies.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidator: AbstractValidator<GetGenreDetailQuery>
    {
        public GetGenreDetailQueryValidator()
        {
            RuleFor(q => q.GenreId).GreaterThan(0);
        }
    }

}