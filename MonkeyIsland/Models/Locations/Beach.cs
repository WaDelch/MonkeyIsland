using System;
using MonkeyIsland1.Views;
using MonkeyIsland1.Controllers;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Beach : Location
    {
        public override void Event(Transporter t)
        {
            while(true)
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetDescription()} ===~~~");
                Animation.RPGPrint("Du gräbst nach Schätzen.");
                System.Threading.Thread.Sleep(200);
                Animation.DigSite();
                randomInt = Program.rnd.Next(1, 101);
                if (randomInt < 16) // 15% Chance 10 Taler zu finden
                {
                    Animation.RPGPrint("Du hast einen Sack mit zehn Talern gefunden!");
                    t.pirate.SetCoins(t.pirate.GetCoins() + 10);
                }
                else if (randomInt < 81) // 65% Chance einen Taler zu finden
                {
                    Animation.RPGPrint("Du hast einen Taler gefunden!");
                    t.pirate.SetCoins(t.pirate.GetCoins() + 1);
                }
                else
                    Animation.RPGPrint("Du hast nichts gefunden.");
                Animation.RPGPrint($"Du hast jetzt {t.pirate.GetCoins()} Taler.");
                Animation.RPGPrint("Willst du weitergraben? (j = ja)\n" + Output.back2mainMenue);
                if (!InputCheck.CheckString())
                    break;
            }
        }
    }
}
