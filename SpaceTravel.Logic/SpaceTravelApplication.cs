using SpaceTravel.Logic.Interface;
using SpaceTravel.Logic.Models;
using System.Text;

namespace SpaceTravel.Logic
{
    public class SpaceTravelApplication : ISpaceTravelApplication
    {
        private double _travelDistanceLimit;
        private Planet _earth = new Planet();
        private List<Planet> _planetsToVisit = new List<Planet>();
        private Spacecraft _spacecraft = new Spacecraft();
        private TravelResult _result = new TravelResult();

        public TravelResult Travel(Spacecraft spacecraft, List<Planet> planetsToVisit, Planet earth, bool optimize)
        {
            _earth = earth;
            _planetsToVisit = planetsToVisit;
            _spacecraft = spacecraft;

            CalculateRealTravelLimit();
            OptimizePlanetRoute(optimize);
            CalculatePlanetsToTravel();

            CreateResultMessage();

            return _result;
        }

        private void OptimizePlanetRoute(bool optimize)
        {
            if (optimize)
            {
                _planetsToVisit = _planetsToVisit.OrderBy(p => p.PositionIndex).DistinctBy(p => p.PositionIndex).ToList();
            }
        }

        private void CalculateRealTravelLimit()
        {
            double maxTravelDistance = _spacecraft.MaxTravelDistance;
            double numberOfPassengers = _spacecraft.Passengers;
            double capacity = _spacecraft.Capacity;

            double maxPercentOfDistanceLoss = 0.3;

            double percentageOfDistanceLoss = (numberOfPassengers / capacity) * maxPercentOfDistanceLoss;

            _travelDistanceLimit = maxTravelDistance * (1 - percentageOfDistanceLoss);

            _result.TravelDistanceLimit = _travelDistanceLimit;
        }

        private void CalculatePlanetsToTravel()
        {
            List<Planet> visitedPlanets = new List<Planet>();

            visitedPlanets.Add(_earth);

            double travelCapacity = _travelDistanceLimit;

            int numberOfVisitedPlanets = 0;
            bool isAbleToTravel = true;

            while (isAbleToTravel)
            {
                numberOfVisitedPlanets = visitedPlanets.Count - 1;

                Planet? planetToVisit = _planetsToVisit.Skip(numberOfVisitedPlanets).FirstOrDefault();

                if (planetToVisit == null)
                {
                    isAbleToTravel = false;
                    break;
                };

                Planet lastVisitedPlanet = visitedPlanets.Last();

                double distanceNeedsToTravel = planetToVisit.DistanceFromEarth - lastVisitedPlanet.DistanceFromEarth;

                bool isAbleBackEarth = CalculateIfSpacecraftBackToEarth(travelCapacity, planetToVisit, distanceNeedsToTravel);
                isAbleToTravel = CalculateIfSpacecraftCanTravelBeteweenPlanets(travelCapacity, distanceNeedsToTravel);

                isAbleToTravel = isAbleToTravel && isAbleBackEarth;

                if (isAbleToTravel)
                {
                    travelCapacity = CalculateTravelCapacity(travelCapacity, distanceNeedsToTravel);
                    visitedPlanets.Add(planetToVisit);
                }
            }

            double distanceBackToEarth = visitedPlanets.Last().DistanceFromEarth;

            travelCapacity = CalculateTravelCapacity(travelCapacity, distanceBackToEarth);
            visitedPlanets.Add(_earth);

            UpdateResult(visitedPlanets, travelCapacity);
        }

        private double CalculateTravelCapacity(double travelCapacity, double distanceToTravel)
        {
            travelCapacity = travelCapacity - Math.Abs(distanceToTravel);
            return travelCapacity;
        }

        private static bool CalculateIfSpacecraftBackToEarth(double travelCapacity, Planet planetToVisit, double currentTravelDistance)
        {
            bool isAbleBackEarth;
            double distanceFromPlanetToEarth = planetToVisit.DistanceFromEarth;
            isAbleBackEarth = (travelCapacity - Math.Abs(currentTravelDistance) - Math.Abs(distanceFromPlanetToEarth)) > 0;
            return isAbleBackEarth;
        }

        private static bool CalculateIfSpacecraftCanTravelBeteweenPlanets(double travelCapacity, double currentTravelDistance)
        {
            return (travelCapacity - Math.Abs(currentTravelDistance)) > 0;
        }

        private void UpdateResult(List<Planet> visitedPlanets, double travelCapacity)
        {
            _result.TravelDistance = _travelDistanceLimit - travelCapacity;
            _result.Planets = visitedPlanets;

            CheckIfNeedsToInterruptTheTravel(visitedPlanets);
        }

        private void CheckIfNeedsToInterruptTheTravel(List<Planet> visitedPlanets)
        {
            int visitedPlanetsWithoutStartAndEndEarths = visitedPlanets.Count - 2;
            int visitedPlanetsCount = _planetsToVisit.Count;

            bool needToInterrupsTheTravel = visitedPlanetsWithoutStartAndEndEarths != visitedPlanetsCount;
            _result.NotTheFullTravel = needToInterrupsTheTravel;
        }

        private void CreateResultMessage()
        {
            StringBuilder message = new StringBuilder();

            message.AppendLine("Hello fellow travler!");
            message.AppendLine();
            message.AppendLine($"You choose the '{_spacecraft.Name}' for your travel! We got that you have {_spacecraft.Passengers} passengers. ");

            if (_spacecraft.Passengers > 0)
            {
                message.AppendLine($"Unfortunaly, because of the number of passengers your travel capacity goes from the max of {Math.Round(_spacecraft.MaxTravelDistance, 2)} Mkm to {Math.Round(_travelDistanceLimit, 2)} Mkm!");
            }
            message.AppendLine();
            message.AppendLine($"With your planet list that you wish to visit, I saw that you can go to theses ones: ");

            string planetsToVisit = String.Join(", ", _result.Planets.Select(p => p.Name).ToList());

            message.AppendLine();
            message.AppendLine(planetsToVisit);

            message.AppendLine();
            message.AppendLine($"For that, you will travel {Math.Round(_result.TravelDistance, 2)} Mkm.");

            if (_result.NotTheFullTravel)
            {
                message.AppendLine();
                message.AppendLine($"Unfortunaly, your spacecraft cant handle all your desire planets, so we will take you as far as you can!");
            }

            message.AppendLine();
            message.AppendLine("See you!");

            _result.Message = message.ToString();
        }

    }
}