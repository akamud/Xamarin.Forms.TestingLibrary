using MvvmHelpers;
using System.Windows.Input;

namespace Xamarin.Forms.TestingLibrary.SampleApp
{
    public class MainPageViewModel : ObservableObject
    {
        private bool _tapped;
        public ICommand TapCommand => new Command(TapLabel);

        public bool Tapped
        {
            get => _tapped;
            set => SetProperty(ref _tapped, value);
        }

        private void TapLabel()
        {
            Tapped = !Tapped;
        }
    }
}
