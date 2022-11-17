using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inima.classes
{
    internal class FoodRepository
    {
        //al the kinds of food
        //snacks
        Food snack1 = new Food() { Name = "snickers", FillValue = 0.05, SaltValue = 0.05 };
        Food snack2 = new Food() { Name = "soep", FillValue = 0.08, SaltValue = 2.05 };
        Food snack3 = new Food() { Name = "wafel", FillValue = 0.05, SaltValue = 0.34 };
        Food snack4 = new Food() { Name = "soep", FillValue = 0.06, SaltValue = 0.4 };
        Food snack5 = new Food() { Name = "fijnkost", FillValue = 0.06, SaltValue = 0.4 };

        List<Food> snacks = new List<Food>();

        //hot food
        Food food1 = new Food() { Name = "Aardappelen met hamburger", FillValue = 0.4, SaltValue = 2.24 };
        Food food2 = new Food() { Name = "Lasagne bolognese", FillValue = 0.3, SaltValue = 2.34 };
        Food food3 = new Food() { Name = "Kipfilet met wortelen", FillValue = 0.2, SaltValue = 2.5 };
        Food food4 = new Food() { Name = "frieten", FillValue = 0.3, SaltValue = 1.1 };
        Food food5 = new Food() { Name = "aardappelen met varkenslapje", FillValue = 0.3, SaltValue = 0.5 };
        Food food6 = new Food() { Name = "Vol au vent met kroketten", FillValue = 0.3, SaltValue = 0.78 };
        Food food7 = new Food() { Name = "Gehaktballetjes met krieken", FillValue = 0.3, SaltValue = 0.78 };
        Food food8 = new Food() { Name = "Pensen met appelmoes en gekookte aardappelen", FillValue = 0.3, SaltValue = 2.05 };
        Food food9 = new Food() { Name = "Broccoli met krieltjes en visfilet", FillValue = 0.23, SaltValue = 0.13 };
        Food food10 = new Food() { Name = "Zalm met fijne groentjes en puree", FillValue = 0.25, SaltValue = 0.23 };

        List<Food> foods = new List<Food>();
        public FoodRepository()
        {
            snacks.Add(snack1);
            snacks.Add(snack2);
            snacks.Add(snack3);
            snacks.Add(snack4);
            snacks.Add(snack5);

            foods.Add(food1);
            foods.Add(food2);
            foods.Add(food3);
            foods.Add(food4);
            foods.Add(food5);
            foods.Add(food6);
            foods.Add(food7);
            foods.Add(food8);
            foods.Add(food9);
            foods.Add(food10);
        }
        public Food GetSnack()
        {
            Random random = new Random();
            int rnd = random.Next(0, snacks.Count());
            return snacks[rnd];
        }
        public Food GetFood()
        {
            Random random = new Random();
            int rnd = random.Next(0, foods.Count());
            return foods[rnd];
        }
    }
}
