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
using System.IO;
using Microsoft.Win32;

namespace Whiteboard
{
    public partial class MainWindow : Window
    {
        void Menu_Save(object sender, EventArgs e)
        {
            if(tabController.SelectedIndex == -1)
            {
                MessageBox.Show("No boards are active to be saved.");
                return;
            }
            TabItem currentBoard = (TabItem) tabController.Items[tabController.SelectedIndex];
            SaveFileDialog dialog = new SaveFileDialog();
            Label header = (Label)currentBoard.Header;
            dialog.FileName = (string) header.Content;
            dialog.DefaultExt = ".wtb";
            dialog.Filter = "Whiteboard Files (.wtb)|*.wtb";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                string fileName = dialog.FileName;
                BinaryWriter write = new BinaryWriter(new FileStream(fileName, FileMode.Create));
                Canvas canv = (Canvas)currentBoard.Content;
                List<Polyline> linesToWrite = new List<Polyline>();
                foreach (UIElement ele in canv.Children)
                {
                    if(ele is Polyline)
                    {
                        linesToWrite.Add((Polyline)ele); 
                    }
                }
                write.Write(linesToWrite.Count); //Write the total number of lines to the file
                foreach(Polyline line in linesToWrite)
                {
                    List<Point> points = line.Points.ToList();
                    write.Write(line.StrokeThickness); //Write the thickness of the stroke
                    Color col = ((SolidColorBrush)line.Stroke).Color;
                    //Write the bytes of the color
                    write.Write(col.A);
                    write.Write(col.R);
                    write.Write(col.G);
                    write.Write(col.B);
                    write.Write(points.Count); //write how many points the line has
                    //loop through all the points and write them
                    foreach(Point p in points)
                    {
                        write.Write(p.X); //Write X coord to the file
                        write.Write(p.Y); //Write Y coord to the file
                    }
                }
                write.Close();
            }
        }
    }

}