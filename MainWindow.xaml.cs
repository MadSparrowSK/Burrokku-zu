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

namespace Interface_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region DragMainWindow
        private void Mouse_Drag_Window(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region Interactions with Header

        #region right_side
        private void Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0.2 };
        }
        private void Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Mouse_Enter_Close(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Background = Brushes.Red;
        }
        private void Mouse_Leave_Close(object sender, RoutedEventArgs e)
        {
            lbl_Close.Background = new SolidColorBrush(Colors.Gray) { Opacity = 0 };
        }
        #endregion

        #region left_side
        private void Help_Window(object sender, RoutedEventArgs e) { }
        private void Header_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.White;
        }
        private void Header_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.Gray;
        }
        #endregion

        #endregion

        #region Interactions with SideBar
        private void SideBar_Mouse_Enter(object sender, RoutedEventArgs e)
        {
            if (lbl_Ellipse == sender)
                Ellipse.Stroke = Brushes.White;
            if (lbl_Cylce == sender)
                Cycle.Stroke = Brushes.White;
            if (lbl_Parrabellum == sender)
                Parrabellum.Stroke = Brushes.White;
            if (lbl_Rhomb == sender)
                Rhomb.Stroke = Brushes.White;
            if (lbl_Rect == sender)
                Rekt.Stroke = Brushes.White;
        }
        private void SideBar_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            if (lbl_Ellipse == sender)
                Ellipse.Stroke = Brushes.Gray;
            if (lbl_Cylce == sender)
                Cycle.Stroke = Brushes.Gray;
            if (lbl_Parrabellum == sender)
                Parrabellum.Stroke = Brushes.Gray;
            if (lbl_Rhomb == sender)
                Rhomb.Stroke = Brushes.Gray;
            if (lbl_Rect == sender)
                Rekt.Stroke = Brushes.Gray;
        }
        #endregion

        #region Show Window with Settings
        private void Change_Window(object sender, RoutedEventArgs e)
        {
            //Window1 w1 = new Window1();
            //w1.Show();
        }
        private void HelpWindowOpen(object sender, RoutedEventArgs e)
        {
            //HelpWindow hw = new HelpWindow();
            //hw.Show();
        }
        #endregion

        #region ResizeMainWindow
        private void Resize_Wondow(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }
        private void Roll_Up(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Drag_N_Drop

        private bool Rectangle_Check = false;
        private bool Parrabullem_Check = false;
        private bool Rhomb_Check = false;
        private bool Cycle_Check = false;
        private bool Ellipse_Check = false;

        private void DragDrop_MD(object sender, MouseButtonEventArgs e)
        {
            if (Rekt == sender)
                Rectangle_Check = true;
            if (Rhomb == sender)
                Rhomb_Check = true;
            if (Parrabellum == sender)
                Parrabullem_Check = true;
            if (Cycle == sender)
                Cycle_Check = true;
            if (Ellipse == sender)
                Ellipse_Check = true;

            var smt = (UIElement)sender;
            lastPoint = e.GetPosition(smt);

            DragDrop.DoDragDrop(this, this, DragDropEffects.Copy);
        }

        #region Interaction_With_Shapes_Into_Canvas

        Polyline anchor_size;
        Polyline anchor_left;
        Polyline anchor_top;

        Point lastPoint;
        Point lastPoint_anchor;
        Point current_anchor_postion;

        bool is_anchor_create = false;

        private void RectangleAdd(Polygon polygon, TextBox txt, Point rectangleNW, Point rectangleSE, Point rectangleSW, Point rectnagleNE)
        {
            polygon.MouseDown += IntoCanvasDownPolylineRectangle;

            void IntoCanvasDownPolylineRectangle(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(txt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(anchor_left);
                    CanvasPos.Children.Remove(anchor_top);
                }
                #endregion

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) - Math.Sqrt(Math.Pow(rectnagleNE.X, 2))) / 2;
                double anchor_top_indent =  Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) - Math.Sqrt(Math.Pow(rectangleSW.Y, 2))) / 2;

                #region anchor
                if (!is_anchor_create)
                {
                    #region points_anchors

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp   += AnchorMouseUp;

                    anchor_left = new Polyline();
                    anchor_left.Points = anchor_Left.Points;
                    anchor_left.Stroke = Brushes.Red;
                    anchor_left.Fill   = Brushes.Transparent;

                    anchor_left.MouseDown += AnchorMouseDown;
                    anchor_left.MouseMove += AnchorLeftMouseMove;
                    anchor_left.MouseUp   += AnchorMouseUp;

                    anchor_top = new Polyline();
                    anchor_top.Points = anchor_Top.Points;
                    anchor_top.Stroke = Brushes.Red;
                    anchor_top.Fill   = Brushes.Transparent;

                    anchor_top.MouseDown += AnchorMouseDown;
                    anchor_top.MouseMove += AnchorTopMouseMove;
                    anchor_top.MouseUp   += AnchorMouseUp;

                    #endregion

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorLeftMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke  = Brushes.Transparent;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) - Math.Sqrt(Math.Pow(rectnagleNE.X, 2))) / 2;
                            anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) - Math.Sqrt(Math.Pow(rectangleSW.Y, 2))) / 2;                            
                            
                            Cursor = Cursors.SizeWE;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) + Math.Sqrt(Math.Pow(rectnagleNE.X, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X)
                            {

                                rectangleSE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                rectnagleNE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) + Math.Sqrt(Math.Pow(rectnagleNE.X, 2)));
                            }
                            else if (point_summ_second_X > 60)
                            {
                                rectangleSE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                rectnagleNE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) + Math.Sqrt(Math.Pow(rectnagleNE.X, 2)));
                            }
                        }
                    }
                    void AnchorTopMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) - Math.Sqrt(Math.Pow(rectnagleNE.X, 2))) / 2;
                            anchor_top_indent =  Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) - Math.Sqrt(Math.Pow(rectangleSW.Y, 2))) / 2;
                            
                            Cursor = Cursors.SizeNS;

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) + Math.Sqrt(Math.Pow(rectangleSW.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                rectangleSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectangleSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) + Math.Sqrt(Math.Pow(rectangleSW.Y, 2)));
                            }
                            else if(point_summ_second_Y > 40)
                            {
                                rectangleSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectangleSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) + Math.Sqrt(Math.Pow(rectangleSW.Y, 2)));
                            }

                        }
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {

                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke  = Brushes.Transparent;

                            Cursor = Cursors.SizeNWSE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) - Math.Sqrt(Math.Pow(rectnagleNE.X, 2))) / 2;
                            anchor_top_indent =  Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) - Math.Sqrt(Math.Pow(rectangleSW.Y, 2))) / 2;

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) + Math.Sqrt(Math.Pow(rectangleSW.Y, 2)));
                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) + Math.Sqrt(Math.Pow(rectnagleNE.X, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                rectangleSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectangleSE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                rectangleSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectnagleNE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) + Math.Sqrt(Math.Pow(rectangleSW.Y, 2)));
                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) + Math.Sqrt(Math.Pow(rectnagleNE.X, 2)));
                            }
                            else if (point_summ_second_X >= 60 && point_summ_second_Y >= 40)
                            {
                                rectangleSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectangleSE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;
                                rectangleSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                rectnagleNE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(rectangleNW);
                                points.Add(rectangleSW);
                                points.Add(rectangleSE);
                                points.Add(rectnagleNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) + Math.Sqrt(Math.Pow(rectangleSW.Y, 2)));
                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) + Math.Sqrt(Math.Pow(rectnagleNE.X, 2)));
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        anchor_size.Stroke = Brushes.Red;
                        anchor_left.Stroke = Brushes.Red;
                        anchor_top.Stroke  = Brushes.Red;

                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 7);
                        Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                        Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt));
                        Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + anchor_top_indent - 5);

                        Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 3);
                        Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 3);
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);
                    CanvasPos.Children.Add(anchor_left);
                    CanvasPos.Children.Add(anchor_top);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 7);
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + anchor_top_indent - 5);

                    Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 3);
                    Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 3);

                    is_anchor_create = true;
                }
                #endregion

                RectangleIntoCanvasMouseMove(polygon, txt, rectangleNW, rectangleSE, rectangleSW, rectnagleNE);
            }
        }
        private void RectangleIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point rectangleNW, Point rectangleSE, Point rectangleSW, Point rectnagleNE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                        CanvasPos.Children.Remove(anchor_left);
                        CanvasPos.Children.Remove(anchor_top);
                    }

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) - Math.Sqrt(Math.Pow(rectnagleNE.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) - Math.Sqrt(Math.Pow(rectangleSW.Y, 2))) / 2;

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + anchor_left_indent - 10);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + anchor_top_indent - 5);
                }
            }
        }

        private void ParrabellumAdd(Polygon polygon, TextBox txt, Point parabellumNW, Point parabellumSW, Point parabellumSE, Point parabellumNE)
        {
            polygon.MouseDown += IntoCanvasDownPolylineParrabellum;

            void IntoCanvasDownPolylineParrabellum(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(txt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(anchor_left);
                    CanvasPos.Children.Remove(anchor_top);
                }
                #endregion

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) - Math.Sqrt(Math.Pow(parabellumNE.X, 2))) / 2;
                double anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.Y, 2))) / 2;

                #region anchor
                if (!is_anchor_create)
                {
                    #region points_anchors

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill   = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp   += AnchorMouseUp;

                    anchor_left = new Polyline();
                    anchor_left.Points = anchor_Left.Points;
                    anchor_left.Stroke = Brushes.Red;
                    anchor_left.Fill   = Brushes.Transparent;

                    anchor_left.MouseDown += AnchorMouseDown;
                    anchor_left.MouseMove += AnchorLeftMouseMove;
                    anchor_left.MouseUp   += AnchorMouseUp;

                    anchor_top = new Polyline();
                    anchor_top.Points = anchor_Top.Points;
                    anchor_top.Stroke = Brushes.Red;
                    anchor_top.Fill   = Brushes.Transparent;

                    anchor_top.MouseDown += AnchorMouseDown;
                    anchor_top.MouseMove += AnchorTopMouseMove;
                    anchor_top.MouseUp   += AnchorMouseUp;

                    #endregion

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorTopMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            Cursor = Cursors.SizeNS;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) - Math.Sqrt(Math.Pow(parabellumNE.X, 2))) / 2;
                            anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.Y, 2))) / 2;

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) + Math.Sqrt(Math.Pow(parabellumSW.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                parabellumSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) + Math.Sqrt(Math.Pow(parabellumSW.Y, 2)));
                            }
                            else if(point_summ_second_Y >= 40)
                            {
                                parabellumSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) + Math.Sqrt(Math.Pow(parabellumSW.Y, 2)));
                            }
                        }
                    }
                    void AnchorLeftMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed) 
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            Cursor = Cursors.SizeWE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) - Math.Sqrt(Math.Pow(parabellumNE.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.Y, 2))) / 2;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) + Math.Sqrt(Math.Pow(parabellumNE.X, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X)
                            {
                                parabellumSE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                parabellumNE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) + Math.Sqrt(Math.Pow(parabellumNE.X, 2)));
                            }
                            else if(point_summ_second_X > 60)
                            {
                                parabellumSE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                parabellumNE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) + Math.Sqrt(Math.Pow(parabellumNE.X, 2)));
                            }
                        }
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke  = Brushes.Transparent;

                            Cursor = Cursors.SizeNWSE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) - Math.Sqrt(Math.Pow(parabellumNE.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.Y, 2))) / 2;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) + Math.Sqrt(Math.Pow(parabellumNE.X, 2)));
                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) + Math.Sqrt(Math.Pow(parabellumSW.Y, 2)));

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                parabellumSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumSE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                parabellumSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumNE.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) + Math.Sqrt(Math.Pow(parabellumNE.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) + Math.Sqrt(Math.Pow(parabellumSW.Y, 2)));
                            }
                            else if (point_summ_second_X > 60 && point_summ_second_Y > 40)
                            {
                                parabellumSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumSE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                parabellumSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                parabellumNE.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(parabellumNW);
                                points.Add(parabellumSW);
                                points.Add(parabellumSE);
                                points.Add(parabellumNE);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 13);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) + Math.Sqrt(Math.Pow(parabellumNE.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) + Math.Sqrt(Math.Pow(parabellumSW.Y, 2)));
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        anchor_size.Stroke = Brushes.Red;
                        anchor_left.Stroke = Brushes.Red;
                        anchor_top.Stroke = Brushes.Red;

                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 8);
                        Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                        Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt) - 5);
                        Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + anchor_top_indent - 3);

                        Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 3);
                        Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 6);
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);
                    CanvasPos.Children.Add(anchor_left);
                    CanvasPos.Children.Add(anchor_top);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 8);
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt) - 5);
                    Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + anchor_top_indent - 3);

                    Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 3);
                    Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 6);

                    is_anchor_create = true;
                }
                #endregion

                ParrabellumIntoCanvasMouseMove(polygon, txt, parabellumNW, parabellumSE, parabellumSE, parabellumNE);
            }
        }
        private void ParrabellumIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point parabellumNW, Point parabellumSW, Point parabellumSE, Point parabellumNE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                        CanvasPos.Children.Remove(anchor_left);
                        CanvasPos.Children.Remove(anchor_top);
                    }

                    var smt = (Polygon)sender;
                    smt.CaptureMouse();

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) - Math.Sqrt(Math.Pow(parabellumNE.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.Y, 2))) / 2;

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 13);
                    Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 6);
                }
            }
        }

        private void RhombAdd(Polygon polygon, TextBox txt, Point rhombN, Point rhombW, Point rhombS, Point rhombE)
        {
            polygon.MouseDown += IntoCanvasDownPolylineRhomb;

            void IntoCanvasDownPolylineRhomb(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2))) / 2;
                double anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2))) / 2;

                #region anchor
                if (!is_anchor_create)
                {
                    #region points_anchors

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill   = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp   += AnchorMouseUp;

                    anchor_left = new Polyline();
                    anchor_left.Points = anchor_Left.Points;
                    anchor_left.Stroke = Brushes.Red;
                    anchor_left.Fill   = Brushes.Transparent;

                    anchor_left.MouseDown += AnchorMouseDown;
                    anchor_left.MouseMove += AnchorLeftMouseMove;
                    anchor_left.MouseUp   += AnchorMouseUp;

                    anchor_top = new Polyline();
                    anchor_top.Points = anchor_Top.Points;
                    anchor_top.Stroke = Brushes.Red;
                    anchor_top.Fill   = Brushes.Transparent;

                    anchor_top.MouseDown += AnchorMouseDown;
                    anchor_top.MouseMove += AnchorTopMouseMove;
                    anchor_top.MouseUp   += AnchorMouseUp;

                    #endregion

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorTopMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke  = Brushes.Transparent;

                            Cursor = Cursors.SizeNS;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) - Math.Sqrt(Math.Pow(rhombE.X, 2))) / 2;
                            anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) - Math.Sqrt(Math.Pow(rhombS.Y, 2))) / 2;

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2)));

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                rhombN.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 20;
                                
                                rhombS.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 20;
                                
                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 20);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 10);

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon) + anchor_top_indent;

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2)));
                            }
                            else if(point_summ_second_Y > 30)
                            {
                                rhombN.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 20;

                                rhombS.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 20;
                                
                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 20);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 14);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 14);

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon) + anchor_top_indent;

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2)));
                            }
                        }
                    }
                    void AnchorLeftMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            Cursor = Cursors.SizeWE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2))) / 2;
                            anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2))) / 2;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2)));

                            if (pos.X < current_anchor_postion.X)
                            {
                                rhombN.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                             
                                rhombS.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                rhombE.X += Math.Abs(pos.X - current_anchor_postion.X);

                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 14);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 14);

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2)));
                            }
                            else if(point_summ_second_X > 70)
                            {
                                rhombN.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                rhombS.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                rhombE.X -= Math.Abs(pos.X - current_anchor_postion.X);

                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 14);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 14);

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2)));
                            }
                        }
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) - Math.Sqrt(Math.Pow(rhombE.X, 2))) / 2;
                            anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) - Math.Sqrt(Math.Pow(rhombS.Y, 2))) / 2;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2)));
                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2)));

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                rhombN.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                rhombN.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 20;

                                rhombS.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 20;
                                rhombS.X += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                rhombE.X += Math.Abs(pos.X - current_anchor_postion.X);

                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 20);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 14);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 14);

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2)));
                            }
                            else if (point_summ_second_X > 50 && point_summ_second_Y > 25)
                            {
                                rhombN.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                rhombN.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 20;

                                rhombS.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 20;
                                rhombS.X -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                rhombE.X -= Math.Abs(pos.X - current_anchor_postion.X);

                                PointCollection points = new PointCollection();
                                points.Add(rhombW);
                                points.Add(rhombS);
                                points.Add(rhombE);
                                points.Add(rhombN);

                                smt.Points = points;

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 20);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 14);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 14);

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(rhombE.X, 2)) + Math.Sqrt(Math.Pow(rhombW.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2)));
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        anchor_size.Stroke = Brushes.Red;
                        anchor_left.Stroke = Brushes.Red;
                        anchor_top.Stroke  = Brushes.Red;

                        Cursor = Cursors.Arrow;

                        anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) - Math.Sqrt(Math.Pow(rhombE.X, 2))) / 2;
                        anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) - Math.Sqrt(Math.Pow(rhombS.Y, 2))) / 2;

                        Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                        Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                        Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt) - anchor_top_indent - 3);
                        Canvas.SetTop(anchor_left, Canvas.GetTop(smt));

                        Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 10);
                        Canvas.SetTop(anchor_top, Canvas.GetTop(polygon) - anchor_top_indent);
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);
                    CanvasPos.Children.Add(anchor_left);
                    CanvasPos.Children.Add(anchor_top);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt) - anchor_top_indent - 3);
                    Canvas.SetTop(anchor_left, Canvas.GetTop(smt));

                    Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 10);
                    Canvas.SetTop(anchor_top, Canvas.GetTop(polygon) - anchor_top_indent);

                    is_anchor_create = true;
                }
                #endregion

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(txt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(anchor_left);
                    CanvasPos.Children.Remove(anchor_top);
                }
                #endregion

                RhombIntoCanvasMouseMove(polygon, txt,rhombN,rhombW,rhombS,rhombE);
            }
        }
        private void RhombIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point rhombN, Point rhombW, Point rhombS, Point rhombE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                        CanvasPos.Children.Remove(anchor_left);
                        CanvasPos.Children.Remove(anchor_top);
                    }

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2))) / 2;

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + anchor_left_indent - 14);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + anchor_top_indent - 14);
                }
            }
        }

        private void CycleAdd(Polygon polygon, TextBox txt, Point cycleNW, Point cycleW, Point cycleSW, Point cycleSE, Point cycleE, Point cycleNE)
        {
            polygon.MouseDown += IntoCanvasDownPolylineCycle;

            void IntoCanvasDownPolylineCycle(object sender, MouseButtonEventArgs e)
            {
                var smt = (Polygon)sender;
                lastPoint = e.GetPosition(smt);

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2))) / 2;
                double anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.Y, 2))) / 2;

                #region anchor
                if (!is_anchor_create)
                {
                    #region creating anchors

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill   = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp   += AnchorMouseUp;

                    anchor_left = new Polyline();
                    anchor_left.Points = anchor_Left.Points;
                    anchor_left.Stroke = Brushes.Red;
                    anchor_left.Fill   = Brushes.Transparent;

                    anchor_left.MouseDown += AnchorMouseDown;
                    anchor_left.MouseMove += AnchorLeftMouseMove;
                    anchor_left.MouseUp   += AnchorMouseUp;

                    anchor_top = new Polyline();
                    anchor_top.Points = anchor_Top.Points;
                    anchor_top.Stroke = Brushes.Red;
                    anchor_top.Fill   = Brushes.Transparent;

                    anchor_top.MouseDown += AnchorMouseDown;
                    anchor_top.MouseMove += AnchorTopMouseMove;
                    anchor_top.MouseUp   += AnchorMouseUp;
                    #endregion

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorTopMouseMove(object sndr,MouseEventArgs evnt) 
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed) 
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke  = Brushes.Transparent;

                            Cursor = Cursors.SizeNS;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2))) / 2;
                            anchor_top_indent  = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.Y, 2))) / 2;

                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) + Math.Sqrt(Math.Pow(cycleSW.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                cycleW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 8;

                                cycleSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                cycleSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                cycleE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 8;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) + Math.Sqrt(Math.Pow(cycleSW.Y, 2)));

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);
                            }
                            else if (point_summ_second_Y > 40)
                            {
                                cycleW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                cycleSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                cycleSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                cycleE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) + Math.Sqrt(Math.Pow(cycleSW.Y, 2)));

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);
                            }
                        }
                    }
                    void AnchorLeftMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            Cursor = Cursors.SizeWE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.Y, 2))) / 2;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) + Math.Sqrt(Math.Pow(cycleNE.X, 2)));

                            if (pos.X < current_anchor_postion.X)
                            {
                                cycleW.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 10;

                                cycleSE.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                cycleE.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 1.65;

                                cycleNE.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) + Math.Sqrt(Math.Pow(cycleNE.X, 2)));

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);
                            }
                            else if(point_summ_second_X > 80)
                            {
                                cycleW.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 10;

                                cycleSE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                cycleE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 1.65;

                                cycleNE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) + Math.Sqrt(Math.Pow(cycleNE.X, 2)));

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);
                            }
                        }
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke  = Brushes.Transparent;

                            Cursor = Cursors.SizeNWSE;

                            anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2))) / 2;
                            anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.Y, 2))) / 2;

                            double point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) + Math.Sqrt(Math.Pow(cycleNE.X, 2)));
                            double point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) + Math.Sqrt(Math.Pow(cycleSW.Y, 2)));

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                cycleW.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 10;
                                cycleW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 8;

                                cycleSW.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                cycleSE.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 2;
                                cycleSE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                cycleE.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 1.65;
                                cycleE.Y += Math.Abs(pos.Y - current_anchor_postion.Y) / 8;

                                cycleNE.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) + Math.Sqrt(Math.Pow(cycleNE.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) + Math.Sqrt(Math.Pow(cycleSW.Y, 2)));

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);
                            }
                            else if (point_summ_second_X > 60 && point_summ_second_Y > 40)
                            {
                                cycleW.X += Math.Abs(pos.Y - current_anchor_postion.Y) / 10;
                                cycleW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 8;

                                cycleSW.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                cycleSE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;
                                cycleSE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                cycleE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 1.65;
                                cycleE.Y -= Math.Abs(pos.Y - current_anchor_postion.Y) / 8;

                                cycleNE.X -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                PointCollection points = new PointCollection();
                                points.Add(cycleW);
                                points.Add(cycleSW);
                                points.Add(cycleSE);
                                points.Add(cycleE);
                                points.Add(cycleNE);
                                points.Add(cycleNW);

                                smt.Points = points;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                point_summ_second_X = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) + Math.Sqrt(Math.Pow(cycleNE.X, 2)));
                                point_summ_second_Y = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) + Math.Sqrt(Math.Pow(cycleSW.Y, 2)));

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + anchor_left_indent - 10);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + anchor_top_indent - 5);
                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        anchor_size.Stroke = Brushes.Red;
                        anchor_left.Stroke = Brushes.Red;
                        anchor_top.Stroke = Brushes.Red;

                        Cursor = Cursors.Arrow;

                        Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 8);
                        Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                        Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt));
                        Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + anchor_top_indent - 5);

                        Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 3);
                        Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 6);
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);
                    CanvasPos.Children.Add(anchor_left);
                    CanvasPos.Children.Add(anchor_top);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 8);
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt));
                    Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + anchor_top_indent - 5);

                    Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + anchor_left_indent - 3);
                    Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 6);

                    is_anchor_create = true;
                }
                #endregion

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(txt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(anchor_left);
                    CanvasPos.Children.Remove(anchor_top);
                }
                #endregion

                CycleIntoCanvasMouseMove(polygon, txt, cycleNW, cycleW, cycleSW, cycleSE, cycleE, cycleNE);
            }
        }
        private void CycleIntoCanvasMouseMove(Polygon polygon, TextBox txt, Point cycleNW, Point cycleW, Point cycleSW, Point cycleSE, Point cycleE, Point cycleNE)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                        CanvasPos.Children.Remove(anchor_left);
                        CanvasPos.Children.Remove(anchor_top);
                    }

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2))) / 2;
                    double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.Y, 2))) / 2;

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + anchor_left_indent - 10);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + anchor_top_indent - 5);
                }
            }
        }

        private void EllipseAdd(Rectangle polygon, TextBox txt)
        {
            polygon.MouseDown += IntoCanvasMouseDownRectangle;

            void IntoCanvasMouseDownRectangle(object sender, MouseButtonEventArgs e)
            {
                var smt = (Rectangle)sender;
                lastPoint = e.GetPosition(smt);

                #region anchor
                if (!is_anchor_create)
                {
                    #region points_anhcors

                    anchor_size = new Polyline();
                    anchor_size.Points = Anchor.Points;
                    anchor_size.Stroke = Brushes.Red;
                    anchor_size.Fill   = Brushes.Transparent;

                    anchor_size.MouseDown += AnchorMouseDown;
                    anchor_size.MouseMove += AnchorMouseMove;
                    anchor_size.MouseUp   += AnchorMouseUp;

                    anchor_left = new Polyline();
                    anchor_left.Points = anchor_Left.Points;
                    anchor_left.Stroke = Brushes.Red;
                    anchor_left.Fill   = Brushes.Transparent;

                    anchor_left.MouseDown += AnchorMouseDown;
                    anchor_left.MouseMove += AnchorLeftMouseMove;
                    anchor_left.MouseUp   += AnchorMouseUp;

                    anchor_top = new Polyline();
                    anchor_top.Points = anchor_Top.Points;
                    anchor_top.Stroke = Brushes.Red;
                    anchor_top.Fill   = Brushes.Transparent;

                    anchor_top.MouseDown += AnchorMouseDown;
                    anchor_top.MouseMove += AnchorTopMouseMove;
                    anchor_top.MouseUp   += AnchorMouseUp;

                    #endregion

                    #region anchor_action

                    void AnchorMouseDown(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (UIElement)sndr;
                        lastPoint_anchor = evnt.GetPosition(anchor);
                        current_anchor_postion = evnt.GetPosition(CanvasPos);
                    }
                    void AnchorTopMouseMove(object sndr, MouseEventArgs evnt) 
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke  = Brushes.Transparent;

                            Cursor = Cursors.SizeNS;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.Y < current_anchor_postion.Y)
                            {
                                smt.Height += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);
                            }
                            else if (polygon.Height > 40)
                            {
                                polygon.Height -= Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);

                            }
                        }
                    }
                    void AnchorLeftMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if(evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            Cursor = Cursors.SizeWE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X)
                            {
                                smt.Width += Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);
                            }
                            else if (polygon.Width > 75)
                            {
                                smt.Width -= Math.Abs(pos.X - current_anchor_postion.X) / 2;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);
                            }
                        }
                    }
                    void AnchorMouseMove(object sndr, MouseEventArgs evnt)
                    {
                        if (evnt.LeftButton == MouseButtonState.Pressed)
                        {
                            var anchor = (Polyline)sndr;
                            anchor.CaptureMouse();

                            anchor_size.Stroke = Brushes.Transparent;
                            anchor_left.Stroke = Brushes.Transparent;
                            anchor_top.Stroke = Brushes.Transparent;

                            Cursor = Cursors.SizeNWSE;

                            var pos = e.GetPosition(CanvasPos) - lastPoint_anchor;

                            if (pos.X < current_anchor_postion.X && pos.Y < current_anchor_postion.Y)
                            {
                                smt.Width += Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                smt.Height += Math.Abs(pos.Y - current_anchor_postion.Y) / 4;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) - Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) - Math.Abs(pos.Y - current_anchor_postion.Y) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);
                            }
                            else if (polygon.Width > 75 && polygon.Height > 40)
                            {
                                smt.Width  -= Math.Abs(pos.X - current_anchor_postion.X) / 2;
                                smt.Height -= Math.Abs(pos.Y - current_anchor_postion.Y) / 2;

                                current_anchor_postion.X = Canvas.GetLeft(polygon);
                                current_anchor_postion.Y = Canvas.GetTop(polygon);

                                Canvas.SetLeft(polygon, Canvas.GetLeft(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 2);
                                Canvas.SetTop(polygon, Canvas.GetTop(polygon) + Math.Abs(pos.X - current_anchor_postion.X) / 4);

                                Canvas.SetLeft(txt, Canvas.GetLeft(polygon) + polygon.Width / 2 - 15);
                                Canvas.SetTop(txt, Canvas.GetTop(polygon) + polygon.Height / 2 - 10);

                            }
                        }
                    }
                    void AnchorMouseUp(object sndr, MouseButtonEventArgs evnt)
                    {
                        var anchor = (Polyline)sndr;
                        anchor.ReleaseMouseCapture();

                        Cursor = Cursors.Arrow;

                        anchor_size.Stroke = Brushes.Red;
                        anchor_left.Stroke = Brushes.Red;
                        anchor_top.Stroke = Brushes.Red;


                        Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 5);
                        Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                        Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt) - 6);
                        Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + polygon.Height / 2 - 8);

                        Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + polygon.Width / 2 - 8);
                        Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 6);
                    }

                    #endregion

                    CanvasPos.Children.Add(anchor_size);
                    CanvasPos.Children.Add(anchor_left);
                    CanvasPos.Children.Add(anchor_top);

                    Canvas.SetLeft(anchor_size, Canvas.GetLeft(smt) + 5);
                    Canvas.SetTop(anchor_size, Canvas.GetTop(smt));

                    Canvas.SetLeft(anchor_left, Canvas.GetLeft(smt) - 6);
                    Canvas.SetTop(anchor_left, Canvas.GetTop(smt) + polygon.Height/2 - 8);

                    Canvas.SetLeft(anchor_top, Canvas.GetLeft(smt) + polygon.Width/2 - 8);
                    Canvas.SetTop(anchor_top, Canvas.GetTop(smt) - 6);

                    is_anchor_create = true;
                }
                #endregion

                #region delete shape
                if (e.RightButton == MouseButtonState.Pressed)
                {
                    CanvasPos.Children.Remove(smt);
                    CanvasPos.Children.Remove(txt);
                    CanvasPos.Children.Remove(anchor_size);
                    CanvasPos.Children.Remove(anchor_left);
                    CanvasPos.Children.Remove(anchor_top);
                }
                #endregion

                EllipseIntoCanvasMouseMove(polygon, txt);
            }
        }
        private void EllipseIntoCanvasMouseMove(Rectangle polygon, TextBox txt)
        {
            polygon.MouseMove += IntoCanvasMove;

            void IntoCanvasMove(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (is_anchor_create)
                    {
                        is_anchor_create = false;
                        CanvasPos.Children.Remove(anchor_size);
                        CanvasPos.Children.Remove(anchor_left);
                        CanvasPos.Children.Remove(anchor_top);
                    }

                    var smt = (UIElement)sender;
                    smt.CaptureMouse();

                    Canvas.SetLeft(smt, e.GetPosition(CanvasPos).X - lastPoint.X);
                    Canvas.SetTop(smt, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                    Canvas.SetLeft(txt, Canvas.GetLeft(smt) + polygon.Width / 2 - 15);
                    Canvas.SetTop(txt, Canvas.GetTop(smt) + polygon.Height / 2 - 10);
                }
            }
        }

        public void IntoCanvasUp(object sender, MouseButtonEventArgs e)
        {
            var smt = (UIElement)sender;
            smt.ReleaseMouseCapture();
        }
        #endregion

        private void DnD_Drop(object sender, DragEventArgs e)
        {
            #region points

            Point rectangleNW = new Point();
            Point rectangleSW = new Point();
            Point rectangleSE = new Point();
            Point rectnagleNE = new Point();

            Point parabellumNW = new Point();
            Point parabellumSW = new Point();
            Point parabellumSE = new Point();
            Point parabellumNE = new Point();

            Point rhombN = new Point();
            Point rhombW = new Point();
            Point rhombS = new Point();
            Point rhombE = new Point();

            Point cycleNW = new Point();
            Point cycleW = new Point();
            Point cycleSW = new Point();
            Point cycleSE = new Point();
            Point cycleE = new Point();
            Point cycleNE = new Point();

            #endregion

            #region initialized shapes points

            #region rectangle points
            rectangleNW.X = 8;
            rectangleNW.Y = 1;

            rectangleSW.X = 8;
            rectangleSW.Y = 30;

            rectangleSE.X = 60;
            rectangleSE.Y = 30;

            rectnagleNE.X = 60;
            rectnagleNE.Y = 1;
            #endregion

            #region parrabellum points
            parabellumNW.X = 8;
            parabellumNW.Y = 1;

            parabellumSW.X = 0;
            parabellumSW.Y = 30;

            parabellumSE.X = 60;
            parabellumSE.Y = 30;

            parabellumNE.X = 68;
            parabellumNE.Y = 1;
            #endregion

            #region rhomb points
            rhombW.X = 0;
            rhombW.Y = 8;

            rhombS.X = 40;
            rhombS.Y = 20;

            rhombE.X = 80;
            rhombE.Y = 8;

            rhombN.X = 40;
            rhombN.Y = -4;
            #endregion

            #region cycle points

            cycleW.X = -1;
            cycleW.Y = 15;

            cycleSW.X = 8;
            cycleSW.Y = 30;

            cycleSE.X = 60;
            cycleSE.Y = 30;

            cycleE.X = 69;
            cycleE.Y = 15;

            cycleNE.X = 60;
            cycleNE.Y = 1;

            cycleNW.X = 8;
            cycleNW.Y = 1;
            #endregion

            #endregion

            if (Rectangle_Check)
            {
                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Rectangle_Check = false;
                polyline.Points = Rekt.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.X, 2)) - Math.Sqrt(Math.Pow(rectnagleNE.X, 2))) / 2;
                double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rectangleNW.Y, 2)) - Math.Sqrt(Math.Pow(rectangleSW.Y, 2))) / 2;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + anchor_left_indent - 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + anchor_top_indent - 5);

                RectangleAdd(polyline, text_into_shapes, rectangleNW, rectangleSE, rectangleSW, rectnagleNE);
            }
            if (Parrabullem_Check)
            {
                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Parrabullem_Check = false;
                polyline.Points = Parrabellum.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill   = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.X, 2)) - Math.Sqrt(Math.Pow(parabellumNE.X, 2))) / 2;
                double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(parabellumNW.Y, 2)) - Math.Sqrt(Math.Pow(parabellumSW.Y, 2))) / 2;

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + anchor_left_indent - 13);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + anchor_top_indent - 2);

                ParrabellumAdd(polyline, text_into_shapes, parabellumNW, parabellumSW, parabellumSE, parabellumNE);
            }
            if (Rhomb_Check)
            {
                Rhomb_Check = false;

                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Parrabullem_Check = false;
                polyline.Points = Rhomb.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombW.X, 2)) + Math.Sqrt(Math.Pow(rhombE.X, 2))) / 2;
                double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(rhombN.Y, 2)) + Math.Sqrt(Math.Pow(rhombS.Y, 2))) / 2;

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + anchor_left_indent - 14);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + anchor_top_indent - 14);

                RhombAdd(polyline, text_into_shapes, rhombN, rhombW, rhombS, rhombE);
            }
            if (Cycle_Check)
            {
                Cycle_Check = false;

                Polygon polyline = new Polygon();
                TextBox text_into_shapes = new TextBox();

                Parrabullem_Check = false;
                polyline.Points = Cycle.Points;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                polyline.Stroke = Brushes.White;
                polyline.Fill = Brushes.Transparent;

                polyline.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(polyline);
                CanvasPos.Children.Add(text_into_shapes);

                double anchor_left_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.X, 2)) - Math.Sqrt(Math.Pow(cycleNE.X, 2))) / 2;
                double anchor_top_indent = Math.Abs(Math.Sqrt(Math.Pow(cycleNW.Y, 2)) - Math.Sqrt(Math.Pow(cycleSW.Y, 2))) / 2;

                Canvas.SetLeft(polyline, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(polyline, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(polyline) + anchor_left_indent - 10);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(polyline) + anchor_top_indent - 5);

                CycleAdd(polyline, text_into_shapes, cycleNW, cycleW, cycleSW, cycleSE, cycleE, cycleNE);
            }
            if (Ellipse_Check)
            {
                Ellipse_Check = false;

                Rectangle rectangle = new Rectangle();
                TextBox text_into_shapes = new TextBox();

                rectangle.Width = Ellipse.Width;
                rectangle.Height = Ellipse.Height;
                rectangle.RadiusX = Ellipse.RadiusX;
                rectangle.RadiusY = Ellipse.RadiusY;
                rectangle.Fill = Brushes.Transparent;
                rectangle.Stroke = Brushes.White;

                text_into_shapes.Text = "Text";
                text_into_shapes.MinWidth = 40;
                text_into_shapes.MinHeight = 20;
                text_into_shapes.FontSize = 10;
                text_into_shapes.BorderBrush = Brushes.Transparent;
                text_into_shapes.Foreground = Brushes.White;
                text_into_shapes.Background = Brushes.Transparent;

                rectangle.MouseUp += IntoCanvasUp;

                CanvasPos.Children.Add(rectangle);
                CanvasPos.Children.Add(text_into_shapes);

                Canvas.SetLeft(rectangle, e.GetPosition(CanvasPos).X - lastPoint.X);
                Canvas.SetTop(rectangle, e.GetPosition(CanvasPos).Y - lastPoint.Y);

                Canvas.SetLeft(text_into_shapes, Canvas.GetLeft(rectangle) + rectangle.Width / 2 - 15);
                Canvas.SetTop(text_into_shapes, Canvas.GetTop(rectangle) + rectangle.Height / 2 - 10);

                EllipseAdd(rectangle, text_into_shapes);
            }
        }

        private void Clear_Mouse_Enter(object sender, RoutedEventArgs e)
        {

            ((Label)sender).Foreground = Brushes.White;
        }
        private void Clear_Mouse_Leave(object sender, RoutedEventArgs e)
        {
            ((Label)sender).Foreground = Brushes.Gray;
        }
        private void inTrash(object sender, RoutedEventArgs e)
        {
            CanvasPos.Children.Clear();
        }

        #endregion
    }
}
