using Newtonsoft.Json.Linq;
using SpaceTravel.Logic;
using SpaceTravel.Logic.Models;
using SpaceTravel.WPF.Database;
using SpaceTravel.WPF.Mapper;
using SpaceTravel.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SpaceTravel.WPF.ViewModels
{
    public class SpaceTravelViewModel : INotifyPropertyChanged
    {
        private SpaceTravelApplication _spaceTravelApplication;
        private MapSpacecraft _spacecraftMapper;
        private MapPlanet _planetMapper;

        public SpaceTravelViewModel()
        {
            _spaceTravelApplication = new SpaceTravelApplication();
            _spacecraftMapper = new MapSpacecraft();
            _planetMapper = new MapPlanet();

            PopulateData();
        }

        #region[SPACECRAFT]
        private List<SpacecraftModel> _spacecrafts = new List<SpacecraftModel>();

        public List<SpacecraftModel> Spacecrafts
        {
            get { return _spacecrafts; }
        }

        private SpacecraftModel _spacecraftSelected = new SpacecraftModel();
        public SpacecraftModel SpacecraftSelected
        {
            get
            {
                return _spacecraftSelected;
            }
            set
            {
                _spacecraftSelected = value;
            }
        }
        #region[PASSENGERS]

        private int _currentPassengers = 0;
        private int _maxPassengers = 0;
        public int MaxPassengers
        {
            get { return _maxPassengers; }
        }
        public int CurrentPassengers
        {
            get
            {
                return _currentPassengers;
            }
            set
            {
                _currentPassengers = value;
            }
        }

        #endregion[PASSENGERS]
        #endregion[SPACECRAFT]

        #region[PLANETS]
        private PlanetModel _earth;

        private string _planetNames = String.Empty;

        private List<PlanetModel> _planets = new List<PlanetModel>();
        public List<PlanetModel> Planets
        {
            get { return _planets; }
        }

        private List<PlanetModel> _planetsSelected = new List<PlanetModel>();
        public string PlanetNames { get { return _planetNames; } set { _planetNames = value; OnPropertyChanged("PlanetNames"); } }

        private PlanetModel _selectedPlanet = new PlanetModel();
        public PlanetModel SelectedPlanet
        {
            get
            {
                return _selectedPlanet;
            }
            set
            {
                PlanetModel planetSelected = value;

                if (planetSelected != null)
                {
                    _planetsSelected.Add(value);
                    PlanetNames = String.Join(", ", _planetsSelected.Select(p => p.Name).ToList());

                    planetSelected = new PlanetModel();
                    OnPropertyChanged("SelectedPlanet");
                }

                _selectedPlanet = planetSelected;

            }
        }


        #endregion[PLANETS]

        #region[RESULT]
        private string _planetsToVisit = "Earth";
        public string PlanetsToVisit
        {
            get { return _planetsToVisit; }
            set
            {
                _planetsToVisit = value;
                OnPropertyChanged("PlanetsToVisit");
            }
        }

        private bool _optimize = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool Optimize
        {
            get
            {
                return _optimize;
            }
            set
            {
                _optimize = value;
            }
        }

        public void Travel()
        {
            Spacecraft spacecraft = _spacecraftMapper.Mapper(_spacecraftSelected, _currentPassengers);
            List<Planet> planets = _planetMapper.Mapper(_planetsSelected);
            Planet earth = _planetMapper.Mapper(_earth);

            List<Planet> planetsToVisit = _spaceTravelApplication.Travel(spacecraft, planets, earth, _optimize);


            PlanetsToVisit = String.Join(", ", planetsToVisit.Select(p => p.Name).ToList());

            if (planets.Count > (planetsToVisit.Count - 2))
            {
                PlanetsToVisit += " - you have not fuel to visit all you desire planets";
            }
        }

        #endregion[RESULT]


        private void PopulateData()
        {
            string jsonString = DataJson.Data;
            JObject jsonData = JObject.Parse(jsonString);

            _spacecrafts = jsonData
                .SelectTokens("$..spacecrafts")
                .Children()
                .Select(c => c.ToObject<SpacecraftModel>())
                .ToList();

            _planets = jsonData
                .SelectTokens("$..planets")
                .Children()
                .Select(c => c.ToObject<PlanetModel>())
                .ToList();

            _spacecraftSelected = _spacecrafts.First();
            //_planetNames = String.Join(", ", _planets.Select(p => p.Name).ToList());

            PlanetModel? earth = _planets.Find(p => p.PositionIndex == 3);

            if (earth != null)
                _earth = earth;
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
