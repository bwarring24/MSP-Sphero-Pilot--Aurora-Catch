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
        SensorData sensors;
        DispatcherTimer timer = new DispatcherTimer();

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

            // Lets go ahead and create all of our data stream methods
            mySphero.SensorControl.AccelerometerUpdatedEvent += SensorControl_AccelerometerUpdatedEvent;
            mySphero.SensorControl.AttitudeUpdatedEvent += SensorControl_AttitudeUpdatedEvent;
            mySphero.SensorControl.GyrometerUpdatedEvent += SensorControl_GyrometerUpdatedEvent;
            mySphero.SensorControl.LocationUpdatedEvent += SensorControl_LocationUpdatedEvent;
            mySphero.SensorControl.QuaternionUpdatedEvent += SensorControl_QuaternionUpdatedEvent;
            mySphero.SensorControl.VelocityUpdatedEvent += SensorControl_VelocityUpdatedEvent;
        }

        private void OnNoSpheroEvent(object sender, EventArgs e)
        {
            blkStatus.Text = "No Sphero connected :(";
        }

        private void SensorControl_VelocityUpdatedEvent(object sender, VelocityReading e)
        {
            sensors.velocity.Add(new velocityData(e));
        }

        private void SensorControl_QuaternionUpdatedEvent(object sender, QuaternionReading e)
        {
            sensors.quaternion.Add(new quaternionData(e));
        }

        private void SensorControl_LocationUpdatedEvent(object sender, LocationReading e)
        {
            sensors.location.Add(new locationData(e));
        }

        private void SensorControl_GyrometerUpdatedEvent(object sender, GyrometerReading e)
        {
            sensors.gryometer.Add(new gyrometerData(e));
        }

        private void SensorControl_AttitudeUpdatedEvent(object sender, AttitudeReading e)
        {
            sensors.attitude.Add(new attitudeData(e));
        }

        private void SensorControl_AccelerometerUpdatedEvent(object sender, AccelerometerReading e)
        {
            sensors.accelerometer.Add(new accelerometerData(e));
        }
    }
}
