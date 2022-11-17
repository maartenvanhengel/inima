using inima.classes;
using inima.views;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inima.models
{
    public partial class AvatarViewModel:ObservableObject
    {
        public AvatarViewModel()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = System.IO.Path.Combine(folderPath, "InimaStatus.txt");

            if (File.Exists(filePath) == false)
            {
                App.Current.MainPage.DisplayAlert("new user", "make an avatar to play with", "ok");
            }
            else
            {

            }
        }
        //new user
        [ObservableProperty]
        string name;

        [ICommand]
        public async void AddUser()
        {
            Avatar avatar = new Avatar();
            avatar.Name = Name;
            avatar.FoodProgress = 1;
            avatar.DrinkProgress = 1;
            avatar.Status = "normal";
            avatar.FoodTime = DateTime.Now;
            avatar.DrinkTime = DateTime.Now;
            avatar.LastHospitalTime = DateTime.Now;
            avatar.Medicines = new List<Medicine>();
            avatar.MaxSaltValue = 4;
            avatar.BetweenDrinkTime = 30;
            avatar.BetweenFoodTime = 30;
            avatar.SportProgress = 1;
            avatar.SportTime = DateTime.Now;

            avatar.WriteStatus();

            await App.Current.MainPage.DisplayAlert("new user", "used added", "ok");
            await Shell.Current.GoToAsync("../");
        }
    }
}
