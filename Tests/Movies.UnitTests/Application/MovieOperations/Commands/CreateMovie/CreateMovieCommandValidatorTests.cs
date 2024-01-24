using AutoMapper;
using FluentAssertions;
using Movies.Application.MovieOperations.Command.CreateMovie;
using Movies.DbOperations;
using Movies.Entities;
using Movies.UnitTests.TestsSetup;
using static Movies.Application.MovieOperations.Command.CreateMovie.CreateMovieCommand;

namespace Movies.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
     
        [Theory]
        [InlineData("Lord Of The Rings", "tr",0)]
        [InlineData("Lord Of The Rings", "en",1)]
        [InlineData("", "",0)]
        [InlineData("lor", "",0)]
        [InlineData("Lord Of The Rings", "tr",0)]
        [InlineData(" ", "tur",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReutnErrors(string title, string language, int genreId){

           
            CreateMovieCommand command = new(null,null);
            command.Model = new(){
                Title = title,
                Language = language,
                PublishDate = DateTime.Now.AddYears(-1),
                GenreId = genreId
            };

            CreateMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError(){
             CreateMovieCommand command = new(null,null);
            command.Model = new(){
                Title = "",
                Language = "tr",
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            
            CreateMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInpursIsGiven_Validator_ShouldBeNotReturnError(){
             CreateMovieCommand command = new(null,null);
            command.Model = new(){
                Title = "Lord Of Thhe Rings",
                Language = "tr",
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            
            CreateMovieCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Equals(0);
        }
    }
}