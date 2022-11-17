using Android.OS;
using Android.Util;
using Java.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environment = System.Environment;

namespace inima.classes
{
    internal class Avatar
    {
        public string Name { get; set; }
        public double FoodProgress { get; set; }
        public double DrinkProgress { get; set; }
        public double SportProgress { get; set; }
        public string Status { get; set; } //hospital, normal, buikpijn
        public DateTime FoodTime { get; set; }
        public DateTime DrinkTime { get; set; }
        public DateTime SportTime { get; set; }
        public DateTime LastHospitalTime { get; set; }

        public List<Medicine> Medicines { get; set; }   

        public double SaltValue { get; set; }

        public int MaxSaltValue { get; set; }
        public int BetweenDrinkTime { get; set; }   
        public int BetweenFoodTime { get; set; }

        public void WriteStatus()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = System.IO.Path.Combine(folderPath, "InimaStatus.txt");
            try
            {
                StreamWriter stream = new StreamWriter(filePath);
                stream.WriteLine(Name+"£"+FoodProgress+"£" + FoodTime +"£"+DrinkProgress+ "£" + DrinkTime +"£"+ SaltValue+ "£" + LastHospitalTime + "£" + Status + "£" + SportProgress + "£" + SportTime);
                stream.WriteLine(ArrayToString());
                stream.WriteLine(MaxSaltValue + "£" + BetweenDrinkTime + "£" + BetweenFoodTime);
                stream.Close();
            }
            catch(Exception ex)
            {
                
            }
        }
        public async void ReadStatus()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string filePath = System.IO.Path.Combine(folderPath, "InimaStatus.txt");
            try
            {
                Medicines = new List<Medicine>();
                StreamReader stream = new StreamReader(filePath);
                String line = stream.ReadLine();
                string[] words = line.Split("£");
                Name = words[0];
                FoodProgress = Convert.ToDouble(words[1]);
                FoodTime = Convert.ToDateTime(words[2]);
                DrinkProgress = Convert.ToDouble(words[3]);
                DrinkTime = Convert.ToDateTime(words[4]);
                SaltValue = Convert.ToDouble(words[5]);
                DateTime timeNow = DateTime.Now;
                TimeSpan timeSpanFood = timeNow - FoodTime;
                TimeSpan timeSpanDrink = timeNow - DrinkTime;
                LastHospitalTime = Convert.ToDateTime(words[6]);
                Status = words[7];
                SportProgress = Convert.ToDouble(words[8]);
                SportTime = Convert.ToDateTime(words[9]);
                TimeSpan timeSpanSport = timeNow - SportTime;
                if (FoodTime.Day < timeNow.Day && DrinkTime.Day < timeNow.Day || FoodTime.Month < timeNow.Month && DrinkTime.Month < timeNow.Month) // new day
                {
                    SaltValue = 0;
                     foreach (Medicine medicine in Medicines) //check if al medicines are taken the day befor
                     {
                         if (medicine.isTaken == false)
                         {
                             LastHospitalTime = DateTime.Now;
                         }
                         medicine.isTaken = false;    //all medicines on false
                }
                }
                if (FoodTime.Day < timeNow.Day|| FoodTime.Month < timeNow.Month || FoodTime.Year< timeNow.Year) //nieuwe dag, 8 uur af voor slaap bij eten
                {
                    for (int i = 480; i < timeSpanFood.TotalMinutes; i++)
                    {
                        FoodProgress = FoodProgress - 0.001;
                    }
                }
                else
                {
                    for (int i = 0; i < timeSpanFood.TotalMinutes; i++)
                    {
                        FoodProgress = FoodProgress - 0.001;
                    }
                }
                if (DrinkTime.Day < timeNow.Day || DrinkTime.Month < timeNow.Month || DrinkTime.Year < timeNow.Year) //nieuwe dag, 8 uur af voor slaap bij drinken
                {
                    for (int i = 480; i < timeSpanDrink.TotalMinutes; i++)
                    {
                        DrinkProgress = DrinkProgress - 0.001;
                    }
                }
                else //normale procedure
                {
                    for (int i = 0; i < timeSpanDrink.TotalMinutes; i++)
                    {
                        DrinkProgress = DrinkProgress - 0.0017;
                    }
                }
                if (SportTime.Day< timeNow.Day || SportTime.Month < timeNow.Month || SportTime.Year < timeNow.Year)
                {
                    for (int i = 480; i < timeSpanSport.TotalMinutes; i++)
                    {
                        SportProgress = SportProgress - 0.002;
                    }
                }
                else
                {
                    for (int i = 0; i < timeSpanSport.TotalMinutes; i++)
                    {
                        SportProgress = SportProgress - 0.002;
                    }
                }
                if (FoodProgress < 0)
                {
                    FoodProgress = 0;
                }
                if (DrinkProgress <0)
                {
                    DrinkProgress = 0;
                }
                if (SportProgress <0)
                {
                    SportProgress = 0;
                }

                //medicines
                line = stream.ReadLine();
                if (line != null)
                {
                words = line.Split("£");
                    for (int i = 1; i < words.Length; i++)
                    {
                        string[] medicineData = words[i].Split("°");
                        Medicine medicine = new Medicine() { name = medicineData[0], time = TimeSpan.Parse(medicineData[1]), isTaken = Convert.ToBoolean(medicineData[2]) };
                        Medicines.Add(medicine);
                    }
                }

                //settings
                line = stream.ReadLine();
                if (line != null)
                {
                    words = line.Split("£");
                    MaxSaltValue = Convert.ToInt32(words[0]);
                    BetweenDrinkTime = Convert.ToInt32(words[1]);
                    BetweenFoodTime = Convert.ToInt32(words[2]);
                }  

                stream.Close();
                CheckStatus();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("test", ex.ToString(), "ok");
            }
        }
        public bool addSalt(double value)
        {
            SaltValue = SaltValue + value;
            if (SaltValue >  MaxSaltValue)
            {
                return false;
            }
            return true;
        }
        public void CheckStatus()
        {
            if (SaltValue > MaxSaltValue)
            {
                //LastHospitalTime = DateTime.Now;
                if (FoodProgress <= 0 && DrinkProgress <= 0)
                {
                    Status = "ziekenhuis";
                }
                else if (FoodProgress <= 0)
                {
                    Status = "uitgehongerd en uitgedroogd";
                }
                else if (DrinkProgress <= 0)
                {
                    Status = "uitedroogd";
                }
                else
                {
                    Status = "uitgedroogd (zout)";
                }
            }
            else
            {
                if (FoodProgress <= 0 && DrinkProgress <= 0)
                {
                    Status = "uitgehongerd en uitgedroogd";
                  //  LastHospitalTime = DateTime.Now;
                }
                else if (FoodProgress <= 0)
                {
                    Status = "uitgehongerd";
                  //  LastHospitalTime = DateTime.Now;
                }
                else if (DrinkProgress <= 0)
                {
                    Status = "uitedroogd";
                  //  LastHospitalTime = DateTime.Now;
                }
                else
                {
                    Status = "normal";
                }
            }
        }
        public string ArrayToString()
        {
            string text = "";
            foreach (Medicine item in Medicines)
            {
                text = text +"£"+ item.ToString();
            }
            return text;
        }
       
    }
}
