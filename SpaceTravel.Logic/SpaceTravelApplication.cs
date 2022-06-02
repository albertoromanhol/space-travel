using SpaceTravel.Logic.Interface;
using SpaceTravel.Logic.Models;

namespace SpaceTravel.Logic
{
    public class SpaceTravelApplication : ISpaceTravelApplication
    {
        private double _travelDistanceLimit;
        private Planet _earth = new Planet();
        private List<Planet> _planetsToVisit = new List<Planet>();
        private Spacecraft _spacecraft = new Spacecraft();

        public List<Planet> Travel(Spacecraft spacecraft, List<Planet> planetsToVisit, Planet earth, bool optimize)
        {
            _earth = earth;
            _planetsToVisit = planetsToVisit;
            _spacecraft = spacecraft;

            List<Planet> visitedPlanets = new List<Planet>() { earth };

            CalculateRealTravelLimit();
            OptimizePlanetRoute(optimize);
            CalculatePlanetsToTravel(visitedPlanets);

            return visitedPlanets;
        }

        private void OptimizePlanetRoute(bool optimize)
        {
            if (optimize)
            {
                _planetsToVisit = _planetsToVisit.OrderBy(p => p.PositionIndex).ToList();
            }
        }

        private void CalculateRealTravelLimit()
        {
            double maxTravelDistance = _spacecraft.MaxTravelDistance;
            int numberOfPassengers = _spacecraft.Passengers;
            int capcity = _spacecraft.Capacity;

            double maxPercentOfDistanceLoss = 0.3;

            double percentageOfDistanceLoss = (numberOfPassengers / capcity) * maxPercentOfDistanceLoss;

            _travelDistanceLimit = maxTravelDistance * (1 - percentageOfDistanceLoss);
        }

        private void CalculatePlanetsToTravel(List<Planet> visitedPlanets)
        {
            double travelCapacity = _travelDistanceLimit;

            int numberOfVisitedPlanets = 0;

            while (true)
            {
                Planet? planetToVisit = _planetsToVisit.Skip(numberOfVisitedPlanets).FirstOrDefault();

                if (planetToVisit == null) break;

                Planet lastVisitedPlanet = visitedPlanets.Last();

                double currentTravelDistance = planetToVisit.DistanceFromEarth - lastVisitedPlanet.DistanceFromEarth;

                double distanceFromPlanetToEarth = planetToVisit.DistanceFromEarth;
                bool isAbleBackEarth = (travelCapacity - Math.Abs(currentTravelDistance) - Math.Abs(distanceFromPlanetToEarth)) > 0;

                if (!isAbleBackEarth) break;

                bool isAbleToTravel = (travelCapacity - Math.Abs(currentTravelDistance)) > 0;

                if (!isAbleToTravel) break;

                travelCapacity = travelCapacity - Math.Abs(currentTravelDistance);

                numberOfVisitedPlanets += 1;

                visitedPlanets.Add(planetToVisit);
            }

            double distanceBackToEarth = visitedPlanets.Last().DistanceFromEarth;
            travelCapacity = travelCapacity - Math.Abs(distanceBackToEarth);

            visitedPlanets.Add(_earth);

        }
    }
}