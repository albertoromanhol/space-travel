using Caliburn.Micro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpaceTravel.Logic;
using SpaceTravel.WPF.Database;
using SpaceTravel.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace SpaceTravel.WPF.ViewModels
{
    public class SpaceTravelViewModel : Conductor<object>
    {
        private SpaceTravelApplication _spaceTravelApplication;
        public SpaceTravelViewModel()
        {
            _spaceTravelApplication = new SpaceTravelApplication();

            PopulateSpacecrafts();
        }

        #region[HEADER]

        #endregion[HEADER]

        #region[SPACECRAFT]
        private List<Spacecraft?> _spacecrafts;
        private List<Planet?> _planets;

        private void PopulateSpacecrafts()
        {
            string jsonString = DataJson.Data;

            JObject jsonData = JObject.Parse(jsonString);
            _spacecrafts = jsonData
                .SelectTokens("$..spacecrafts")
                .Children()
                .Select(c => c.ToObject<Spacecraft>())
                .ToList();

            _planets = jsonData
                .SelectTokens("$..planets")
                .Children()
                .Select(c => c.ToObject<Planet>())
                .ToList();
        }


        public List<Spacecraft> Spacecrafts
        {
            get { return _spacecrafts; }
        }

        private Spacecraft _SpacecraftSelected;
        public Spacecraft SpacecraftSelected
        {
            get
            {
                return _SpacecraftSelected;
            }
            set
            {
                _SpacecraftSelected = value;
                HandleSpacecraftChange();
            }
        }

        private int _CurrentPassengers = 0;
        private int _MaxPassengers = 10;

        private void HandleSpacecraftChange()
        {
            MaxPassengers = _SpacecraftSelected.Capacity;
        }
        
        public int MaxPassengers
        {
            get { return _MaxPassengers; }
            set
            {
                _MaxPassengers = value;
            }
        }

        public int CurrentPassengers
        {
            get
            {
                return _CurrentPassengers;
            }
            set
            {
                _CurrentPassengers = value;
            }
        }

        #endregion[SPACECRAFT]

        #region[PLANETS]

        #endregion[PLANETS]
    }
}
