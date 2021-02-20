using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xalendar.Api.Interfaces;
using Xalendar.Api.Models;

namespace Xamarin.Forms.TestingLibrary.SampleApp
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            Images = new ObservableCollection<string> {"image1", "imaga2", "image3"};
            Events = new ObservableCollection<ICalendarViewEvent>(new List<ICalendarViewEvent>
            {
                new Event("1", "A", DateTime.Now, DateTime.Now, true)
            });
        }

        public ObservableCollection<ICalendarViewEvent> Events { get; }

        private bool _tapped;
        public ICommand TapCommand => new Command(TapLabel);

        public bool Tapped
        {
            get => _tapped;
            set => SetProperty(ref _tapped, value);
        }

        private ObservableCollection<string> _images;
        public ObservableCollection<string> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        private void TapLabel()
        {
            Tapped = !Tapped;
        }
    }
}
