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
        #endregion[SPACECRAFT]


        #region[PASSENGERS]

        private int _currentPassengers = 1;
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

        private PlanetModel? _selectedPlanet = null;
        public PlanetModel? SelectedPlanet
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
                    _planetsSelected.Add(planetSelected);
                    PlanetNames = String.Join(", ", _planetsSelected.Select(p => p.Name).ToList());

                    planetSelected = new PlanetModel();
                }

                _selectedPlanet = planetSelected;
                OnPropertyChanged("SelectedPlanet");

            }
        }

        public void ClearPlanetList()
        {
            _planetsSelected = new List<PlanetModel>();
            PlanetNames = String.Empty;
            _selectedPlanet = null;

            OnPropertyChanged("PlanetNames");
            OnPropertyChanged("SelectedPlanet");

        }

        #endregion[PLANETS]

        #region[RESULT]
        private string _travelResult = String.Empty;
        public string TravelResult
        {
            get { return _travelResult; }
            set
            {
                _travelResult = value;
                OnPropertyChanged("TravelResult");
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

            TravelResult result = _spaceTravelApplication.Travel(spacecraft, planets, earth, _optimize);

            TravelResult = result.Message;
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
