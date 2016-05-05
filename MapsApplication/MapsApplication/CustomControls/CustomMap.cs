using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapsApplication.CustomControls
{
    public class CustomMap : Map
    {
        /// <summary>
        /// Event thrown when the user taps on the map
        /// </summary>
        public event EventHandler<MapTapEventArgs> Tapped;

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomMap() { }

        /// <summary>
        /// Constructor that takes a region
        /// </summary>
        /// <param name="region">Region (MapSpan)</param>
        public CustomMap(MapSpan region) : base(region) { }

        #endregion

        #region Event Handler Methods

        public void OnTap(Position coordinate)
        {
            OnTap(new MapTapEventArgs { Position = coordinate });
        }

        protected virtual void OnTap(MapTapEventArgs e)
        {
            var handler = Tapped;

            if (handler != null)
                handler(this, e);
        }

        #endregion
    }

    /// <summary>
    /// Event args used with maps, when the user tap on it
    /// </summary>
    public class MapTapEventArgs : EventArgs
    {
        public Position Position { get; set; }
    }

}

