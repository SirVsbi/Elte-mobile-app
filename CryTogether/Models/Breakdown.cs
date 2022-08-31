using Geolocation;


namespace CryTogether.Models
{
    public class Breakdown
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string suferer { get; set; }

        public Coordinate cord { get; set; }


    }
}