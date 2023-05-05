using System;

namespace MeteoAppSkeleton.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public WeatherCondition Weather { get; set; }

        public Location(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Location(string name)
        {
            // Get random UUID
            ID = Guid.NewGuid().GetHashCode();
            Name = name;
        }
    }
}