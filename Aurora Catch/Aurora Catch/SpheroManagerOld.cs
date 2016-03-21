using RobotKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora_Catch
{
    class SpheroManagerOld
    {
        public Sphero sp = null;
        SensorData sensors;

        public SpheroManagerOld()
        {
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
            if (sp == null)
            {
                RobotProvider provider = RobotProvider.GetSharedProvider();
                provider.ConnectRobot(e);
                sp = (Sphero)e;
            }
        }

        private void OnSpheroConnected(object sender, Robot e)
        {
            // Lets go ahead and create all of our data stream methods
            sp.SensorControl.AccelerometerUpdatedEvent += SensorControl_AccelerometerUpdatedEvent;
            sp.SensorControl.AttitudeUpdatedEvent += SensorControl_AttitudeUpdatedEvent;
            sp.SensorControl.GyrometerUpdatedEvent += SensorControl_GyrometerUpdatedEvent;
            sp.SensorControl.LocationUpdatedEvent += SensorControl_LocationUpdatedEvent;
            sp.SensorControl.QuaternionUpdatedEvent += SensorControl_QuaternionUpdatedEvent;
            sp.SensorControl.VelocityUpdatedEvent += SensorControl_VelocityUpdatedEvent;
        }

        private void OnNoSpheroEvent(object sender, EventArgs e)
        {

        }

        private void ShutdownSpheroConnection(object sender, EventArgs e)
        {
            try
            {
                sp.SensorControl.StopAll();
                sp.Sleep();
            }
            catch (Exception)
            {
                
                throw;
            }
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
