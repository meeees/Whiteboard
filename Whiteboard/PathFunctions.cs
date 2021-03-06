﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Whiteboard
{
    class PathFunctions
    {
        //how far apart points need to be for them to be considered significant
        private static float mostSignifigance = 1f;

        //remove all points not considered significant
        public static List<Point> RemoveInsignificants(List<Point> path)
        {
            List<Point> reduced = new List<Point>(path);
            for(int i = 1; i < reduced.Count; i++)
            {
                if(Distance(reduced[i - 1], reduced[i]) < mostSignifigance)
                {
                    reduced.RemoveAt(i);
                    i--;
                }
            }
            Console.WriteLine(String.Format("Before {0}, After {1}", path.Count, reduced.Count));
            return reduced;
        }
        
        //simple distance between two points
        public static double Distance(Point p, Point p2)
        {
            return Math.Sqrt((p.X - p2.X) * (p.X - p2.X) + (p.Y - p2.Y) * (p.Y - p2.Y));
        }

        //averages points with their neighbors to remove jagedness from the final paths
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
                    if(j < 0 || j >= path.Count -1)
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
