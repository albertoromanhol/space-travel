using SpaceTravel.Logic.Models;

namespace SpaceTravel.Logic.Interface
{
    public interface ISpaceTravelApplication
    {
        List<Planet> Travel(Spacecraft spacecraft, List<Planet> planetsToVisit, Planet earth, bool optimize);
    }
}
