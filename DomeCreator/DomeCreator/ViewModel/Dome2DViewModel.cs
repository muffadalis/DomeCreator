using System;
using System.Data;

namespace DomeCreator.ViewModel
{
    public class Dome2DViewModel : DomeViewModel
    {
        #region ctor
        public Dome2DViewModel(MainWindowViewModel mainWindowModel)
            : base(mainWindowModel) {
        }
        #endregion

        #region Public Properties
        public DataTable DomeValues {
            get { return _domeValue; }
            set {
                if (value == _domeValue)
                    return;

                _domeValue = value;
                base.OnPropertyChanged("DomeValues");
            }
        }
        #endregion

        #region Private Fields
        private DataTable _domeValue;
        #endregion

        #region Public Methods
        public void CalculateDomeValues() {
            int r = MainWindowVM.ParametersVM.DomeDiameter / 2;
            double r1 = r - ScaleHeight;
            double radius = r * ScaleWidth;
            double height = MainWindowVM.ParametersVM.DomeHeight*ScaleWidth;

            DataTable dt = new DataTable();
            for (int i = 0; i < MainWindowVM.ParametersVM.DomeDiameter; i++) {
                string columnName = (i + 1).ToString();
                DataColumn dc = new DataColumn(columnName, Type.GetType("System.Int32")) {DefaultValue = 0};
                dt.Columns.Add(dc);
            }

            for (double x = -r1; x <= r1; x += 1) {
                DataRow dr = dt.NewRow();

                for (double z = -r1; z <= r1; z += 1) {

                    double xScaled = x * ScaleWidth;
                    double zScaled = z * ScaleWidth;

                    //Assume height is zero
                    double y = 0;
                    //distanceFromOrgin is the distance from the origin
                    //the distance from the origin to your point is the hypotenuse of a right triangle!
                    double distanceFromOrgin = Math.Sqrt(xScaled * xScaled + zScaled * zScaled);

                    double rEdge = 0;
                    switch (MainWindowVM.ParametersVM.DomeBase) {
                        case DomeBase.Circular:
                            rEdge = radius;
                            break;
                    }
                    
                    if (distanceFromOrgin <= rEdge) {
                        y = CalculateHeight(rEdge, radius, distanceFromOrgin, height);
                    }

                    if (y <= 0) continue;

                    int k = (int)(z + r - ScaleHeight);

                    dr[k] = y;
                }

                dt.Rows.Add(dr);
            }

            DomeValues = dt;
        }
        #endregion

        #region overriden properties
        protected override double ScaleWidth {
            get { return 2.5; }
        }

        protected override double ScaleHeight {
            get { return 0.5; }
        }
        #endregion

        //private const double ScaleWidth = 2.5;
        //private const double ScaleHeight = 0.5;
    }
}
