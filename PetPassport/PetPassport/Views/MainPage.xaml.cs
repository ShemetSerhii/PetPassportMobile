using PetPassport.Services;
using System.ComponentModel;
using Xamarin.Forms;

namespace PetPassport.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (ApiClient.User == null)
            {
                Navigation.PushModalAsync(new LoginPage()).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }
    }
}