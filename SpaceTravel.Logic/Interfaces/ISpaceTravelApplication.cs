using SpaceTravel.Logic.Models;

namespace SpaceTravel.Logic.Interface
{
    public interface ISpaceTravelApplication
    {
        TravelResult Travel(Spacecraft spacecraft, List<Planet> planetsToVisit, Planet earth, bool optimize);
    }
}
