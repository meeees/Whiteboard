﻿using System;
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
        void CloseBoardFromHeader(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouse = (MouseEventArgs)e;
            if (mouse.MiddleButton == MouseButtonState.Pressed)
            {
                TabItem parent = (TabItem)((Control)sender).Parent;
                TabControl grandParent = (TabControl)parent.Parent;
                grandParent.Items.Remove(parent);
            }
        }
        void CloseActiveBoard(object sender, RoutedEventArgs e)
        {
            int selected = tabController.SelectedIndex;
            if(selected != -1)
            {
                tabController.Items.RemoveAt(selected);
            }
        }

        void Menu_New(object sender, EventArgs e)
        {
            AddNewBoard(sender, (RoutedEventArgs) e);
        }

        void AddNewBoard(object sender, RoutedEventArgs e)
        {
            TabItem newTab = new TabItem();
            Label headerLabel = new Label();
            headerLabel.Content = String.Format("Board {0}", newTabCount);
            headerLabel.AddHandler(Mouse.MouseDownEvent, new RoutedEventHandler(CloseBoardFromHeader));
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