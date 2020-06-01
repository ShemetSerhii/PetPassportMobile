using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

using PetPassport.Models;
using PetPassport.ViewModels;
using PetPassport.Services;

namespace PetPassport.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
            viewModel.LoadItemsCommand.Execute(null);

            LoginPage.Handler += Load;
            viewModel.Handler += Load;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Pet item))
                return;

            var info = await viewModel.GetPetInfo(item.Id);
            var medicals = (await viewModel.GetMedicals(item.Id)).ToList();

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(info, medicals)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        private async void Load()
        {
            await viewModel.GetPetsAsync();

            if (stack.Children.Count > 1)
            {
                stack.Children.RemoveAt(1);
            }

            var listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = viewModel.Pets,
                ItemTemplate = new DataTemplate(() =>
                {
                    var titleLabel = new Label { FontSize = 18 };
                    titleLabel.SetBinding(Label.TextProperty, "Name");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Vertical,
                            Children = { titleLabel}
                        }
                    };
                })
            };

            listView.ItemSelected += OnItemSelected;
            listView.Refreshing += ListView_Refreshing;

            stack.Children.Add(listView);
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            Load();
        }
    }
}