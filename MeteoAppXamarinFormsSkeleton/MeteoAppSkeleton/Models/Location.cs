﻿namespace MeteoAppSkeleton.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Location(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}