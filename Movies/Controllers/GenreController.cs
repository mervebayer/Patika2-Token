using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DbOperations;
using Movies.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Movies.Entities;
using Movies.Application.GenreOperations.Queries.GetGenres;
using Movies.Application.GenreOperations.Queries.GetGenreDetail;
using static Movies.Application.GenreOperations.Command.CreateGenre.CreateGenreCommand;
using Movies.Application.GenreOperations.Command.CreateGenre;
using Movies.Application.MovieOperations.Command.UpdateGenre;
using Movies.Application.MovieOperations.Command.DeleteGenre;



namespace Movies.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;

    public GenreController(MovieStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetGenres()
    {
        GetGenresQuery query = new(context, mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetGenreDetail(int id)
    {
        GenreDetailViewModel result;
        try
        {
            GetGenreDetailQuery query = new(context, mapper)
            {
                GenreId = id
            };
            GetGenreDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);
            result = query.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(result);
    }


    [HttpPost]
    public IActionResult AddGenre([FromBody] CreateGenreModel genre)
    {
        CreateGenreCommand command = new(context, mapper);
        try
        {
            command.Model = genre;
            CreateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
   
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel genre)
    {
        try
        {
            UpdateGenreCommand command = new(context);
            command.GenreId = id;
            command.Model = genre;
            UpdateGenreCommandValidator validator = new();
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
    public IActionResult DeleteGenre(int id)
    {
        try
        {
            DeleteGenreCommand command = new(context)
            {
                GenreId = id
            };
            DeleteGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);
            command.Handle();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok();
    }

}

