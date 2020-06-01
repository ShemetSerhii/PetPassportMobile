using System;
using PetPassport.Models.UserModels;
using PetPassport.Services;
using PetPassport.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static PetPassport.Views.LoginPage;

namespace PetPassport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly AuthClient _authClient = new AuthClient();

        public delegate void LoginHandle();

        public static event LoginHandle Handler;
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = this;

        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var login = new Login
                {
                    Email = Email.Text,
                    Password = Password.Text
                };

                await _authClient.LoginAsync(login);

                if (_authClient.HasUser)
                {
                    Handler?.Invoke();

                    await Navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                Error.Text = "Невірно введений логін або пароль";
            }
        }
    }
}