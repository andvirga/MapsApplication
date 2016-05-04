using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MapsApplication.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("IhOwKwbdVUr2t4cn9KoB~gHjVyHLjHsj03VwJ2iqWqw~Amz-JWLI_1AIu9zfjeB1eUCMEmTETIWS5Z3iEdW2_Z2fMZCOoIZRGIX2UCvdahhP");

            LoadApplication(new MapsApplication.App());
        }
    }
}
