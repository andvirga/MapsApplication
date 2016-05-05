using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using MapsApplication.CustomControls;
using MapsApplication.Droid.CustomRenderers;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapsApplication.Droid.CustomRenderers
{
    /// <summary>
    /// Renderer for the xamarin map.
    /// Enable user to get a position by taping on the map.
    /// </summary>
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        // We use a native google map for Android
        private GoogleMap _map;

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;

            if (_map != null)
                _map.MapClick += googleMap_MapClick;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            if (_map != null)
                _map.MapClick -= googleMap_MapClick;

            base.OnElementChanged(e);

            if (Control != null)
                ((MapView)Control).GetMapAsync(this);
        }

        private void googleMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            ((CustomMap)Element).OnTap(new Position(e.Point.Latitude, e.Point.Longitude));
        }
    }
}