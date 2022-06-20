using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.DTOs
{
    public class CinemaFilterDTO
    {

        [Range(-90, 90)]
        public double Latitude { get; set; }
        
        [Range(-180, 180)]
        public double Longitude { get; set; }

        public int distanceInKMs { get; set; } = 10;
        public int maxDistanceInKMs = 50;

        public int DistanceInKMs
        {
            get => distanceInKMs;
            set
            {
                distanceInKMs = value > maxDistanceInKMs ? maxDistanceInKMs : value;
            }
        }
    }
}
