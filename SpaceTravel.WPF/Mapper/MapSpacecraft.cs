using SpaceTravel.Logic.Models;
using SpaceTravel.WPF.Models;

namespace SpaceTravel.WPF.Mapper
{
    public class MapSpacecraft
    {
        public Spacecraft Mapper(SpacecraftModel spacecraftModel, int passengers)
        {
            Spacecraft spacecraft = new Spacecraft
            {
                Name = spacecraftModel.Name,
                MaxTravelDistance = spacecraftModel.MaxTravelDistance,
                Capacity = spacecraftModel.Capacity,
                Passengers = passengers,
            };

            return spacecraft;
        }

    }
}