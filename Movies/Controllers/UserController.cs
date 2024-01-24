
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Movies.DbOperations;
using static Movies.Application.MovieOperations.Command.CreateUser.CreateUserCommand;
using Movies.Application.MovieOperations.Command.CreateUser;
using Movies.TokenOperations.Models;
using Movies.Application.MovieOperations.Commands.CreateToken;
using static Movies.Application.MovieOperations.Commands.CreateToken.CreateTokenCommand;



namespace Movies.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly MovieStoreDbContext context;
    private readonly IMapper mapper;
    readonly IConfiguration configuration;

    public UserController(MovieStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        this.context = context;
        this.mapper = mapper;
        this.configuration = configuration;

    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserModel newUser)
    {
        CreateUserCommand command = new(context, mapper)
        {
            Model = newUser
        };
        command.Handle();

        return Ok();
    }
    
    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody]CreateTokenModel login)
    {
        CreateTokenCommand command = new(context, mapper, configuration)
        {
            Model = login
        };
        var token =command.Handle();

        return token;
    }

}

