using System.Windows.Controls;
using _3DTools;
using DomeCreator.ViewModel;

namespace DomeCreator.View
{
    /// <summary>
    /// Interaction logic for Dome3DView.xaml
    /// </summary>
    public partial class Dome3DView : UserControl
    {
        private Trackball _tracking = new Trackball();
        public Dome3DView() {
            InitializeComponent();
            SetupTracking();

            DataContextChanged += OnDome3DViewDataContextChanged;
        }

        void OnDome3DViewDataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e) {
            if (e.OldValue != null) return;

            MainWindowViewModel vm = ((MainWindowViewModel)e.NewValue);
            vm.Dome3DVM.Viewport = mainViewport;
        }

        private void SetupTracking() {
            _tracking.EventSource = CaptureBorder;
            Camera.Transform = _tracking.Transform;
            Headlight1.Transform = _tracking.Transform;
            //Headlight2.Transform = _tracking.Transform;
        }
    }
}
