using Android.Service.Voice;
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
    public partial class MainPageViewModel : ObservableObject
    {
        FoodRepository foodRepository;

        [ObservableProperty]
        double progressFoodValue = 0.5;


        [ObservableProperty]
        double progressDrinkValue = 0.5;

        [ObservableProperty]
        double progressSportValue;

        Avatar avatar;
        public MainPageViewModel()
        {
            //declaratie
            LeftMenuVisuable = false;
            StreakVisuable = false;
            MenuVisuable = true;
            SubMenuVisuable = false;
            TakeMedicinesVisuable = false;

            HamburgerImage = "hamburger.png";

            avatar = new Avatar();
            foodRepository = new FoodRepository();

            //check if file exist
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = System.IO.Path.Combine(folderPath, "InimaStatus.txt");

            if (File.Exists(filePath) == false)
            {
                Shell.Current.GoToAsync(nameof(AvatarPage));
                avatar.ReadStatus();
            }
            else
            {
                UpdateView();
            }
        }
        public void UpdateView()
        {
            avatar.ReadStatus();
            CheckMedicines();

            Name = avatar.Name;
            ProgressFoodValue = avatar.FoodProgress;
            ProgressDrinkValue = avatar.DrinkProgress;
            ProgressSportValue = avatar.SportProgress;
            Status = avatar.Status;

            TimeSpan hostpitalspan = DateTime.Now - avatar.LastHospitalTime;
            if (hostpitalspan.Days > 0)
            {
                StreakValue = hostpitalspan.Days;
                StreakVisuable = true;
            }
        }
        //status
        [ObservableProperty]
        string status;
        //left side menu
        [ObservableProperty]
        string hamburgerImage;

        [ObservableProperty]
        bool leftMenuVisuable;

        [ObservableProperty]
        string name;

        [ICommand]
        public async void GoToMedicines()
        {
            HamburgerKlik();
            await Shell.Current.GoToAsync($"{nameof(MedicinesPage)}?TextAlignment=test");
        }
        [ICommand]
        public async void GoToAvatar()
        {
            HamburgerKlik();
            await Shell.Current.GoToAsync(nameof(AvatarPage));
        }
        [ICommand]
        public async void GoToSettings()
        {
            HamburgerKlik();
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }
        
        [ICommand]
        public void HamburgerKlik()
        {
            if (LeftMenuVisuable == true)
            {
                HamburgerImage = "hamburger.png";
            }
            else
            {
                HamburgerImage = "close.png";
            }
            LeftMenuVisuable =! LeftMenuVisuable;
        }
        //streak
        [ObservableProperty]
        bool streakVisuable = false;

        [ObservableProperty]
        double streakValue;
        //menu
        [ObservableProperty]
        bool menuVisuable;

        [ObservableProperty]
        bool subMenuVisuable;
        [ICommand]
        public void GoBack()
        {
            FoodOptionsVisuable = false;
            DrinkOptionsVisuable = false;
            SportOptionsVisuable = false;
            MenuVisuable = true;
        }
        //food
        [ObservableProperty]
        bool foodOptionsVisuable = false;

        [ObservableProperty]
        string food1Name;

        [ObservableProperty]
        string food2Name;

        [ObservableProperty]
        string food3Name;

        Food food1;
        Food food2;
        Food food3;
        [ICommand]
        public void AddFood()
        {
            FoodOptionsVisuable = true;
            MenuVisuable = false;
            food1 = foodRepository.GetSnack();
            food2 = foodRepository.GetFood();
            food3 = foodRepository.GetFood();
            while (food3 == food2)
            {
                foodRepository.GetFood();
            }
            //toevoegen van welke etens gaat anders bepaald worden
            Food1Name = food1.Name;
            Food2Name = food2.Name;
            Food3Name = food3.Name;
        }
        [ICommand]
        public async void SelectFood1()
        {
            if (food1.Name == "fijnkost")    //eten van  bepaalde producten is verboden
            {
                await App.Current.MainPage.DisplayAlert("eten", "Geen fijnkost kut", "ok");
                avatar.Status = "buikpijn";
            }
            else
            {
                AddFoodProgress(food1.FillValue);
                if (avatar.addSalt(food1.SaltValue) == false) //toevoegen van zout, false indien te veel zout
                {
                    await App.Current.MainPage.DisplayAlert("salt", "te veel zout", "ok"); //bepalen wat te doen met zout
                }
            }
        }
        [ICommand]
        public async void SelectFood2()
        {
            if (food2.Name == "fijnkost")    //eten van  bepaalde producten is verboden
            {
                await App.Current.MainPage.DisplayAlert("eten", "Geen fijnkost kut", "ok");
            }
            else
            {
                AddFoodProgress(food2.FillValue);
                if (avatar.addSalt(food2.SaltValue) == false) //toevoegen van zout, false indien te veel zout
                {
                    await App.Current.MainPage.DisplayAlert("salt", "te veel zout", "ok"); //bepalen wat te doen met zout
                }
            }
        }
        [ICommand]
        public async void SelectFood3()
        {
            if (food3.Name == "fijnkost")    //eten van  bepaalde producten is verboden
            {
                await App.Current.MainPage.DisplayAlert("eten", "Geen fijnkost kut", "ok");
            }
            else
            {
                AddFoodProgress(food3.FillValue);
                if (avatar.addSalt(food3.SaltValue) == false) //toevoegen van zout, false indien te veel zout
                {
                    await App.Current.MainPage.DisplayAlert("salt", "te veel zout", "ok"); //bepalen wat te doen met zout
                }
            }
        }
        public async void AddFoodProgress(double value)
        {
            TimeSpan timeSpan = DateTime.Now - avatar.FoodTime;
            if (timeSpan.TotalMinutes < avatar.BetweenFoodTime)
            {
                await App.Current.MainPage.DisplayAlert("eten", "eet minder op 1 moment, beter op verschillende momenten", "ok");
            }
            else
            {
                ProgressFoodValue = ProgressFoodValue + value;
                if (ProgressFoodValue > 1)
                 {
                    ProgressFoodValue = 1;
                }
            else if (ProgressFoodValue < 0)
                {
                    ProgressFoodValue = 0;
                }
            avatar.FoodProgress = ProgressFoodValue;
            avatar.FoodTime = DateTime.Now;
            avatar.WriteStatus();
                UpdateView();
            }

        }
        //drinks
        [ObservableProperty]
        bool drinkOptionsVisuable = false;

        [ObservableProperty]
        string drink1Name;

        [ObservableProperty]
        string drink2Name;

        [ObservableProperty]
        string drink3Name;

        [ICommand]
        public void AddDrink()
        {
            DrinkOptionsVisuable = true;
            MenuVisuable = false;
            DateTime dateTime = DateTime.Now;
            DayOfWeek dayOfWeek = dateTime.DayOfWeek;
            if (DateTime.Now.Hour > 3 && DateTime.Now.Hour < 11 )
            {
                Drink1Name = "melk";
            }
            else
            {
                if (dateTime.DayOfWeek == DayOfWeek.Friday || dateTime.DayOfWeek == DayOfWeek.Saturday) //bier in avonden in de weekenden
                {
                    Drink1Name = "Jupiler";
                }
                else
                {
                    Drink1Name = "cola";
                }
            }
            //toevoegen van welke dranken gaat anders bepaald worden
        }
        [ICommand]
        public async void SelectDrink1()
        {
            if (Drink1Name == "Jupiler")
            {
                await App.Current.MainPage.DisplayAlert("alcohol", "geen alcohol", "ok");
            }
            else
            {
                AddDrinkProgress(0.25);
            }
            
        }
        [ICommand]
        public void SelectDrink2()
        {
            AddDrinkProgress(0.3);
        }
        [ICommand]
        public void SelectDrink3()
        {
            AddDrinkProgress(0.25);
        }
        public async void AddDrinkProgress(double value)
        {
            TimeSpan timeSpan = DateTime.Now - avatar.DrinkTime;
            if (timeSpan.TotalMinutes < avatar.BetweenDrinkTime)
            {
                await App.Current.MainPage.DisplayAlert("drank", "drink minder op 1 moment, beter op verschillende momenten", "ok");
            }
            else
            {
                ProgressDrinkValue = ProgressDrinkValue + value;
                if (ProgressDrinkValue > 1)
                {
                    ProgressDrinkValue = 1;
                }
                else if (ProgressDrinkValue < 0)
                {
                    ProgressDrinkValue = 0;
                }
                avatar.DrinkProgress = ProgressDrinkValue;
                avatar.DrinkTime = DateTime.Now;
                avatar.WriteStatus(); 
                UpdateView();
            }
        }
        //sport
        [ObservableProperty]
        bool sportOptionsVisuable = false;

        [ObservableProperty]
        string sport1Name;

        [ObservableProperty]
        string sport2Name;

        [ObservableProperty]
        string sport3Name;

        [ICommand]
        public void AddSport()
        {
            MenuVisuable = false;
            SportOptionsVisuable = true;

            Sport1Name = "oefeningen voor botten";
            Sport2Name = "springen";
            Sport3Name = "fietsen";
        }
        [ICommand]
        public void SelectSport1()
        {
            AddSportProgress(0.2);
        }
        [ICommand]
        public void SelectSport2()
        {
            AddSportProgress(0.2);
        }
        [ICommand]
        public void SelectSport3()
        {
            AddSportProgress(0.5);
        }
        public void AddSportProgress(double value)
        {
            avatar.SportTime = DateTime.Now;
            ProgressSportValue = ProgressSportValue + value;
            if (ProgressSportValue > 1)
            {
                ProgressSportValue = 1;
            }
            avatar.SportProgress = ProgressSportValue;
            avatar.WriteStatus();
            UpdateView();
        }
        //medicines
        [ObservableProperty]
        bool takeMedicinesVisuable;
        public async void CheckMedicines()
        {
            DateTime NowTime = DateTime.Now;
            DateTime StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan EffectiveSpan = NowTime - StartTime;
            foreach (Medicine medicine in avatar.Medicines)
            {
                if (EffectiveSpan.TotalMinutes - 30 < medicine.time.TotalMinutes && medicine.time.TotalMinutes < EffectiveSpan.TotalMinutes +30 && medicine.isTaken == false) //half uur voor en na de tijd
                {
                    TakeMedicinesVisuable = true;
                }
            }
        }
        [ICommand]
        public void TakeMedicines()
        {
            DateTime NowTime = DateTime.Now;
            DateTime StartTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan EffectiveSpan = NowTime - StartTime;
            foreach (Medicine medicine in avatar.Medicines)
            {
                if (EffectiveSpan.TotalMinutes - 30 < medicine.time.TotalMinutes && medicine.time.TotalHours < EffectiveSpan.TotalMinutes + 30 && medicine.isTaken == false) //half uur voor en na de tijd
                {
                    medicine.isTaken = true;
                }
            }
            TakeMedicinesVisuable = false;

            avatar.WriteStatus();
            UpdateView();
        }

        //communicationPage
        [ICommand]
        public async void GoToCommunication()
        {
            await Shell.Current.GoToAsync(nameof(CommunicationPage));
        }
    }
}
