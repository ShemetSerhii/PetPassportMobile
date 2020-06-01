
using System.ComponentModel;
using Xamarin.Forms;

using PetPassport.ViewModels;
using System.IO;
using System.Linq;
using PetPassport.Models;

namespace PetPassport.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            var image = new Image
            {
                Source = viewModel.Item.Picture != null ? ImageSource.FromStream(() => new MemoryStream(viewModel.Item.Picture)) : "placeholder.png",
                HeightRequest = 100,
                WidthRequest = 100
            };

            stack.Children.Insert(0, image);

            if (viewModel.MedicalRows.Any())
            {
                var label = new Label
                {
                    Text = "Медичні записи",
                    FontSize = 20,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                stack.Children.Add(label);

                var listView = new ListView
                {
                    HasUnevenRows = true,
                    ItemsSource = viewModel.MedicalRows,
                    ItemTemplate = new DataTemplate(() =>
                    {
                        var titleLabel = new Label();
                        titleLabel.SetBinding(Label.TextProperty, "Name");

                        var companyLabel = new Label();
                        companyLabel.SetBinding(Label.TextProperty, "Date");

                        // создаем объект ViewCell.
                        return new ViewCell
                        {
                            View = new StackLayout
                            {
                                Padding = new Thickness(0, 5),
                                Orientation = StackOrientation.Vertical,
                                Children = { titleLabel, companyLabel }
                            }
                        };
                    })
                };

                listView.ItemSelected += ListView_ItemSelected;

                stack.Children.Add(listView);
            }
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var medRow = e.SelectedItem as MedicalRow;

            await Navigation.PushAsync(new MedRowPage(medRow));
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            viewModel = new ItemDetailViewModel();
            BindingContext = viewModel;
        }
    }
}