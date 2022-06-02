namespace SpaceTravel.WPF.Models
{
    public class PlanetModel
    {
        public string Name { get; set; }
        public int PositionIndex { get; set; }
        public double Diameter { get; set; }
        public double AverageTemperature { get; set; }
        public double DistanceFromEarth { get; set; }
        public bool IsDwarf { get; set; }
    }
}