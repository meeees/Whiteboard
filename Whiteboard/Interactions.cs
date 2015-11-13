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
    public partial class MainWindow : Window
    {
        bool isDrawing = false;
        bool toolbarOpen = true;

        Polyline GetPolylineAtPoint(Canvas c, Point p)
        {
            foreach (UIElement ui in c.Children)
            {
                if (ui is Polyline)
                {
                    Polyline uiLine = (Polyline)ui;
                    if (uiLine.RenderedGeometry.StrokeContains(new Pen(uiLine.Stroke, uiLine.StrokeThickness), p))
                    {
                        return uiLine;
                    }
                }
            }
            return null;
        }
        //when the drawing or selecting button has been pressed in the toolbar, newState will be true for drawing and false for selecting
        void UpdateSelectionButtons(bool newState)
        {
            if (newState == isDrawing) 
                return;
            isDrawing = newState;
            if(isDrawing)
            {
                buttonDrawing.Background = Brushes.LightGray;
                buttonSelecting.Background = Brushes.White;
            }
            else
            {
                buttonDrawing.Background = Brushes.White;
                buttonSelecting.Background = Brushes.LightGray;
            }
        }
        //takes the selecting or drawing button press and calls UpdateSelectionButtons with the correct newState
        void StateButtonPress(object sender, RoutedEventArgs e)
        {
            UpdateSelectionButtons(sender == buttonDrawing);
        }

        //whenever a board's header is clicked on, close it if it was the middle mouse button
        void CloseBoardFromHeader(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (mouse.MiddleButton == MouseButtonState.Pressed && mouse.LeftButton == MouseButtonState.Released && mouse.RightButton == MouseButtonState.Released)
            {
                TabItem parent = (TabItem)((Control)sender).Parent;
                TabControl grandParent = (TabControl)parent.Parent;
                grandParent.Items.Remove(parent);
            }
        }

        //will hide and unhide the toolbar
        void ButtonToggleToolbar(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button) sender;
            toolbarOpen = !toolbarOpen;
            if(toolbarOpen)
            {
                SetControlVisibility(panelToolBar, Visibility.Visible, senderButton);
                panelToolBar.Width = 140;
                senderButton.Content = "<";
            }
            else
            {
                SetControlVisibility(panelToolBar, Visibility.Collapsed, senderButton);
                panelToolBar.Width = 20;
                senderButton.Content = ">";
            }
        }

        void SetControlVisibility(StackPanel parent, Visibility vis, UIElement ignore=null)
        {
            foreach(UIElement ui in parent.Children)
                {
                    if (ui is StackPanel)
                    {
                        SetControlVisibility((StackPanel) ui, vis);
                    }
                    else if(ui != ignore)
                    {
                        ui.Visibility = vis;
                    }
                }
        }

        //ctrl+W is bound to this, closes the currently active board
        void CloseActiveBoard(object sender, RoutedEventArgs e)
        {
            int selected = tabController.SelectedIndex;
            if(selected != -1)
            {
                tabController.Items.RemoveAt(selected);
            }
        }

        //when the File->New button is clicked
        void Menu_New(object sender, EventArgs e)
        {
            AddNewBoard(sender, (RoutedEventArgs) e);
        }

        //sets up a board with a canvas and gives the canvas the proper mouse bindings, then sets the active board to the new board
        void AddNewBoard(object sender, RoutedEventArgs e)
        {
            TabItem newTab = new TabItem();
            Label headerLabel = new Label();
            headerLabel.Content = String.Format("Board {0}", newTabCount);
            headerLabel.AddHandler(Mouse.MouseDownEvent, new RoutedEventHandler(CloseBoardFromHeader));
            newTab.Header = headerLabel;

            Canvas tabCanvas = new Canvas();
            tabCanvas.Background = Brushes.Transparent;
            tabCanvas.AddHandler(Mouse.MouseDownEvent, new RoutedEventHandler(CanvasMouseClick));
            tabCanvas.AddHandler(Mouse.MouseMoveEvent, new RoutedEventHandler(CanvasMouseMove));
            tabCanvas.AddHandler(Mouse.MouseUpEvent, new RoutedEventHandler(CanvasMouseClickEnd));
            newTab.Content = tabCanvas;
            newTabCount++;
            tabController.Items.Add(newTab);
            tabController.SelectedItem = newTab;
        }

        void MoveActiveF(object sender, RoutedEventArgs e)
        {
            int selected = tabController.SelectedIndex + 1;
            if(selected >= tabController.Items.Count)
            {
                selected = 0;
            }
            tabController.SelectedIndex = selected;
        }

        void MoveActiveB(object sender, RoutedEventArgs e)
        {
            int selected = tabController.SelectedIndex - 1;
            if (selected < 0)
            {
                selected = tabController.Items.Count - 1;
            }
            tabController.SelectedIndex = selected;
        }
        void Menu_Exit(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
