namespace Xamarin.Forms.TestingLibrary
{
    public class Screen<TPage> where TPage : Page
    {
        public TPage Container { get; }

        public Screen(TPage page)
        {
            Container = page;
        }
    }
}
