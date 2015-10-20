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
    public static class WhiteboardCommands
    {
        public static RoutedCommand CloseActiveBoard = new RoutedCommand();
        public static RoutedCommand CreateNewBoard = new RoutedCommand();
        public static RoutedCommand MoveActiveForwards = new RoutedCommand();
        public static RoutedCommand MoveActiveBackwards = new RoutedCommand();
    }
}
