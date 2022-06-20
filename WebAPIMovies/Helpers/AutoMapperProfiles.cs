using AutoMapper;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using WebAPIMovies.DTOs;
using WebAPIMovies.Entities;

namespace WebAPIMovies.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<Genre, GenreDTO>().ReverseMap();
            CreateMap<GenreCreateDTO, Genre>();

            CreateMap<Cinema, CinemaDTO>()
                .ForMember(c => c.Latitude, opt => opt.MapFrom(c => c.Location.Y))
                .ForMember(c => c.Longitude, opt => opt.MapFrom(c => c.Location.X));

            CreateMap<CinemaDTO, Cinema>()
                .ForMember(c => c.Location, opt => opt.MapFrom(x =>
                geometryFactory.CreatePoint(new Coordinate(x.Longitude, x.Latitude))));

            CreateMap<CinemaCreateDTO, Cinema>()
                .ForMember(c => c.Location, opt => opt.MapFrom(x =>
                geometryFactory.CreatePoint(new Coordinate(x.Longitude, x.Latitude))));

            CreateMap<CinemaCreateDTO, Cinema>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreateDTO, Actor>().ForMember(a =>
            a.Photo, opt => opt.Ignore());
            CreateMap<ActorUpdateDTO, Actor>().ReverseMap();

            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<MovieCreateDTO, Movie>()
                .ForMember(m => m.Poster, opt => opt.Ignore())
                .ForMember(m => m.MoviesGenres, opt => opt.MapFrom(MapMoviesGenres))
                .ForMember(m => m.MoviesActors, opt => opt.MapFrom(MapMoviesActors));
            CreateMap<MovieUpdateDTO, Movie>().ReverseMap();
            CreateMap<Movie, MovieDetailDTO>()
                .ForMember(m => m.Genres, opt => opt.MapFrom(MapMoviesGenres))
                .ForMember(m => m.Actors, opt => opt.MapFrom(MapMoviesActors));
        }

        private List<MoviesGenres> MapMoviesGenres(MovieCreateDTO movieCreateDTO, Movie movie)
        {
            var result = new List<MoviesGenres>();

            if (movieCreateDTO.GenresIds == null)
            {
                return result;
            }

            foreach (var id in movieCreateDTO.GenresIds)
            {
                result.Add(new MoviesGenres
                {
                    GenreId = id
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

        private List<GenreDTO> MapMoviesGenres(Movie movie, MovieDetailDTO movieDetailDTO)
        {
            var result = new List<GenreDTO>();

            if (movie.MoviesGenres == null)
            {
                return result;
            }
            
            foreach (var movieGenre in movie.MoviesGenres)
            {
                result.Add(new GenreDTO
                {
                    Id = movieGenre.GenreId,
                    Name = movieGenre.Genre.Name
                });
            }

            return result;
        }

        private List<MovieActorDetailDTO> MapMoviesActors(Movie movie, MovieDetailDTO movieDetailDTO)
        {
            var result = new List<MovieActorDetailDTO>();

            if (movie.MoviesActors == null)
            {
                return result;
            }

            foreach (var movieActor in movie.MoviesActors)
            {
                result.Add(new MovieActorDetailDTO
                {
                    ActorId = movieActor.ActorId,
                    Character = movieActor.Character,
                    Name = movieActor.Actor.Name,
                });
            }

            return result;
        }
    }
}
