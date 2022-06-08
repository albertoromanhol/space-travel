namespace SpaceTravel.Logic.Models
{
    public class TravelResult
    {
        public List<Planet> Planets { get; set; }
        public double TravelDistanceLimit { get; set; }
        public double TravelDistance { get; set; }
        public string Message { get; set; }
        public bool NotTheFullTravel { get; set; }
    }
}