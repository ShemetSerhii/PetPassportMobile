using System.Collections.Generic;
using PetPassport.Models;

namespace PetPassport.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public PetInfo Item { get; }

        public List<MedicalRow> MedicalRows { get; }

        public ItemDetailViewModel(PetInfo item = null, List<MedicalRow> medicalRows = null)
        {
            Title = item.Name;
            Item = item;
            MedicalRows = medicalRows ?? new List<MedicalRow>();
        }
    }
}
