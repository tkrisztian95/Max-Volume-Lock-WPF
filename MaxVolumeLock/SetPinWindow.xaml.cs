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
using System.Windows.Shapes;

namespace MaxVolumeLock
{
    /// <summary>
    /// Interaction logic for SetPinWindow.xaml
    /// </summary>
    public partial class SetPinWindow : Window
    {
        private MainWindow mainWindow;

        public SetPinWindow(MainWindow parent)
        {
            InitializeComponent();
            this.mainWindow = parent;
        }

        private void cancelButton1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lockWithPinButton1_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.setPin(int.Parse(pin.Password));
            mainWindow.toggleVolumeLock();
            this.Close();
        }
    }
}
