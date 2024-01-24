using AutoMapper;
using FluentAssertions;
using Movies.Application.MovieOperations.Command.DeleteMovie;
using Movies.DbOperations;
using Movies.Entities;
using Movies.UnitTests.TestsSetup;
using static Movies.Application.MovieOperations.Command.DeleteMovie.DeleteMovieCommand;

namespace Movies.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
     
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidMovieIdIsGiven_InvalidOperationsException_ShouldBeReturnErrors(int MovieId)
        { 
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId=MovieId;

            DeleteMovieCommandValidator validations = new DeleteMovieCommandValidator();
            
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);    
        }

        [Theory]
        [InlineData(0)]
        public void WhenInvalidMovieIdIsGiven_Validator_ShouldBeReturnErrors(int MovieId)
        { 
            DeleteMovieCommand command = new DeleteMovieCommand(null);
            command.MovieId=MovieId;

            DeleteMovieCommandValidator validations = new DeleteMovieCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }     
    }
}