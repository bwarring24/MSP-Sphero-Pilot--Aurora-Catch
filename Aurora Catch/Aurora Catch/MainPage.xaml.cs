﻿using RobotKit;
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
        DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();
            sp = new SpheroManager();
            DataContext = sp;

        }

        private void swtConnect_Toggled(object sender, RoutedEventArgs e)
        {
            swtConnect.OnContent = "Connecting...";
            if (swtConnect.IsOn)
            {
                if(sp.m_robot == null)
                {
                    sp.SetupRobotConnection();
                }
            }
            else
            {
                sp.ShutdownRobotConnection();
            }

            swtConnect.OnContent = "Connected";
        }

        private void sldOrientation_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(sp.m_robot != null)
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

        private void TextBlock_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {

        }

        private void btnHamburger_Click(object sender, RoutedEventArgs e)
        {
            spltHamburger.IsPaneOpen = !spltHamburger.IsPaneOpen;
        }
    }
}
