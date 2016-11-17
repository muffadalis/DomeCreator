
namespace DomeCreator.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region ctor
        public MainWindowViewModel() {
            ParametersVM = new ParametersViewModel(this);
            Dome2DVM = new Dome2DViewModel(this);
            Dome3DVM = new Dome3DViewModel(this);
            ColorChooserVM = new ColorChooserViewModel(this);
        }
        #endregion

        #region Public Properties
        public ParametersViewModel ParametersVM { get; set; }
        public Dome2DViewModel Dome2DVM { get; set; }
        public Dome3DViewModel Dome3DVM { get; set; }
        public ColorChooserViewModel ColorChooserVM { get; set; }
        #endregion

        #region Fields
        #endregion

        #region Private Functions
        #endregion

    }
}