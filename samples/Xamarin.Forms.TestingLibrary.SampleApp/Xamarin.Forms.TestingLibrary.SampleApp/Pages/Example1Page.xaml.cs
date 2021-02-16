using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.TestingLibrary.SampleApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.TestingLibrary.SampleApp.Pages
{
    public partial class Example1Page : ContentPage
    {
        public Example1Page()
        {
            InitializeComponent();

            BindingContext = new Example1PageViewModel();
        }
    }
}

