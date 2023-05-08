using System;
using SQLite;

namespace MeteoAppSkeleton.Models
{
    public class Location
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        //public WeatherCondition Weather { get; set; }

        public Location()
        {
            // Get random UUID
            Id = Guid.NewGuid().GetHashCode();
           //Weather = null;
          
        }
    }
}