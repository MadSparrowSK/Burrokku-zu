﻿using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Connect
{
    public class ConnectionLine
    {
        public Ellipse circle_left;
        public Ellipse circle_right;
        public Ellipse circle_top;
        public Ellipse circle_bottom;

        public bool left_left = false;
        public bool left_right = false;

        public bool right_left = false;
        public bool right_right = false;

        public bool top_left = false;
        public bool top_right = false;

        public bool bottom_left = false;
        public bool bottom_right = false;

        public List<UndefiendLine> undefiendLinesLeftFrom = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesLeftTo = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesLeftFromY1 = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesLeftToY2 = new List<UndefiendLine>();

        public List<UndefiendLine> undefiendLinesRightFrom = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesRightTo = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesRightFromY1 = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesRightToY2 = new List<UndefiendLine>();

        public List<UndefiendLine> undefiendLinesTopFrom = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesTopTo = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesTopFromY1 = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesTopToY2 = new List<UndefiendLine>();

        public List<UndefiendLine> undefiendLinesBottomFrom = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesBottomTo = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesBottomFromY1 = new List<UndefiendLine>();
        public List<UndefiendLine> undefiendLinesBottomToY2 = new List<UndefiendLine>();

        public ConnectionLine()
        {
            circle_left = new Ellipse();
            circle_left.Height = 6.5;
            circle_left.Width  = 6.5;
            circle_left.Fill = Brushes.Green;

            circle_right = new Ellipse();
            circle_right.Height = 6.5;
            circle_right.Width = 6.5;
            circle_right.Fill = Brushes.Green;

            circle_top = new Ellipse();
            circle_top.Height = 6.5;
            circle_top.Width = 6.5;
            circle_top.Fill = Brushes.Green;

            circle_bottom = new Ellipse();
            circle_bottom.Height = 6.5;
            circle_bottom.Width = 6.5;
            circle_bottom.Fill = Brushes.Green;
        }
    }
}