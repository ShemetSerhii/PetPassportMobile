using System;
using System.ComponentModel;
using Xamarin.Forms;

using PetPassport.Models;
using PetPassport.Services;

namespace PetPassport.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        private readonly PetClient _petClient = new PetClient();
        public PetCreate Item { get; } = new PetCreate
        {
            DateOfBirth = DateTime.UtcNow
        };

        public NewItemPage()
        {
            InitializeComponent();



            BindingContext = this;
        }

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            Item.DateOfBirth = e.NewDate;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);

            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}