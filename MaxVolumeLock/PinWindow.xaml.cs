﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaxVolumeLock
{
    /// <summary>
    /// Interaction logic for SetPinWindow.xaml
    /// </summary>
    public partial class PinWindow : Window
    {
        private MainWindow mainWindow;

        public PinWindow(MainWindow parent)
        {
            this.mainWindow = parent;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (mainWindow.getIsLocked())
            {
                toggleLockWithPinButton.Content = "Unlock with PIN";
            }
            else
            {
                toggleLockWithPinButton.Content = "Lock with PIN";
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void toggleLockWithPinButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mainWindow.getIsLocked())
            {
                mainWindow.setPin(int.Parse(pinInput.Password));
            }
            else
            {
                if (mainWindow.getPin() == int.Parse(pinInput.Password))
                {
                    mainWindow.clearPin();
                }
                else
                {
                    pinInputErrorTextBlock.Visibility = Visibility.Visible;
                    return;
                }
            }
            mainWindow.toggleVolumeLock();
            this.Close();
        }
    }
}
