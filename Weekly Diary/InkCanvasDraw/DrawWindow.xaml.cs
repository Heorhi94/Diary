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

namespace Weekly_Diary
{
    /// <summary>
    /// Логика взаимодействия для DrawWindow.xaml
    /// </summary>
    public partial class DrawWindow : Window
    {
        MainWindow mainWindow;
        int val = 0;

        public DrawWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void clearDraw_Click(object sender, RoutedEventArgs e)
        {
            draw.Strokes.Clear();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            // Save
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)draw.Width, (int)draw.Height, 96d, 96d, PixelFormats.Pbgra32);
            renderBitmap.Render(draw);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream stream = new FileStream($"D:\\StudiesProject\\C#\\klimekAlgor\\Project\\Weekly Diary\\Weekly Diary\\InkCanvasDraw\\inkImage{val}.png", FileMode.Create))
            {
                encoder.Save(stream);
            }
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri($"D:\\StudiesProject\\C#\\klimekAlgor\\Project\\Weekly Diary\\Weekly Diary\\InkCanvasDraw\\inkImage{val}.png", UriKind.Relative);
            bitmapImage.EndInit();
            Image image = new Image();
            image.Source = bitmapImage;
            InlineUIContainer container = new InlineUIContainer(image);
            mainWindow.textDiary.CaretPosition.InsertTextInRun("");
            mainWindow.textDiary.CaretPosition.Paragraph.Inlines.Add(container);
            val++;
            Close();
        }

        private void closeDraw_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
