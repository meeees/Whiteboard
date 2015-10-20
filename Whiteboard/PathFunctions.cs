using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Whiteboard
{
    class PathFunctions
    {
        public static List<Point> SmoothPath(List<Point> path, int depth)
        {
            List<Point> smoothed = new List<Point>();
            for(int i = 0; i < path.Count; i++)
            {
                double x = 0;
                double y = 0;
                int count = 0;
                for(int j = i - depth; j <= i + depth * 2; j++)
                {
                    if(j < 0)
                    {
                        x += path[i].X;
                        y += path[i].Y;
                    }
                    else if(j >= path.Count -1)
                    {
                        x += path[i].X;
                        y += path[i].Y;
                    }
                    else
                    {
                        x += path[j].X;
                        y += path[j].Y;
                    }
                    count++;
                }
                x /= count;
                y /= count;
                smoothed.Add(new Point(x, y));
            }
            return smoothed;
        }
    }
}
