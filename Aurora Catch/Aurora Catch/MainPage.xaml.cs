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

        SpheroManager sp;

        public MainPage()
        {
            this.InitializeComponent();
            sp = new SpheroManager();
            DataContext = sp;

            frmContent.Navigate(typeof(Home), sp);
        }

        private void btnHamburger_Click(object sender, RoutedEventArgs e)
        {
            spltHamburger.IsPaneOpen = !spltHamburger.IsPaneOpen;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Home), sp);
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Play), sp);
        }

        private void btnReplay_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Replay), sp);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(typeof(Settings), sp);
        }
    }
}
