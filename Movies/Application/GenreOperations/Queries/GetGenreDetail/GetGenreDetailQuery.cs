using AutoMapper;
using Movies.DbOperations;

namespace Movies.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreId{get; set;}
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public GetGenreDetailQuery(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public  GenreDetailViewModel  Handle(){
            var genre = dbContext.Genres.Where(x => x.IsActive && x.Id == GenreId).OrderBy(x => x.Id);
            if(genre is null){
                throw new InvalidOperationException("Genre türü bulunamadı");
            }
            return mapper.Map<GenreDetailViewModel>(genre);
        }
    }

    public class GenreDetailViewModel{
        public int Id {get; set;}
        public string Name {get; set;}
    }
}