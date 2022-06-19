using AutoMapper;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Gender, GenderDTO>().ReverseMap();
            CreateMap<GenderCreateDTO, Gender>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreateDTO, Actor>().ForMember(a =>
            a.Photo, opt => opt.Ignore());
            CreateMap<ActorUpdateDTO, Actor>().ReverseMap();

            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<MovieCreateDTO, Movie>()
                .ForMember(m => m.Poster, opt => opt.Ignore())
                .ForMember(m => m.MoviesGenders, opt => opt.MapFrom(MapMoviesGenders))
                .ForMember(m => m.MoviesActors, opt => opt.MapFrom(MapMoviesActors));
            CreateMap<MovieUpdateDTO, Movie>().ReverseMap();
        }

        
        private List<MoviesGenders> MapMoviesGenders(MovieCreateDTO movieCreateDTO, Movie movie)
        {
            var result = new List<MoviesGenders>();

            if (movieCreateDTO.GendersIds == null)
            {
                return result;
            }

            foreach (var id in movieCreateDTO.GendersIds)
            {
                result.Add(new MoviesGenders
                {
                    GenderId = id
                });
            }

            return result;
        }

        private List<MoviesActors> MapMoviesActors(MovieCreateDTO movieCreateDTO, Movie movie)
        {
            var result = new List<MoviesActors>();

            if (movieCreateDTO.Actors == null)
            {
                return result;
            }

            foreach (var actor in movieCreateDTO.Actors)
            {
                result.Add(new MoviesActors
                {
                    ActorId = actor.ActorId,
                    Character = actor.Character
                });
            }

            return result;
        }
    }
}
