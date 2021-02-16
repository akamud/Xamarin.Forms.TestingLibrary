using MvvmHelpers;

namespace Xamarin.Forms.TestingLibrary.SampleApp.ViewModels
{
    public class Example1PageViewModel : ObservableObject
    {
        private string _userName;

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public Example1PageViewModel()
        {
            UserName = "Marvin";
        }
    }
}
