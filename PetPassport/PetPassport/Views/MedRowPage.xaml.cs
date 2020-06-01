using PetPassport.Models;
using PetPassport.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PetPassport.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedRowPage : ContentPage
    {
        MedicalRow MedicalRow;

        private readonly PetClient _petClient = new PetClient();

        public MedRowPage(MedicalRow medicalRow)
        {
            InitializeComponent();

            Title = "Медичний запис";

            MedicalRow = medicalRow;

            var rowName = new Label {Text = MedicalRow.Name, FontSize = 18 };
            var rowDate = new Label {Text = MedicalRow.Date.ToShortDateString(), FontSize = 18 };

            var attachments = new Label { Text = "Документи", FontSize = 18, HorizontalTextAlignment = TextAlignment.Center };

            var listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = MedicalRow.Attachments,
                ItemTemplate = new DataTemplate(() =>
                {
                    var titleLabel = new Label { FontSize = 18 };
                    titleLabel.SetBinding(Label.TextProperty, "FileName");

                    var companyLabel = new Label { FontSize = 18 };
                    companyLabel.SetBinding(Label.TextProperty, "CreationDate");

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

            stack.Children.Add(rowName);
            stack.Children.Add(rowDate);
            stack.Children.Add(listView);
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var attachment = e.SelectedItem as AttachmentDto;

            await _petClient.DownloadFile(attachment);
        }
    }
}