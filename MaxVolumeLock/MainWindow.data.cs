using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MaxVolumeLock
{
    /// <summary>
    /// Window state and data 
    /// </summary>
    public partial class MainWindow
    {

        private const string TEXT_BTN_LOCK = "Lock it";
        private const string TEXT_BTN_UNLOCK = "Unlock it";

        private const string ICO_LOCK_PATH = "Assets/lock.ico";
        private const string ICO_UNLOCK_PATH = "Assets/unlock.ico";
        private const int DEFAULT_MAX_VOL = 80;

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

        private bool IsLocked = false;
        private bool IsMinimized = false;
        private bool IsPinRequired = false;
        private int _pin;

        private void minimizeToTray()
        {
            if (!IsMinimized)
            {
                this.Hide();
                IsMinimized = true;
                ico_trayIcon.Visibility = Visibility.Visible;
            }
        }
        private void showWindowNormal()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            IsMinimized = false;
            ico_trayIcon.Visibility = Visibility.Hidden;
        }

        private void lockMaxVolume()
        {
            img_lockUnlock.Source = new BitmapImage(new Uri(ICO_LOCK_PATH, UriKind.Relative));
            IsLocked = true;
            slider_maxVol.IsEnabled = false;
            chb_pinRequired.IsEnabled = false;
            btn_lockMaxVol.Content = TEXT_BTN_UNLOCK;
        }
        private void unlockMaxVolume()
        {
            img_lockUnlock.Source = new BitmapImage(new Uri(ICO_UNLOCK_PATH, UriKind.Relative));
            IsLocked = false;
            slider_maxVol.IsEnabled = true;
            chb_pinRequired.IsEnabled = true;
            btn_lockMaxVol.Content = TEXT_BTN_LOCK;
        }

        public void setPin(int pin)
        {
            _pin = pin;
        }

        public int getPin()
        {
            return _pin;
        }

        public void clearPin()
        {
            _pin = 0;
        }

        public void toggleVolumeLock()
        {
            if (IsLocked) { unlockMaxVolume(); }
            else { lockMaxVolume(); }
        }

        public bool getIsLocked()
        {
            return IsLocked;
        }

    }
}
