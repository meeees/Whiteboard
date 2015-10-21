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

namespace Whiteboard
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Polyline currentLine;
        int newTabCount = 1;
        public MainWindow()
        {
            InitializeComponent();
        }
        void StartConnectingDots(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (currentLine == null && mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePoint = mouse.GetPosition((IInputElement)sender);
                currentLine = new Polyline
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                Canvas canvas = (Canvas)sender;
                canvas.Children.Add(currentLine);
                currentLine.Points.Add(mousePoint);
            }
        }
        void ConnectTheDots(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (currentLine != null && mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePoint = mouse.GetPosition((IInputElement)sender);
                currentLine.Points.Add(mousePoint);
            }
        }
        void StopConnectingDots(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (currentLine != null && mouse.LeftButton == MouseButtonState.Released)
            {
                PointCollection collect = new PointCollection(PathFunctions.SmoothPath(PathFunctions.RemoveInsignificants(currentLine.Points.ToList<Point>()), 2));
                currentLine.Points = collect;
                currentLine = null;
            }
        }
    }
}
