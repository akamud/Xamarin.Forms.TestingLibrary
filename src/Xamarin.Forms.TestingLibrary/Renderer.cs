using System;
using System.Linq;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Mocks;
using Xamarin.Forms.TestingLibrary.Extensions;

namespace Xamarin.Forms.TestingLibrary
{
    /// <summary>
    /// The renderer class that is the entrypoint for the Testing Library.
    /// </summary>
    /// <typeparam name="TApp">The app class that represents your Xamarin.Forms application.</typeparam>
    public class Renderer<TApp>
        where TApp : Application, new()
    {
        internal readonly TApp _app;

        /// <summary>
        /// Constructor for a Renderer that allows skipping the auto <see cref="MockForms"/>.Init() call.
        /// <para>Use this if you are calling <see cref="MockForms"/>.Init() by yourself.</para>
        /// </summary>
        /// <param name="skipMockFormsInit">True if you want to skip the MockForms.Init() call. False if you want the Renderer to call the MockForms.Init() automatically.</param>
        public Renderer(bool skipMockFormsInit = false)
        {
            if (!skipMockFormsInit)
                MockForms.Init();
            _app = new TApp();
        }

        /// <summary>
        /// Renders the <typeparamref name="TPage"/> passed and returns a Screen representing its result.
        /// The Screen is the class that will serve as the entry point for all query methods.
        /// <para>Works only for Pages with an empty public constructor. If your page does not have an empty public constructor, use <see cref="Render{TPage}(TPage)"/> instead.</para>
        /// </summary>
        /// <typeparam name="TPage">The Xamarin.Forms Page that you wish to render.</typeparam>
        /// <returns>A Screen representing the rendering result.</returns>
        public Screen<TPage> Render<TPage>() where TPage : Page, new() => Render(new TPage());

        /// <summary>
        /// Renders the page object passed and returns a Screen representing its result.
        /// The Screen is the class that will serve as the entry point for all query methods.
        /// </summary>
        /// <typeparam name="TPage">The type of the Xamarin.Forms Page that you wish to render.</typeparam>
        /// <param name="page">The page object that you wish to render.</param>
        /// <returns>A Screen representing the rendering result.</returns>
        public Screen<TPage> Render<TPage>(TPage page)
            where TPage : Page
        {
            if (page == null)
            {
                throw new InvalidOperationException(
                    "Page cannot be null. Did you forget to pass a valid Page to your Renderer?");
            }

            // Many controls depend on a "Renderer" property being set to "initialize" itself.
            // We do this so every control that depends on this will be initialized as soon as the page is
            // added to the view hierarchy.
            foreach (var element in page.GetPageHierarchy<View>())
            {
                element.SetValue(RendererProperty, "TestingLibraryRenderer");
            }

            _app.MainPage = page;

            return new Screen<TPage>(page);
        }

        private static readonly BindableProperty RendererProperty = BindableProperty.CreateAttached("Renderer",
            typeof(string), typeof(string), default(string));

		/// <summary>
        /// Emulates a tap gesture in the passed Element. This will trigger any associated commands and events with the tap gesture.
        /// </summary>
        /// <param name="view">The view on which the Tap will be emulated.</param>
        /// <param name="numberOfTapsToSend">The number of Taps that you wish to send to the view.</param>
        public void Tap(View view, int numberOfTapsToSend = 1)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            view.GestureRecognizers.OfType<TapGestureRecognizer>()
                .Where(x => x.NumberOfTapsRequired == numberOfTapsToSend)
                .ForEach(x => x.SendTapped(view));
        }
    }
}
