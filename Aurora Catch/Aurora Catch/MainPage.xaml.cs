using Microsoft.ApplicationInsights;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Aurora_Catch
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        SpheroManager sp;
        bool rolling = false;
        private TelemetryClient tc = new TelemetryClient();
        DispatcherTimer timer = new DispatcherTimer();
        List<double> accX = new List<double>();
        List<double> accY = new List<double>();
        List<double> accZ = new List<double>();
        int count = 0;

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

            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Tick += Timer_Tick;

            // frmContent.Navigate(typeof(Home));
        }

        private void btnHamburger_Click(object sender, RoutedEventArgs e)
        {
            //spltHamburger.IsPaneOpen = !spltHamburger.IsPaneOpen;
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            //frmContent.Navigate(typeof(Home));
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            //frmContent.Navigate(typeof(Play));
        }

        private void btnReplay_Click(object sender, RoutedEventArgs e)
        {
            //frmContent.Navigate(typeof(Replay));
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            //frmContent.Navigate(typeof(Settings));
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            //frmContent.Navigate(typeof(History));
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            sp.m_robot.SetBackLED((float)1);
            sp.m_robot.SensorControl.AccelerometerUpdatedEvent += SensorControl_AccelerometerUpdatedEvent;
            sp.m_robot.SensorControl.VelocityUpdatedEvent += SensorControl_VelocityUpdatedEvent;
        }

        private void SensorControl_VelocityUpdatedEvent(object sender, VelocityReading e)
        {
            blkGyrometer.Text = "X: " + e.X.ToString() + Environment.NewLine + "Y: " + e.Y.ToString();
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

        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SensorControl_AccelerometerUpdatedEvent(object sender, AccelerometerReading e)
        {
            if (accX.Count() < 10)
            {

                if (count > 60)
                {
                    accX.Clear();
                    accY.Clear();
                    accZ.Clear();
                    accX.Add(e.X);
                    accY.Add(e.Y);
                    accZ.Add(e.Z);
                    count = 0;
                }
                else
                {

                    accX.Add(e.X);


                    accY.Add(e.Y);

                    accZ.Add(e.Z);


                }
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
                if(index > accY.Count()-2)
                {
                    index = index - 1;
                }else if(index < 2){
                    index = index + 1;
                }
                q3 = (accY[index - 1] + accY[index] + accY[index + 1]) / 3;

                blkGyrometer.Text = "TOTAL: " + total.ToString();
                blkLocation.Text = "INDEX: " + index + " = " + accY[index].ToString();
                blkLocation.Text += Environment.NewLine + "Q3: " + q3.ToString();

                if (accY[index] > 0.2)
                {
                    rolling = true;
                    sp.m_robot.Roll(0, ((float)((Math.Sqrt((accY[index] * 250))) - 1) / 4) / 10);
                    timer.Interval = new TimeSpan(0, 0, 0, ((int)(Math.Sqrt((accY[index] * 250))) - 1) / 4);
                    timer.Start();
                    sp.m_robot.SensorControl.StopAll();
                }
                accX.Clear();
                accY.Clear();
                accZ.Clear();
            }
            count++;
        }

        private void Timer_Tick(object sender, object e)
        {
            sp.m_robot.Roll(0, 0.0f);

            Task.Delay(1000).Wait();
            rolling = false;
            sp.m_robot.SensorControl.AccelerometerUpdatedEvent += SensorControl_AccelerometerUpdatedEvent;
            timer.Stop();
        }
    }
}
