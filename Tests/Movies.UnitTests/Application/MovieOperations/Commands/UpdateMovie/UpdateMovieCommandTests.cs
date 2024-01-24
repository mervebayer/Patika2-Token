using AutoMapper;
using FluentAssertions;
using Movies.Application.MovieOperations.Command.UpdateMovie;
using Movies.DbOperations;
using Movies.Entities;
using Movies.UnitTests.TestsSetup;
using static Movies.Application.MovieOperations.Command.UpdateMovie.UpdateMovieCommand;

namespace Movies.UnitTests.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext context;
        private readonly IMapper mapper;
        public UpdateMovieCommandTests(CommonTestFixture testFixture)
        {
            context = testFixture.Context; 
            mapper = testFixture.Mapper; 
        }
        public object MovieId { get; private set; }

        [Fact]
        public void WhenGivenMovieIdIsNotinDB_InvalidOperationsException_ShouldBeReturn()
        {
            UpdateMovieCommand command = new UpdateMovieCommand(context);
            command.MovieId=0;

            
              FluentActions.Invoking(()=> command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Movie not found");
        }

        [Fact] 
         public void WhenGivenMovieIdinDB_ShouldBeUpdate()
         { 
             UpdateMovieCommand command =new UpdateMovieCommand(context);

             UpdateMovieModel model = new UpdateMovieModel() {Title="WhenGivenMovieIdinDB_ShouldBeUpdate",GenreId=1 };
             command.Model= model;
             command.MovieId = 1;

               FluentActions.Invoking(()=> command.Handle()).Invoke();

               var Movie = context.Movies.SingleOrDefault(Movie=> Movie.Id == command.MovieId);
               Movie.Should().NotBeNull();
         }

    }
}