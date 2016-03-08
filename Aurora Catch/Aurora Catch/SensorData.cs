using RobotKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora_Catch
{
    class SensorData
    {
        public accelerometerData accelerometer;

        public SensorData(AccelerometerReading dataAccelerometer)
        {
            accelerometer = new accelerometerData(dataAccelerometer);
        }
    }

    class accelerometerData
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public accelerometerData(float dataX, float dataY, float dataZ)
        {
            x = dataX;
            y = dataY;
            z = dataZ;
        }

        public accelerometerData(AccelerometerReading dataAccelerometer)
        {
            x = dataAccelerometer.X;
            y = dataAccelerometer.Y;
            z = dataAccelerometer.Z;
        }
    }

    class quaternionData
    {
        public float w { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public quaternionData(float dataW, float dataX, float dataY, float dataZ)
        {
            w = dataW;
            x = dataX;
            y = dataY;
            z = dataZ;
        }

        public quaternionData(QuaternionReading dataQuaternion)
        {
            w = dataQuaternion.W;
            x = dataQuaternion.X;
            y = dataQuaternion.Y;
            z = dataQuaternion.Z;
        }
    }

    class velocityData
    {
        public float x { get; set; }
        public float y { get; set; }

        public velocityData(float dataX, float dataY)
        {
            x = dataX;
            y = dataY;
        }

        public velocityData(VelocityReading dataVelocity)
        {
            x = dataVelocity.X;
            y = dataVelocity.Y;
        }
    }

    class LocationData
    {
        public float x { get; set; }
        public float y { get; set; }

        public LocationData(float dataX, float dataY)
        {
            x = dataX;
            y = dataY;
        }

        public LocationData(LocationReading dataLocation)
        {
            x = dataLocation.X;
            y = dataLocation.Y;
        }
    }

    class GyrometerData
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public GyrometerData(float dataX, float dataY, float dataZ)
        {
            x = dataX;
            y = dataY;
            z = dataZ;
        }

        public GyrometerData(GyrometerReading dataGyrometer)
        {
            x = dataGyrometer.X;
            y = dataGyrometer.Y;
            z = dataGyrometer.Z;
        }
    }

    class AttitudeData
    {
        public float pitch { get; set; }
        public float roll { get; set; }
        public float yaw { get; set; }

        public AttitudeData(float dataPitch, float dataRoll, float dataYaw)
        {
            pitch = dataPitch;
            roll = dataRoll;
            yaw = dataYaw;
        }

        public AttitudeData(AttitudeReading dataAttitude)
        {
            pitch = dataAttitude.Pitch;
            roll = dataAttitude.Roll;
            yaw = dataAttitude.Yaw;
        }
    }
}
