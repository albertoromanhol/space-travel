using SpaceTravel.Logic.Models;
using SpaceTravel.WPF.Models;
using System.Collections.Generic;

namespace SpaceTravel.WPF.Mapper
{
    public class MapPlanet
    {
        public Planet Mapper(PlanetModel planetModel)
        {
            Planet planet = new Planet
            {
                PositionIndex = planetModel.PositionIndex,
                Name = planetModel.Name,
                DistanceFromEarth = planetModel.DistanceFromEarth
            };

            return planet;
        }
        public List<Planet> Mapper(List<PlanetModel> planetModels)
        {
            List<Planet> planets = new List<Planet>();
            planetModels.ForEach(planet => planets.Add(Mapper(planet)));

            return planets;
        }
    }
}