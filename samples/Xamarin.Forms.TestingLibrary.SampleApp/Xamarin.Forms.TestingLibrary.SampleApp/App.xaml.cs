using Xamarin.Forms.TestingLibrary.SampleApp.Pages;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Xamarin.Forms.TestingLibrary.SampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Example1Page();
        }
    }
}
