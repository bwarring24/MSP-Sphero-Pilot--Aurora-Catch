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
        Sphero mySphero;

        public MainPage()
        {
            this.InitializeComponent();
            setupSphero();
        }

        private void setupSphero()
        {
            RobotProvider provider = RobotProvider.GetSharedProvider();
            provider.DiscoveredRobotEvent += OnSpheroDiscovered;
            provider.NoRobotsEvent += OnNoSpheroEvent;
            provider.ConnectedRobotEvent += OnSpheroConnected;
            provider.FindRobots();
        }


        private void OnSpheroDiscovered(object sender, Robot e)
        {
            blkStatus.Text = "Discovered Sphero: " + e.BluetoothName;

            if(mySphero == null)
            {
                RobotProvider provider = RobotProvider.GetSharedProvider();
                provider.ConnectRobot(e);
                mySphero = (Sphero)e;
            }
        }

        private void OnSpheroConnected(object sender, Robot e)
        {
            blkStatus.Text = "Connected to Sphero: " + e.BluetoothName;
        }

        private void OnNoSpheroEvent(object sender, EventArgs e)
        {
            blkStatus.Text = "No Sphero connected :(";
        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            mySphero.SetRGBLED(255, 0, 0);
        }
    }


}
