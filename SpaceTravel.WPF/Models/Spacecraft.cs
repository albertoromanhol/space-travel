namespace SpaceTravel.WPF.Models
{
    public class Spacecraft
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public bool GravityGenerator { get; set; }
        public double MaxTravelDistance { get; set; }
        public bool AsteroidDeflector { get; set; }

    }
}