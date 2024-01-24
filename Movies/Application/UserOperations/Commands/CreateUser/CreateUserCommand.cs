using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Movies;
using Movies.DbOperations;
using Movies.Entities;

namespace Movies.Application.MovieOperations.Command.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IMovieStoreDbContext dbContext;
        private readonly IMapper mapper;
        public CreateUserCommand(IMovieStoreDbContext dbContext,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public void Handle()
        {
            var user =dbContext.Users.SingleOrDefault(x=> x.Email==Model.Email);
            if(user is not null)
            {
                throw new InvalidOperationException("user is exist.");
            }
            user = mapper.Map<User>(Model);
        
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public class CreateUserModel
        {
            
            public string Name {get; set;}
            public string Surname {get; set;}
            public string Email {get; set;}
            public string Password {get; set;}
        }
    }
}