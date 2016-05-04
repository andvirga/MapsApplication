using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapsApplication.Views
{
    public partial class GeocoderPage : ContentPage
    {
        Geocoder geoCoder;
        Label l = new Label();

        public GeocoderPage()
        {
            geoCoder = new Geocoder();

            var b1 = new Button { Text = "Reverse geocode '-32.95, -60.65'" };
            b1.Clicked += async (sender, e) =>
            {
                var fortMasonPosition = new Position(-32.95, -60.65);
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(fortMasonPosition);
                l.Text = "GetAddressesForPositionAsync Run Succesfully \n";
                foreach (var a in possibleAddresses)
                {
                    l.Text += a + "\n";
                }
            };

            var b2 = new Button { Text = "Geocode 'Alvear 1670, Rosario, Argentina'" };
            b2.Clicked += async (sender, e) =>
            {
                var xamarinAddress = "Alvear 1670, Rosario, Argentina";
                var approximateLocation = await geoCoder.GetPositionsForAddressAsync(xamarinAddress);
                l.Text = "GetPositionsForAddressAsync Run Succesfully \n";
                foreach (var p in approximateLocation)
                {
                    l.Text += p.Latitude + ", " + p.Longitude + "\n";
                }
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                Children = {
                    b2,
                    b1,
                    l
                }
            };
        }
    }
}
