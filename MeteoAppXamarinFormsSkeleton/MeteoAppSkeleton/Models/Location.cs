using System;
using SQLite;

namespace MeteoAppSkeleton.Models
{
    public class Location
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public Location()
        {
            // Get random UUID
            Id = Guid.NewGuid().GetHashCode();
        }
    }
}