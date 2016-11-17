namespace DomeCreator.ViewModel
{
    public abstract class ApplicationViewModel : ViewModelBase
    {
        #region ctor
        protected ApplicationViewModel(MainWindowViewModel mainWindowModel) {
            MainWindowVM = mainWindowModel;
        }
        #endregion

        #region Public Properties
        public MainWindowViewModel MainWindowVM { get; set; }
        #endregion
    }
}
