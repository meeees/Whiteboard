using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Whiteboard
{
    public static class WhiteboardCommands
    {
        public static RoutedCommand CloseActiveBoard = new RoutedCommand();
        public static RoutedCommand CreateNewBoard = new RoutedCommand();
        public static RoutedCommand MoveActiveForwards = new RoutedCommand();
        public static RoutedCommand MoveActiveBackwards = new RoutedCommand();
        public static RoutedCommand SaveActiveBoard = new RoutedCommand();
        public static RoutedCommand LoadSavedBoard = new RoutedCommand();
    }
}
