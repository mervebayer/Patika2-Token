using AutoMapper;
using FluentAssertions;
using Movies.Application.MovieOperations.Command.CreateMovie;
using Movies.DbOperations;
using Movies.Entities;
using Movies.UnitTests.TestsSetup;
using static Movies.Application.MovieOperations.Command.CreateMovie.CreateMovieCommand;

namespace Movies.UnitTests.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        public CreateMovieCommandTests(CommonTestFixture testFixture)
        {
            context = testFixture.Context; 
            mapper = testFixture.Mapper; 
        }
        [Fact]

        public void WhenAlreadyExistMovieTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
           //arrange
            var movie = new Movie(){Title="WhenAlreadyExistMovieTitleIsGiven_InvalidOperationException_ShouldBeReturn",Language ="TR", GenreId=1,PublishDate=DateTime.Now};
            context.Movies.Add(movie);
            context.SaveChanges();

            CreateMovieCommand command = new(context, mapper)
            {
                Model = new CreateMovieModel() { Title = movie.Title }
            };

            FluentActions.Invoking(()=> command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie is exist.");
        }

        
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeCreated(){
              CreateMovieCommand command = new(context, mapper);

         
            CreateMovieModel model = new(){
                Title = "Lord Of Thhe Rings",
                Language = "tr",
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };
            command.Model = model;
            
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var movie = context.Movies.SingleOrDefault(movie => movie.Title == model.Title);
            movie.Should().NotBeNull();
            movie.GenreId.Should().Be(model.GenreId);
            movie.PublishDate.Should().Be(model.PublishDate);
            movie.Language.Should().Be(model.Language);
        }

    }
}