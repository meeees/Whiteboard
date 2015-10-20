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
            if (mouse.LeftButton == MouseButtonState.Pressed)
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
            if (mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point mousePoint = mouse.GetPosition((IInputElement)sender);
                currentLine.Points.Add(mousePoint);
            }
        }
        void CloseTabFromHeader(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (mouse.MiddleButton == MouseButtonState.Pressed)
            {
                TabItem parent = (TabItem)((Control)sender).Parent;
                TabControl grandParent = (TabControl)parent.Parent;
                grandParent.Items.Remove(parent);
            }
        }

        void Menu_Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        void Menu_New(object sender, EventArgs e)
        {
            TabItem newTab = new TabItem();
            Label headerLabel = new Label();
            headerLabel.Content = String.Format("Board {0}", newTabCount);
            headerLabel.AddHandler(Mouse.MouseDownEvent, new RoutedEventHandler(CloseTabFromHeader));
            newTab.Header = headerLabel;

            Canvas tabCanvas = new Canvas();
            tabCanvas.Background = Brushes.Transparent;
            tabCanvas.AddHandler(Mouse.MouseDownEvent, new RoutedEventHandler(StartConnectingDots));
            tabCanvas.AddHandler(Mouse.MouseMoveEvent, new RoutedEventHandler(ConnectTheDots));
            newTab.Content = tabCanvas;
            newTabCount++;
            tabController.Items.Add(newTab);
            tabController.SelectedItem = newTab;
        }
    }
}
