using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Movies;
using Movies.DbOperations;
using Movies.Entities;
using Movies.TokenOperations;
using Movies.TokenOperations.Models;

namespace Movies.Application.MovieOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public CreateTokenCommand(IMovieStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public Token Handle()
        {
            var user = dbContext.Users.SingleOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is not null)
            {
                TokenHandler handler = new(configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                dbContext.SaveChanges();
                return token;
            }
            else
            {
                throw new InvalidOperationException("user is wrong.");
            }

        }

        public class CreateTokenModel
        {

            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}