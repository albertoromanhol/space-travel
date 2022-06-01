using SpaceTravel.Logic.Models;

namespace SpaceTravel.Logic.Interface
{
    public interface ISpaceTravelApplication
    {
        RouteResult Travel(Spacecraft spacecraft, List<Planet> planetsToVisit);
    }
}
