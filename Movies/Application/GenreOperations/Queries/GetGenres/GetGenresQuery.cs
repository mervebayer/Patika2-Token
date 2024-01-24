using AutoMapper;
using Movies.DbOperations;

namespace Movies.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetGenresQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public  List<GenresViewModel>  Handle(){
            var genres = dbContext.Genres.Where(x => x.IsActive).OrderBy(x => x.Id);
            List<GenresViewModel> returnObj = mapper.Map<List<GenresViewModel>>(genres);
            return returnObj;
        }
    }

    public class GenresViewModel{
        public int Id {get; set;}
        public string Name {get; set;}
    }
}