using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Media3D;
using _3DTools;
using LDrawPartLib;

namespace DomeCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        //readonly ObservableCollection<Part3D> _parts;

            #region Members
        private const int PLATE_HEIGHT = 8;
        private const int PLATE_WIDTH = 20;
        #endregion

        public MainWindow() {
            InitializeComponent();

            //LoadRandomParts();

            //_parts = Calculate();
        }

        //private ObservableCollection<Part3D> Calculate() {
        //    ObservableCollection<Part3D> list = new ObservableCollection<Part3D>();
        //    double sc = 2.5;
        //    double sc1 = 0.5;

        //    double d = 24 * PLATE_WIDTH;
        //    int s = 6;
        //    int hgt = 16;

        //    double r = d/2;

        //    for (double x = -r; x < r; x += PLATE_WIDTH) {
        //        for (double z = -r; z < r; z += PLATE_WIDTH) {
        //            //Assume height is zero
        //            int h = 0;
        //            //am is the distance from the origin
        //            //the distance from the origin to your point is the hypotenuse of a right triangle!
        //            double am = Math.Sqrt(x*x + z*z);

        //            //if (rbCircularBase.IsChecked.Value) {
        //            double rEdge = r;
        //            //}

        //            if (am <= rEdge) {
        //                h = Convert.ToInt32(Math.Sqrt(r*r - am*am));
        //            }

        //            if (h <= 0) continue;

        //            int plates = h%3;
        //            int bricks = (h - plates)/3;
        //            //for (int i = 0;i < bricks; i++) {
        //            //    AddPart("3005", new Point3D(x, y, z), (int)h);
        //            //    y += (PLATE_HEIGHT*3);
        //            //}
        //            string partcode = "3024";
        //            Point3D pt = new Point3D(x, h, z);
        //            //for (int i = 0; i < plates; i++) {
        //            AddPart(partcode, pt);
        //            Part3D part = new Part3D(partcode, partcode) {Position = pt};

        //            list.Add(part);
        //            //y += PLATE_HEIGHT;
        //            //}
        //        }
        //    }
        //    return list;
        //}

        //private void LoadRandomParts() {
        //    int x = 0;
        //    for (int i = 3; i < 4; i++) {
        //        int y = 0;// PLATE_HEIGHT;
        //        for (int j = 0; j < i + 1; j++) {
        //            AddPart("3005", new Point3D(x, 0, y));

        //            y += PLATE_WIDTH;
        //        }

        //        x += PLATE_WIDTH;
        //    }
        //}

        //[System.Diagnostics.DebuggerStepThrough]
        //private void AddPart(string ptCode, Point3D pt) {
        //    //PartColors color = (mainViewport.Children.Count % 2) == 0 ? PartColors.Blue : PartColors.Red;
        //    //Part3D part = new Part3D(ptCode, ptCode, color) { Position = pt };
            
        //    ////_parts.Add(part);
        //    ////mainViewport.Children.Add(part);
        //}
    }
}
