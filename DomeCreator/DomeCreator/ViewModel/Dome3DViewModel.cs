using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using LDrawPartLib;

namespace DomeCreator.ViewModel
{
    public class Dome3DViewModel : DomeViewModel
    {
        #region Public Properties
        public Viewport3D Viewport { private get; set; }
        #endregion

        #region ctor
        public Dome3DViewModel(MainWindowViewModel mainWindowModel)
            : base(mainWindowModel) {}
        #endregion

        #region Private Functions
        private void ClearViewport() {
            // cannot use the clear method as it will remove the camera as well.
            for (int i = Viewport.Children.Count - 1; i >= 0; i--) {
                ModelVisual3D m = Viewport.Children[i] as Part3D;

                if (m == null) continue;
                Viewport.Children.Remove(m);
            }
        }

        private void FillNeighbours(int diameter, int z, DataRow drPrev, List<int> neighbours) {
            if (z > 0) neighbours.Add(Convert.ToInt32(drPrev[z - 1]));
            if (z < diameter - 1) neighbours.Add(Convert.ToInt32(drPrev[z + 1]));
            neighbours.Add(Convert.ToInt32(drPrev[z]));
        }
        
        #endregion

        //public void CalculateDomeValues() {
        //    List<Point3D> parts = new List<Point3D>();

        //    double radius = (MainWindowVM.ParametersVM.DomeDiameter * ScaleWidth) / 2;
        //    double height = MainWindowVM.ParametersVM.DomeHeight * ScaleWidth;

        //    for (double x = -radius; x < radius + 1; x += ScaleWidth)

        //        for (double z = -radius; z <= radius; z += ScaleWidth) {

        //            //Assume height is zero
        //            double y = 0;
        //            //distanceFromOrgin is the distance from the origin
        //            //the distance from the origin to your point is the hypotenuse of a right triangle!
        //            double distanceFromOrgin = Math.Sqrt(x * x + z * z);

        //            //if (rbCircularBase.IsChecked.Value) {
        //            double rEdge = radius;
        //            //}

        //            if (distanceFromOrgin <= rEdge) {
        //                y = CalculateHeight(rEdge, radius, distanceFromOrgin, height);
        //                //y = Convert.ToInt32( Math.Sqrt(radius * radius - distanceFromOrgin * distanceFromOrgin));
        //            }

        //            if (y <= 0) continue;

        //            // Snap to the closest lego plate.
        //            //if (y % ScaleHeight < ScaleHeight / 2) 
        //            //    y = y - y % ScaleHeight; // Snap at the lower point
        //            //else
        //            //    y = y + (ScaleHeight - y%ScaleHeight); // snap at the higher point as it more that half from the cen
        //            parts.Add(new Point3D(x, y, z));
        //        }
        //    //DomeValues = parts;
        //}

        #region Public Values
        public void CalculateDomeValues() {

            // remove all the existing parts.
            ClearViewport();

            DataTable dt = MainWindowVM.Dome2DVM.DomeValues;
            int diameter = MainWindowVM.ParametersVM.DomeDiameter;
            //int radius = diameter/2;

            for (int x = 0; x < diameter; x++) {
                for (int z = 0; z < diameter; z++) {
                    DataRow drPrev = null, drNext = null;
                    List<int> neighbours = new List<int>();
                    int noOfPlates;

                    // Get the rows from the datatable.
                    DataRow drCurrent = dt.Rows[x];
                    if (x > 0) drPrev = dt.Rows[x - 1];
                    if (x < dt.Rows.Count - 1) drNext = dt.Rows[x + 1];

                    int currentHeight = Convert.ToInt16(drCurrent[z]);

                    // Fill all the values from the surrounding neighbours.
                    FillNeighbours(diameter, z, drCurrent, neighbours);
                    if (drPrev != null) FillNeighbours(dt.Rows.Count, z, drPrev, neighbours);
                    if (drNext != null) FillNeighbours(dt.Rows.Count, z, drNext, neighbours);

                    // Gets the lowest values on the top.
                    neighbours.Sort();

                    // no of viewable plates.
                    if (drNext == null || drPrev == null || z == 0 || z == (diameter - 1)) // if dome edges then show all the plates.
                        noOfPlates = currentHeight;
                    else if (currentHeight == neighbours[0] && currentHeight > 0) // If lowest neighbours is of the current height then add only one plate.
                        noOfPlates = 1;
                    else
                        noOfPlates = currentHeight - neighbours[0]; // no need the show all the plate. Just show the 'viewable' plates.


                    int y = (currentHeight - noOfPlates) * (int)ScaleHeight;
                    for (int i = 0; i < noOfPlates; i++, y += (int)ScaleHeight) {

                        Point3D pt = new Point3D(x * ScaleWidth, y, z * ScaleWidth);

                        PartColors color =
                            (PartColors)Enum.Parse(typeof(PartColors), MainWindowVM.ColorChooserVM.CurrentColor.Name);

                        Part3D part = new Part3D(PLATE_PART_CODE, PLATE_PART_CODE, color) { Position = pt };
                        //part.ShowLines = true;

                        Viewport.Children.Add(part);
                    }
                }
            }
        }

        public void ShowLines(bool value) {
            for (int i = Viewport.Children.Count - 1; i >= 0; i--) {
                Part3D p = Viewport.Children[i] as Part3D;

                if (p == null) continue;

                p.ShowLines = value;
            }
        }

        public void ChangePartColor(PartColors color) {
            for (int i = Viewport.Children.Count - 1; i >= 0; i--) {
                Part3D p = Viewport.Children[i] as Part3D;

                if (p == null) continue;

                p.PartColor = color;
            }
        }

        #endregion

        #region Overridden Properties
        protected override double ScaleWidth {
            get { return 20; }
        }

        protected override double ScaleHeight {
            get { return 8; }
        }
        #endregion

        #region Private Const
        private const string PLATE_PART_CODE = "3024";
        //private const string BRICK_PART_CODE = "3005";
        #endregion

    }
}