namespace Xamarin.Forms.TestingLibrary.SampleApp.Controls
{
    public class CustomStackLayout : StackLayout
    {
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Renderer")
            {
                Children.Add(new Label {Text = "CustomControlText"});
            }
        }
    }
}
