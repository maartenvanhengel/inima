using Android.Net.IpSec.Ike;
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
    public partial class CommunicationViewModel: ObservableObject
    {
        Avatar avatar;
        [ObservableProperty]
        ObservableCollection<Message> items;

        public CommunicationViewModel()
        {
            avatar = new Avatar();
            avatar.ReadStatus();
            Title = "praat met " + avatar.Name;
            textVisuable = false;
            items = new ObservableCollection<Message>();
        }

        [ObservableProperty]
        string title;

        [ObservableProperty]
        string entryText;

        [ObservableProperty]
        string playerText;


        [ObservableProperty]
        bool textVisuable;

        [ObservableProperty]
        bool entryEnabled;

        [ICommand]
        public async void SendMessage()
        {
            TextVisuable = true;
            Thickness thickness = new Thickness() { Left = 5, Bottom = 0, Right = 35, Top = 5 };
            Message message = new Message() { Text = EntryText, Color = Colors.Red, Margin = thickness };
            Items.Add(message);

            await Task.Delay(200);
            Thickness thickness1 = new Thickness() { Left = 35, Bottom = 0, Right = 5, Top = 0 };
            Message message1 = new Message() { Text = "...", Color = Colors.Black, Margin = thickness1 };
            Items.Add(message1);
            PlayerText = EntryText;
            await Task.Delay(1000);
            if (EntryText != null)
            {
                getAnswer();
                EntryEnabled = false;

            }
        }
        public void getAnswer()
        {
            string text;
            EntryText = EntryText.ToLower();
            if (EntryText.Contains("zout"))
            {
                text = "je mag maximaal " + avatar.MaxSaltValue + " gram zout";
            }
            else if (EntryText.Contains("sport"))
            {
                text = "1keer per week fietsen of wandelen, elke dag botten versterken";
            }
            else if (EntryText.Contains("eten"))
            {
                text = "probeer niet te veel op hetzelfde moment te eten maar meer in kleinere porties. Vergeet ook niet te letten op het aantal zout";
            }
            else if (EntryText.Contains("drinken"))
            {
                text = "Je mag alles drinken maar drink vaker in kleinere porties";
            }
            else if (EntryText.Contains("goed") || EntryText.Contains("gaat"))
            {
                text = "alles gaat goet met mij☺";
            }
            else if (EntryText.Contains("hallo") || EntryText.Contains("goededag") || EntryText.Contains("hey") || EntryText.Contains("goedemorgen") || EntryText.Contains("goedeavond"))
            {
                text = "hallo!!";
            }
            else if (EntryText.Contains("wat")&& EntryText.Contains("doe")|| EntryText.Contains("wdj"))
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 3);
                if (random == 0)
                {
                    text = "ik ben aan het dansen in de virtuele wereld";
                }
                else if(random == 1)
                {

                    text = "ik ben een virtueel boek aan het lezen";
                }
                else
                {
                    text = "Ik ben rondjes aan het lopen tegen de verveling";
                }
            }
            else if (EntryText.Contains("grap")|| EntryText.Contains("mop"))
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 5);
                if (random == 0)
                {
                    text = "jantje gaat op bezoek bij zijn oma. \r\nOma zegt: ‘Wow, Jantje, wat ben jij gegroeid!’\r\nJantje antwoordt:\r\n“Nee hoor oma. U bent gewoon flink gekrompen!”";
                }
                else if (random == 1)
                {

                    text = "Waarom loopt een duizendpoot met 999 poten over een mesthoop?\r\nZijn 1000ste poot houd zijn neus dicht.";
                }
                else if (random == 2)
                {
                    text = "De leraar: \"Wat is het meervoud van baby?\"\r\n\r\nJantje: \"Tweeling!\" ";

                }
                else if (random == 3)
                {
                    text = "Wat is het toppunt van optimisme? \n Wachten totdat een Nederlander gaat trakteren";
                }
                else
                {
                    text = "Wat is de kortste maand?\r\nDat is mei, want die heeft maar drie letters.";
                }
            }
            else if (EntryText.Contains("naam"))
            {
                text = "Mijn naam is " + avatar.Name;
            }
            else if (EntryText.Contains("oud")|| EntryText.Contains("leeftijd"))
            {
                Random rnd = new Random();
                int random = rnd.Next(630720000, 1923696000);
                text = "Ik ben " + random + "seconden oud";
            }
            else if (EntryText.Contains("gemaakt") || EntryText.Contains("gevormd"))
            {
                text = "Ik ben gekomen uit een werld hier ver vandaan om jouw te helpen";
            }
            else if (EntryText.Contains("vertel") || EntryText.Contains("zeg"))
            {
                text = "praten is niks voor mij";
            }
            else if (EntryText.Contains("getal"))
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 10);
                text = random + " is mijn getal";
            }
            else if (EntryText.Contains("baas"))
            {
                text = "ik hou niet van bazen, die zijn zo groot";
            }
            else if (EntryText.Contains("leuk")|| EntryText.Contains("fijn"))
            {
                text = "wat niet leuk is, moet je leuk maken. Wat fijn is, moet je fijn houden";
            }
            else if (entryText.Contains("eet"))
            {
                if (EntryText.Contains("jij")|| EntryText.Contains("u"))
                {
                    text = "ik eet vandaag chips, met de smaak microships";
                }
                else
                {
                    text = "probeer niet te veel op hetzelfde moment te eten maar meer in kleinere porties. Vergeet ook niet te letten op het aantal zout";
                }
            }
            else if (EntryText.Contains("vraag"))
            {
                text = "ik ben hier om vragen te beantwoorden";
            }
            else if (EntryText.Contains("verjaardag")|| EntryText.Contains("proficiat"))
            {
                text = "Ik jarig? robots verjaren niet";
            }
            else if (EntryText.Contains("hart"))
            {
                text = "hartproblemen zijn niet leuk voor iedereen, maar zonder die problemen zou je mij wel niet kennen";
            }
            else if (EntryText.Contains("haha")|| EntryText.Contains("grappig"))
            {
                text = "ik ben de grappigste robot van de hele virtuele wereld";
            }
            else if (EntryText.Contains("trouw")|| EntryText.Contains("huwen"))
            {
                text = "ik hou niet van binden, ik heb bindingsangst";
            }
            else if (EntryText.Contains("hobby") ||EntryText.Contains("vrije tijd")|| EntryText.Contains("werk"))
            {
                text = "ik ben fulltime mensen helper";
            }
            else if (EntryText.Contains("lief"))
            {
                text = "ik ben lieve sjat";
            }
            else if (EntryText.Contains("wie")&& EntryText.Contains("ben"))
            {
                text = "ik ben " + avatar.Name + " jouw allerliefste robot vriend";
            }
            else if (EntryText.Contains("Hoe"))
            {
                text = "jij stelt mij vragen \n ik beantwoorde ze magisch";
            }
            else if (EntryText.Contains("vind"))
            {
                text = "ik ben een robot, ik heb niks te vinden";
            }
            else if (EntryText.Contains("robot"))
            {
                text = "ik ben een behulpzame robot, stel je vragen maar";
            }
            else if (EntryText.Contains("dom") || EntryText.Contains("fout")|| EntryText.Contains("niet waar"))
            {
                text = "ik doe ook maar mijn best...";
            }
            else
            {
                Random rnd = new Random();
                int random = rnd.Next(0, 4);
                switch (random)
                {
                    case 0:
                        text = "hier kan ik niet op antwoorden";
                        break;
                    case 1:
                        text = "ik begrijp je vraag niet, probeer hem anders te stellen";
                        break ;
                    case 2:
                        text = "zo slim ben ik nu ook weer niet";
                        break;

                    default:
                        text = "deze vraag zal je toch aan iemand anders moeten stellen";
                        break;
                }
                text = "hier kan ik niet op antwoorden";
            }
            int last = items.Count();
            Items.RemoveAt(last-1);
            EntryText = "";
            Thickness thickness = new Thickness() { Bottom = 0, Left = 35, Right = 5, Top = 0 };
            Message message = new Message() { Color = Colors.Black, Margin = thickness, Text = text };
            Items.Add(message);
        }
        [ICommand]
        public void EntryClick()
        {
            EntryEnabled = true;
        }
    }
}
