using NUnit.Framework;
using Xamarin.Forms.Mocks;

namespace Xamarin.Forms.TestingLibrary.Tests.Support
{
    public abstract class MockedFormsTestBase
    {
        [SetUp]
        public void MockFormsSetUp() => MockForms.Init();
    }
}
