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
        public List<accelerometerData> accelerometer;
        public List<quaternionData> quaternion;
        public List<velocityData> velocity;
        public List<locationData> location;
        public List<gyrometerData> gryometer;
        public List<attitudeData> attitude;

        public SensorData(AccelerometerReading dataAccelerometer, QuaternionReading dataQuarternion, VelocityReading dataVelocity, LocationReading dataLocation, GyrometerReading dataGryometer, AttitudeReading dataAttitude)
        {
            accelerometer.Add(new accelerometerData(dataAccelerometer));
            quaternion.Add(new quaternionData(dataQuarternion));
            velocity.Add(new velocityData(dataVelocity));
            location.Add(new locationData(dataLocation));
            gryometer.Add(new gyrometerData(dataGryometer));
            attitude.Add(new attitudeData(dataAttitude));

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

    class locationData
    {
        public float x { get; set; }
        public float y { get; set; }

        public locationData(float dataX, float dataY)
        {
            x = dataX;
            y = dataY;
        }

        public locationData(LocationReading dataLocation)
        {
            x = dataLocation.X;
            y = dataLocation.Y;
        }
    }

    class gyrometerData
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        public gyrometerData(float dataX, float dataY, float dataZ)
        {
            x = dataX;
            y = dataY;
            z = dataZ;
        }

        public gyrometerData(GyrometerReading dataGyrometer)
        {
            x = dataGyrometer.X;
            y = dataGyrometer.Y;
            z = dataGyrometer.Z;
        }
    }

    class attitudeData
    {
        public float pitch { get; set; }
        public float roll { get; set; }
        public float yaw { get; set; }

        public attitudeData(float dataPitch, float dataRoll, float dataYaw)
        {
            pitch = dataPitch;
            roll = dataRoll;
            yaw = dataYaw;
        }

        public attitudeData(AttitudeReading dataAttitude)
        {
            pitch = dataAttitude.Pitch;
            roll = dataAttitude.Roll;
            yaw = dataAttitude.Yaw;
        }
    }
}
