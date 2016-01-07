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
using System.Drawing.Imaging;
using System.Windows.Shapes;
using System.Drawing;

namespace Recolouring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //Reference this code
            var cm = new ColorMatrix(new float[][]
            {
              new float[] {1, 0, 0, 0, 0},
              new float[] {0, 0, 0, 0, 0},
              new float[] {0, 0, 0, 0, 0},
              new float[] {0, 0, 0, 1, 0},
              new float[] {0, 0, 0, 0, 1}
            });

            var img = System.Drawing.Image.FromFile("C:\\img.png");

            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            var bmp = new Bitmap(img.Width, img.Height);
            var gfx = Graphics.FromImage(bmp);
            var rect = new System.Drawing.Rectangle(0, 0, img.Width, img.Height);

            gfx.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            bmp.Save("D:\\processed.png", ImageFormat.Png);

            Uri uri = new Uri(@"D:\processed.png", UriKind.Absolute);
            ImageSource imgSource = new BitmapImage(uri);

            imgRecol.Source = imgSource;

        }
    }
}
