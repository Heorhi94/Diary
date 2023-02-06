using System;
using System.Collections.Generic;
using System.IO;
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
using Weekly_Diary.InkCanvasDraw;

namespace Weekly_Diary
{
    
    public partial class DrawWindow : Window
    {
        MainWindow mainWindow;
        private ColorRGB colorRGB = new ColorRGB();
        string path = $"{Environment.CurrentDirectory}\\inkImage.png";

        public DrawWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            colorRGB.mcolor = new ColorRGB();
            colorRGB.mcolor.red = 0;
            colorRGB.mcolor.green = 0;
            colorRGB.mcolor.blue = 0;
            lColor.Background = new SolidColorBrush(Color.FromRgb(colorRGB.mcolor.red, colorRGB.mcolor.green, colorRGB.mcolor.blue));
        }

        private void clearDraw_Click(object sender, RoutedEventArgs e)
        {
            draw.Strokes.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {          
            SaveInkImage();            
        }

        public void SaveInkImage()
        {                     
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)draw.Width, (int)draw.Height, 96d, 96d, PixelFormats.Pbgra32);
            renderBitmap.Render(draw);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                encoder.Save(stream);
            }
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(path, UriKind.Relative);
            bitmapImage.EndInit();
            Image image = new Image();
            image.Source = bitmapImage;
            InlineUIContainer container = new InlineUIContainer(image);
            mainWindow.textDiary.CaretPosition.InsertTextInRun("");
            mainWindow.textDiary.CaretPosition.Paragraph.Inlines.Add(container);
            this.Close();
        }

        private void closeDraw_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void rgbColor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            string name = slider.Name;
            double value = slider.Value;
            if (name.Equals("red"))
            {
                colorRGB.mcolor.red = Convert.ToByte(value);
            }
            if (name.Equals("green"))
            {
                colorRGB.mcolor.green = Convert.ToByte(value);
            }
            if (name.Equals("blue"))
            {
                colorRGB.mcolor.blue = Convert.ToByte(value);
            }

            colorRGB.clr = Color.FromRgb(colorRGB.mcolor.red, colorRGB.mcolor.green, colorRGB.mcolor.blue);
            lColor.Background = new SolidColorBrush(Color.FromRgb(colorRGB.mcolor.red, colorRGB.mcolor.green, colorRGB.mcolor.blue));
            draw.DefaultDrawingAttributes.Color = colorRGB.clr;
        }
    }
}
