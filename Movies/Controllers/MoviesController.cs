using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DbOperations;
using Movies.Dtos;
using Movies.Application.MovieOperations.Command.CreateMovie;
using Movies.Application.MovieOperations.Queries.GetById;
using Movies.Application.MovieOperations.Command.DeleteMovie;
using Movies.Application.MovieOperations.Queries.GetMovies;
using Movies.Application.MovieOperations.Command.UpdateMovie;
using static Movies.Application.MovieOperations.Command.CreateMovie.CreateMovieCommand;
using FluentValidation;
using FluentValidation.Results;
using Movies.Entities;



namespace Movies.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public MoviesController(MovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult Get()
    {
        GetMoviesQuery query = new(context, mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        MovieDetailViewModel result;
        try
        {
            GetByIdQuery query = new(context, mapper)
            {
                MovieId = id
            };
            GetByIdQueryValidator validator = new();
            validator.ValidateAndThrow(query);
            result = query.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(result);
    }

    [HttpGet("GetByIdQuery")]
    public IActionResult GetByIdQuery([FromQuery] int id)
    {
        var movie = context.Movies.FirstOrDefault(x => x.Id == id);
        if (movie == null)
        {
            return NotFound();
        }
        return Ok(movie);
    }

    [HttpGet("GetByParameter")]
    public IActionResult GetByParameter([FromQuery] string? Title, [FromQuery] string? Language)
    {
        var movies = context.Movies.AsQueryable();
        if (!string.IsNullOrEmpty(Title))
        {
            movies = movies.Where(x => x.Title.Contains(Title, StringComparison.OrdinalIgnoreCase));
        }
        if (!string.IsNullOrEmpty(Language))
        {
            movies = movies.Where(x => x.Language.Contains(Language, StringComparison.OrdinalIgnoreCase));
        }
        return Ok(movies.ToList<Movie>());
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] CreateMovieModel movie)
    {
        CreateMovieCommand command = new(context, mapper);
        try
        {
            command.Model = movie;
            CreateMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        // ValidationResult result = validator.Validate(command);
        // if(!result .IsValid){
        //     foreach(var item in result.Errors){
        //         Console.WriteLine("Özellik " + item.PropertyName + "- Error Message: "+ item.ErrorMessage);
        //     }
        // }
        // else{
        //     command.Handle();
        // }

        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieModel movie)
    {
        try
        {
            UpdateMovieCommand command = new(context)
            {
                MovieId = id,
                Model = movie
            };
            UpdateMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        try
        {
            DeleteMovieCommand command = new(context)
            {
                MovieId = id
            };
            DeleteMovieCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

    [HttpGet]
    [Route("SortBy")]
    public IActionResult SortBy([FromQuery] string sorting)
    {
        var movie = context.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(sorting) && sorting.ToUpper(CultureInfo.InvariantCulture) == "TITLE")
        {
            movie = movie.OrderBy(x => x.Title);
        }

        else if (!string.IsNullOrEmpty(sorting) && sorting.ToUpper(CultureInfo.InvariantCulture) == "LANGUAGE")
        {
            movie = movie.OrderBy(x => x.Language);
        }

        else
        {
            return BadRequest();
        }
        context.SaveChanges();
        return Ok(movie.ToList());
    }

}

