using AutoMapper;
using FluentAssertions;
using Movies.Application.MovieOperations.Command.DeleteMovie;
using Movies.DbOperations;
using Movies.Entities;
using Movies.UnitTests.TestsSetup;
using static Movies.Application.MovieOperations.Command.DeleteMovie.DeleteMovieCommand;

namespace Movies.UnitTests.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        public DeleteMovieCommandTests(CommonTestFixture testFixture)
        {
            context = testFixture.Context; 
            mapper = testFixture.Mapper; 
        }
        public object MovieId { get; private set; }

        [Fact]
        public void WhenGivenMovieIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            DeleteMovieCommand command = new DeleteMovieCommand(context);
            command.MovieId=1;
   
            
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie not found");
               
        }
        [Fact]
         
            public void WhenGivenMovieIdIsNotinDB_ShouldBeRemove()
            { 
                DeleteMovieCommand command = new DeleteMovieCommand(context);
                command.MovieId=1;

                  FluentActions.Invoking(()=> command.Handle()).Invoke();

                  var Movie = context.Movies.SingleOrDefault(Movie=> Movie.Id == command.MovieId);
                  Movie.Should().Be(null);
            }

    }
}