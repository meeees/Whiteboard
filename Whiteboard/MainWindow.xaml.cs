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
        Point mousePoint = new Point(-1, -1);
        public MainWindow()
        {
            InitializeComponent();
        }
        void StartConnectingDots(object sender, MouseEventArgs mouse)
        {
            mousePoint = mouse.GetPosition(null);
        }
        void ConnectTheDots(object sender, MouseEventArgs mouse)
        {
            if (mouse.LeftButton == MouseButtonState.Pressed)
            {
                Canvas currentCanvas = (Canvas)sender;
                double x = mouse.GetPosition(null).X;
                double y = mouse.GetPosition(null).Y;
                Line l = new Line
                {
                    X1 = mousePoint.X,
                    X2 = x,
                    Y1 = mousePoint.Y,
                    Y2 = y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                currentCanvas.Children.Add(l);
                mousePoint = new Point(x, y);
            }
        }
    }
}
