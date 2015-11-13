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
            UpdateSelectionButtons(true);
        }
        void CanvasMouseClick(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (isDrawing)
            {
                //Create a new Polyline and add it to the Canvas
                if (currentLine == null && mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Point mousePoint = mouse.GetPosition((IInputElement)sender);
                    currentLine = new Polyline
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = sliderStrokeSize.Value
                    };
                    Canvas canvas = (Canvas)sender;
                    canvas.Children.Add(currentLine);
                    currentLine.Points.Add(mousePoint);
                }
            }
            else
            {
                if(Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Point mousePoint = mouse.GetPosition((IInputElement)sender);
                    Canvas canvas = (Canvas)sender;
                    Polyline selection = GetPolylineAtPoint(canvas, mousePoint);
                    if(selection != null)
                    {
                        canvas.Children.Remove(selection);
                    }
                }
            }
        }
        void CanvasMouseMove(object sender, RoutedEventArgs e)
        {
            if (isDrawing)
            {
                //Use the new mouse position to add points to the Polyline
                MouseEventArgs mouse = (MouseEventArgs)e;
                if (currentLine != null && mouse.LeftButton == MouseButtonState.Pressed)
                {
                    Point mousePoint = mouse.GetPosition((IInputElement)sender);
                    currentLine.Points.Add(mousePoint);
                }
            }
            else
            {

            }
        }
        void CanvasMouseClickEnd(object sender, RoutedEventArgs e)
        {
            if (isDrawing)
            {
                //Finish drawing the current line, and then smooth it and remove redundant points
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
}
