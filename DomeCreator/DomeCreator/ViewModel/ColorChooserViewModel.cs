using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using LDrawPartLib;

namespace DomeCreator.ViewModel
{
    public class ColorChooserViewModel : ApplicationViewModel
    {
        #region Private members
        private RelayCommand _changeColorCommand;
        private RelayCommand _showLinesCommand;
        private PartColor _currentColor;
        #endregion

        #region Public Properties
        public PartColor CurrentColor {
            get { return _currentColor; }
            set {
                if (value == _currentColor)
                    return;

                _currentColor = value;
                base.OnPropertyChanged("CurrentColor");
            }
        }

        public ObservableCollection<PartColor> PartColors { get; private set; }

        public ICommand ChangeColorCommand {
            get { return _changeColorCommand ?? (_changeColorCommand = new RelayCommand(ChangePartColor, CanChangeColor)); }
        }

        public ICommand ShowLinesCommand {
            get { return _showLinesCommand ?? (_showLinesCommand = new RelayCommand(ShowPartLines, CanChangeColor)); }
        }

        public bool CanChangeColor(object param) {
            return true;
        }

        public bool CanShowLines(object param) {
            return true;
        }
        #endregion

        #region Private Functions
        private void ChangePartColor(object color) {

            PartColors pc = LDrawPartLib.PartColors.Blue;

            try {
                pc = (PartColors)Enum.Parse(typeof(PartColors), color.ToString());
            }
            catch { }

            CurrentColor = KnownPartColors.GetColor(pc);
            MainWindowVM.Dome3DVM.ChangePartColor(pc);

        }

        private void ShowPartLines(object value) {

            try {
                MainWindowVM.Dome3DVM.ShowLines((bool)value);

            }
            catch { }
        }
        #endregion

        #region ctor
        public ColorChooserViewModel(MainWindowViewModel mainWindowModel) : base(mainWindowModel) {

            CurrentColor = KnownPartColors.GetColor(LDrawPartLib.PartColors.Blue);

            PartColors = new ObservableCollection<PartColor>();
            foreach (int code in KnownPartColors.Colors.Keys) {
                PartColors.Add(KnownPartColors.Colors[code]);
            }
        }
        #endregion
    }
}