using RobotKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Aurora_Catch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        SpheroManager sp = MainPage.sp;
        int direction = 0;
        bool rolling = false;
        DispatcherTimer timer = new DispatcherTimer();
        List<double> accX = new List<double>();
        List<double> accY = new List<double>();
        List<double> accZ = new List<double>();
        int count = 0;


        public Home()
        {
            DataContext = sp;
            this.InitializeComponent();

            timer.Tick += Timer_Tick;
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            sp.m_robot.SetBackLED((float)1);
            sp.m_robot.SensorControl.AccelerometerUpdatedEvent += SensorControl_AccelerometerUpdatedEvent;
        }

        private void SensorControl_AccelerometerUpdatedEvent(object sender, AccelerometerReading e)
        {
            if (!rolling)
            {
                if (accX.Count() < 10)
                {
                        accX.Add(e.X);
                        accY.Add(e.Y);
                        accZ.Add(e.Z);
                }
                else
                {
                    blkLocation.Text = accX[accX.Count() - 1].ToString();
                    double total = 0.0;

                    for (int i = 0; i < accY.Count(); i++)
                    {
                        total += accY[i];
                    }

                    int index = 0;
                    for (int i = 0; i < accY.Count(); i++)
                    {
                        if (accY[index] < accY[i])
                        {
                            index = i;
                        }
                    }

                    double q3 = 0;
                    if (index > accY.Count() - 2)
                    {
                        index = index - 1;
                    }
                    else if (index < 2)
                    {
                        index = index + 1;
                    }
                    q3 = (accY[index - 1] + accY[index] + accY[index + 1]) / 3;

                    blkGyrometer.Text = "TOTAL: " + total.ToString();
                    blkLocation.Text = "INDEX: " + index + " = " + accY[index].ToString();
                    blkLocation.Text += Environment.NewLine + "Q3: " + q3.ToString();

                    if (accY[index] > 0.2)
                    {
                        sp.m_robot.SensorControl.StopAll();
                        sp.m_robot.SensorControl.AccelerometerUpdatedEvent -= SensorControl_AccelerometerUpdatedEvent;
                        rolling = true;
                        SpheroRollForward(accY[index]);
                    }
                    accX.Clear();
                    accY.Clear();
                    accZ.Clear();
                }
                count++;
            }
        }

        private async void SpheroRollForward(double accY)
        {
            sp.m_robot.Roll(0, ((float)((Math.Sqrt((accY * 250))) - 1) / 4) / 10);
            timer.Interval = new TimeSpan(0, 0, 0, ((int)(Math.Sqrt((accY * 250))) - 1) / 4);
            direction = 0;
            timer.Start();

            await Task.Delay((((int)(Math.Sqrt((accY * 250))) - 1) / 4) * 1000);
            timer.Stop();

            SpheroRollBack(accY);
        }

        private async void SpheroRollBack(double accY)
        {
            sp.m_robot.Roll(180, (float)0);
            await Task.Delay(100);
            sp.m_robot.Roll(180, ((float)(Math.Sqrt((accY * 250))) / 4) / 10);
            timer.Interval = new TimeSpan(0, 0, 0, ((int)(Math.Sqrt((accY * 250))) - 1) / 4);
            direction = 1;
            timer.Start();
            
        }


        private async void Timer_Tick(object sender, object e)
        {
            // When we tick once we have hit the time allowed to roll.
            // We are using the timer to keep track of time.
            sp.m_robot.Roll(0, 0.0f);
            timer.Stop();
            await Task.Delay(3000);

            if (direction == 1)
            {
                direction = 0;
                rolling = false;
                sp.m_robot.SensorControl.AccelerometerUpdatedEvent += SensorControl_AccelerometerUpdatedEvent;
            }

            
        }

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            sp.m_robot.SetBackLED((float)1);
            sp.m_robot.SensorControl.AccelerometerUpdatedEvent += SensorControl_AccelerometerUpdatedEvent;
            
        }

        private void sldOrientation_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (sp.m_robot != null)
            {
                sp.m_robot.SetRGBLED(0, 0, 0);
                sp.m_robot.SetBackLED(1f);
                sp.m_robot.Roll((int)sldOrientation.Value, 0f);
            }
        }

        private void sldOrientation_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (sp.m_robot != null)
            {
                sp.m_robot.SetBackLED(0);
                sp.m_robot.SetHeading(0);
            }
        }


        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            sp.m_robot.Roll(0, (float)0);
        }
    }
}
