using Microsoft.ApplicationInsights;
using RobotKit;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Aurora_Catch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static SpheroManager sp;
        private TelemetryClient tc = new TelemetryClient();

        public MainPage()
        {
            sp = new SpheroManager();
            DataContext = sp;
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync();
            this.InitializeComponent();
            tc.InstrumentationKey = "14a9b42e-9767-4712-97b0-ae8612e1c0dd";
            tc.Context.Session.Id = Guid.NewGuid().ToString();

            // Log a page view:
            tc.TrackPageView("MainPage");

            frmContent.Navigate(typeof(Home));
        }

        private void btnHamburger_Click(object sender, RoutedEventArgs e)
        {
            spltHamburger.IsPaneOpen = !spltHamburger.IsPaneOpen;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Home));
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Play));
        }

        private void btnReplay_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Replay));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Settings));
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(History));
        }
    }
}
