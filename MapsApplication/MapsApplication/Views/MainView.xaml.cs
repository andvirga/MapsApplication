﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapsApplication.Views
{
    public partial class MainView : ContentPage
    {
        //--Geocoder Instance.
        Geocoder geocoder;

        /// <summary>
        /// Constructor
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            geocoder = new Geocoder();
            this.InitializeMap();
        }

        #region Map Methods

        /// <summary>
        /// Initializes the map with the entire map of Argentina.
        /// </summary>
        public void InitializeMap()
        {
            //--Matching the event 'Tapped' with the method 'MyMap_Tapped'
            MyMap.Tapped += MyMap_Tapped;

            double latitudeARG = -38.416097d;
            double longitudARG = -63.616672d;

            var mapSpan = MapSpan.FromCenterAndRadius(new Position(latitudeARG, longitudARG), Distance.FromKilometers(2000));

            this.MyMap.MoveToRegion(mapSpan);
        }

        /// <summary>
        /// Map Tapped Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyMap_Tapped(object sender, CustomControls.MapTapEventArgs e)
        {
            this.GetNearestAddress(e);
        }

        /// <summary>
        /// Changes the map to Street View Mode.
        /// </summary>
        public void ChangeToStreetView(object sender, EventArgs e)
        {
            this.MyMap.MapType = MapType.Street;
        }

        /// <summary>
        /// Changes the map to Satellite View Mode.
        /// </summary>
        public void ChangeToSatelliteView(object sender, EventArgs e)
        {
            this.MyMap.MapType = MapType.Satellite;
        }

        /// <summary>
        /// Changes the map to Hybrid View Mode.
        /// </summary>
        public void ChangeToHybridView(object sender, EventArgs e)
        {
            this.MyMap.MapType = MapType.Hybrid;
        }

        /// <summary>
        /// Changes the Zoom level in the map according the value of the Slider Control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangeMapZoomLevel(object sender, EventArgs e)
        {
            var zoomLevel = this.zoomSlider.Value; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            if (MyMap.VisibleRegion != null)
                MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        /// <summary>
        /// Moves the focus of the map to the AAJ US Office Location.
        /// </summary>
        public void MoveToAAJOffice(object sender, EventArgs e)
        {
            double latitude = 26.2055651d;
            double longitude = -80.1513172d;

            var position = new Position(latitude, longitude);

            var mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5d));

            this.MyMap.MoveToRegion(mapSpan);

            var AAJLocationPin = new Pin
            {
                Position = position,
                Label = "AAJ Technologies",
                Address = "6301 NW 5th Way #1700, Fort Lauderdale, FL, USA",
                Type = PinType.Place
            };

            this.MyMap.Pins.Add(AAJLocationPin);
        }

        /// <summary>
        /// Moves the focus of the map to one specific address entered by the user.
        /// </summary>
        public async void MoveToSpecificAddress(object sender, EventArgs e)
        {
            var approximateLocations = await geocoder.GetPositionsForAddressAsync(this.txtAddress.Text);
            if (approximateLocations.Count() > 0)
            {
                var position = approximateLocations.First();

                var mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5d));

                this.MyMap.MoveToRegion(mapSpan);

                var FoundLocationPin = new Pin
                {
                    Position = position,
                    Label = "Location Found",
                    Address = this.txtAddress.Text,
                    Type = PinType.Place
                };

                this.MyMap.Pins.Add(FoundLocationPin);
            }
            else
            {
                await DisplayAlert("Error", "No places were found for the entered address", "Ok");
            }
        }

        /// <summary>
        /// Get the Nearest Address from a selected position.
        /// </summary>
        /// <param name="mapArgs">MapTapEventArgs (Contains the Position)</param>
        public async void GetNearestAddress(CustomControls.MapTapEventArgs mapArgs)
        {
            var approximateAddresses = await geocoder.GetAddressesForPositionAsync(mapArgs.Position);

            if (approximateAddresses.Count() > 0)
                await DisplayAlert("Message", String.Format("Nearest Address: {0}", approximateAddresses.First().ToString()), "Ok");
            else
                await DisplayAlert("D'Oh!", "No near addresses were found from this point", "Ok");
        }

        #endregion
    }
}
