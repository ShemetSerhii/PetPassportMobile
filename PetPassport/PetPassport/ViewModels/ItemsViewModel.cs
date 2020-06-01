using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

using PetPassport.Models;
using PetPassport.Views;
using PetPassport.Services;

namespace PetPassport.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly PetClient _petClient;

        public List<Pet> Pets { get; set; }
        public Command LoadItemsCommand { get; set; }

        public delegate void CreateHandle();

        public event CreateHandle Handler;

        public ItemsViewModel()
        {
            _petClient = new PetClient();

            Title = "Домашні тварини";
            Pets = new List<Pet>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, PetCreate>(this, "AddItem", async (obj, item) =>
                {
                    _petClient.AddPetAsync(item);

                    Handler?.Invoke();
                });
        }

        public Task<PetInfo> GetPetInfo(Guid id)
        {
            return _petClient.GetPetAsync(id);
        }

        public Task<IEnumerable<MedicalRow>> GetMedicals(Guid id)
        {
            return _petClient.GetMedicalsRowsAsync(id);
        }


        public async Task GetPetsAsync()
        {
            Pets = (await _petClient.GetPetsAsync()).ToList();
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Pets.Clear();
                var pets = await _petClient.GetPetsAsync();
                foreach (var item in pets)
                {
                    Pets.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}