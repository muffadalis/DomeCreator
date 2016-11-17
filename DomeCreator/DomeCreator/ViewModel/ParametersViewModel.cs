using System.Windows.Input;

namespace DomeCreator.ViewModel
{
    public class ParametersViewModel : ApplicationViewModel
    {
        #region ctor

        public ParametersViewModel(MainWindowViewModel mainWindowModel) : base(mainWindowModel) {
            DomeDiameter = 12;
            DomeHeight = 16;
        }
        #endregion

        #region Pubic Properties

        public ICommand CalculateCommand {
            get {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand(
                                                                     param => CalculateDomeValues(),
                                                                     param => CanCalculate));
            }
        }

        public bool CanCalculate {
            get { return (DomeDiameter > 0 && DomeHeight > 0); }
        }

        public DomeType DomeType {
            get { return _domeType; }
            set {
                if (value == _domeType)
                    return;

                _domeType = value;
                base.OnPropertyChanged("DomeType");
            }
        }

        public DomeBase DomeBase {
            get { return _domeBase; }
            set {
                if (value == _domeBase)
                    return;

                _domeBase = value;
                base.OnPropertyChanged("DomeBase");
            }
        }
        public int DomeDiameter {
            get { return _domeDiameter; }
            set {
                if (value == _domeDiameter)
                    return;

                _domeDiameter = value;
                base.OnPropertyChanged("DomeDiameter");
            }
        }
        public int DomeHeight {
            get { return _domeHeight; }
            set {
                if (value == _domeHeight)
                    return;

                _domeHeight = value;
                base.OnPropertyChanged("DomeHeight");
            }
        }
        #endregion

        #region Fields
        private DomeType _domeType;
        private DomeBase _domeBase;
        private int _domeHeight;
        private int _domeDiameter;
        private RelayCommand _calculateCommand;
        #endregion

        #region Private Functions
        private void CalculateDomeValues() {
            MainWindowVM.Dome2DVM.CalculateDomeValues();
            MainWindowVM.Dome3DVM.CalculateDomeValues();
        }
        #endregion
    }
}
