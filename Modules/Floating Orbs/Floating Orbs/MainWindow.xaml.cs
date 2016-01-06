using Floating_Orbs.com.orb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Floating_Orbs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private OrbHandler orbHandler;

        public static TextBlock orbCounter;

        public MainWindow()
        {
            
            InitializeComponent();
            orbHandler = new OrbHandler(orbGrid, 0);
            orbCounter = txtOrbCount;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            orbHandler.clear();
        }

        private void chbEnabled_Checked(object sender, RoutedEventArgs e)
        {
            orbHandler.start();
        }

        private void chbEnabled_Unchecked(object sender, RoutedEventArgs e)
        {
            orbHandler.clear();
            orbHandler.stop();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                orbHandler.Size = (short)sldAmount.Value;
                txtValue.Text = "" + (short)sldAmount.Value;
            }
            catch { }
        }



    }
}
