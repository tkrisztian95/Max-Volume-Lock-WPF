﻿using NAudio.CoreAudioApi;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrunkenVolumeLock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string TEXT_BTN_LOCK = "Lock it";
        private const string TEXT_BTN_UNLOCK = "Unlock it";

        private const string ICO_LOCK_PATH = "Assets/lock.ico";
        private const string ICO_UNLOCK_PATH = "Assets/unlock.ico";
        private const int DEFAULT_MAX_VOL = 80;

        private bool IsLocked = false;

        private int _actualVol;
        private int VolumeActual
        {
            get { return _actualVol; }
            set
            {
                _actualVol = value;
                lbl_actualVol.Content = _actualVol + "%";
            }
        }

        private int _maxVol;
        private int VolumeMax
        {
            get { return _maxVol; }
            set
            {
                _maxVol = value;
                lbl_maxVol.Content = _maxVol + "%";
            }
        }

        private MMDevice dev;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            NAudio.CoreAudioApi.MMDeviceEnumerator enumer = new NAudio.CoreAudioApi.MMDeviceEnumerator();
            dev = enumer.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            dev.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;

            VolumeActual = (int)(dev.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
            VolumeMax = DEFAULT_MAX_VOL;

            slider_maxVol.Value = VolumeMax;
            picker_endTime.Value = DateTime.Now.AddHours(2);
        }

        private void slider_maxVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VolumeMax = (int)e.NewValue;
        }

        private void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            Dispatcher.Invoke(() =>
            {
                float volume = (data.MasterVolume * 100);
                VolumeActual = (int)volume;
            });

            if (IsLocked && VolumeActual > VolumeMax)
            {
                float volume = VolumeMax / 100f;
                dev.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
            }
        }

        private void btn_lockMaxVol_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsLocked) { unlockIt(); }
            else { lockIt(); }
        }


        private void lockIt()
        {
            img_lockUnlock.Source = new BitmapImage(new Uri(ICO_LOCK_PATH, UriKind.Relative));
            IsLocked = true;
            slider_maxVol.IsEnabled = false;
            chb_pinRequired.IsEnabled = false;
            btn_lockMaxVol.Content = TEXT_BTN_UNLOCK;
        }
        private void unlockIt()
        {
            img_lockUnlock.Source = new BitmapImage(new Uri(ICO_UNLOCK_PATH, UriKind.Relative));
            IsLocked = false;
            slider_maxVol.IsEnabled = true;
            chb_pinRequired.IsEnabled = true;
            btn_lockMaxVol.Content = TEXT_BTN_LOCK;
        }

    }
}
