using AutoMapper;

using Movies.DbOperations;
using Movies.TokenOperations;
using Movies.TokenOperations.Models;

namespace Movies.Application.MovieOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set;}
        private readonly IMovieStoreDbContext dbContext;
        private readonly IConfiguration configuration;
        public RefreshTokenCommand(IMovieStoreDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public Token Handle()
        {
            var user = dbContext.Users.SingleOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
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
                throw new InvalidOperationException("not valid refresh token");
            }

        }

  
    }
}