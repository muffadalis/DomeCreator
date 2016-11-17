using System;

namespace DomeCreator.ViewModel
{
    public abstract class DomeViewModel : ApplicationViewModel
    {
        #region ctor

        protected DomeViewModel(MainWindowViewModel mainWindowModel) :base (mainWindowModel) {
        }
        #endregion

        #region Virtual Properties

        protected virtual double ScaleWidth { get; private set; }
        protected virtual double ScaleHeight { get; private set; }

        #endregion

        #region Public Functions
        protected double CalculateHeight(double rEdge, double radius, double distanceFromOrgin, double domeHeight) {
            double y = 0;
            switch (MainWindowVM.ParametersVM.DomeType) {
                case DomeType.Spherical:
                    y =  Math.Sqrt(radius * radius - distanceFromOrgin * distanceFromOrgin);
                    break;
                case DomeType.Planar:
                    y =  domeHeight * (rEdge - distanceFromOrgin) / rEdge;
                    break;
                case DomeType.Elliptical:
                    y =  domeHeight / radius * Math.Sqrt(radius * radius - distanceFromOrgin * distanceFromOrgin);
                    break;
            }

            // Snap to the closest grid point.
            if (y % ScaleHeight < ScaleHeight / 2)
                y = y - y % ScaleHeight; // if less than half way then snap at the lower point
            else
                y = y + (ScaleHeight - y % ScaleHeight); // snap at the higher point as it more that half from the mid point of the grid.

            return Math.Ceiling(y);
        }
        #endregion
    }
}
