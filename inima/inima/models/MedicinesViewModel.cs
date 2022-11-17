using inima.classes;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inima.models
{
    public partial class MedicinesViewModel : ObservableObject
    {

        [ObservableProperty]
        List<string> items;
        Avatar avatar = new Avatar();
        public MedicinesViewModel()
        {
            Items = new List<string>();
            PickerTime = new TimeSpan(8, 0, 0);
            avatar.ReadStatus();
            SetMedicines();
        }
        [ObservableProperty]
        TimeSpan pickerTime;

        [ObservableProperty]
        string name;

        [ICommand]
        public async void Add()
        {
            Medicine medicine = new Medicine() { name = Name, time = PickerTime };
            if (avatar.Medicines == null)
            {
                avatar.Medicines = new List<Medicine>();
            }
            avatar.Medicines.Add(medicine);

            avatar.WriteStatus();

            SetMedicines();
            await App.Current.MainPage.DisplayAlert("Medicijn", "medicijn toegevoegd", "OK");

            
        }
        public void SetMedicines()
        {         
            avatar.ReadStatus();

            foreach (Medicine medicine in avatar.Medicines)
            {
                Items.Add(medicine.name);
            }

            if (Items.Count() == 0)
            {
                Items.Add("nog geen medicijnen toegevoegd");
            }
        }
    }
}
