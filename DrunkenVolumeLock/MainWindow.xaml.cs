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
        private const string ICO_LOCK_PATH = "Assets/lock.ico";
        private const string ICO_UNLOCK_PATH = "Assets/unlock.ico";
        private const int DEFAULT_MAX_VOL = 90;

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

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            VolumeActual = (int)Math.Truncate(AudioManager.GetMasterVolume());
            VolumeMax = DEFAULT_MAX_VOL;
            slider_maxVol.Value = VolumeMax;
            picker_endTime.Value = DateTime.Now.AddHours(2);
        }

        private void slider_maxVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VolumeMax = (int)e.NewValue;
        }

        private void lockIt()
        {
            img_lockUnlock.Source = new BitmapImage(new Uri(ICO_LOCK_PATH, UriKind.Relative));
            IsLocked = true;
        }
        private void unlockIt()
        {
            img_lockUnlock.Source = new BitmapImage(new Uri(ICO_UNLOCK_PATH, UriKind.Relative));
            IsLocked = false;
        }

        private void btn_lockMaxVol_Click_1(object sender, RoutedEventArgs e)
        {
            if (IsLocked) { unlockIt(); }
            else { lockIt(); }
        }
    }
}
