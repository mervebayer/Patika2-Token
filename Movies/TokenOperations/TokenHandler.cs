using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Movies.Entities;
using Movies.TokenOperations.Models;

namespace Movies.TokenOperations;

public class TokenHandler
{
    public IConfiguration configuration;
    public TokenHandler(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public Token CreateAccessToken(User user){
        Token tokenModel = new();
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));
        SigningCredentials credentials = new(key,SecurityAlgorithms.HmacSha256);
        tokenModel.Expiration = DateTime.Now.AddMinutes(15);
        JwtSecurityToken securityToken = new(issuer:configuration["Token:Issuer"],
                                            audience:configuration["Token:Audience"],
                                            expires: tokenModel.Expiration,
                                            notBefore: DateTime.Now,
                                            signingCredentials :  credentials);
                                            
        JwtSecurityTokenHandler tokenHandler = new();

        //token yaratma
        tokenModel.AccessToken =   tokenHandler.WriteToken(securityToken);
        tokenModel.RefreshToken =   CreateRefreshToken();
        return tokenModel;                              
    }

    public string CreateRefreshToken(){
        return Guid.NewGuid().ToString();
    }
}