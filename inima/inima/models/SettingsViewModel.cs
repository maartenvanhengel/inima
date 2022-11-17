using inima.classes;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inima.models
{
    public partial class SettingsViewModel :ObservableObject
    {
        Avatar avatar;
        public SettingsViewModel()
        {
            avatar = new Avatar();
            avatar.ReadStatus();
            MaxSaltValue = avatar.MaxSaltValue;
            BetweenDrinkTime = avatar.BetweenDrinkTime;
            BetweenFoodTime = avatar.BetweenFoodTime;
        }

        [ObservableProperty]
        int maxSaltValue;

        [ObservableProperty]
        int betweenDrinkTime;

        [ObservableProperty]
        int betweenFoodTime;

        [ICommand]
        public async void AplySettings()
        {
            avatar.MaxSaltValue = MaxSaltValue;
            avatar.BetweenDrinkTime = BetweenDrinkTime;
            avatar.BetweenFoodTime = BetweenFoodTime;
            avatar.WriteStatus();

            await App.Current.MainPage.DisplayAlert("Instellingen", "instellingen opgeslagen", "OK");
        }
    }
}
