using System;
using MonkeyIsland1.Controllers;
using MonkeyIsland1.Views;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Hut : Location
    {
        public override void Event(Transporter t)
        {
            int preisProNacht = 8;
            while (true)
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetDescription()} ===~~~");
                if (t.pirate.GetCoins() < preisProNacht)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler, um ein Zimmer zu mieten!\nDu wurdest vor die Tür geworfen!");
                    Console.ReadLine();
                    break;
                }
                Animation.RPGPrint($"Du hast zur Zeit {t.pirate.GetCoins()} Taler.\nEin Zimmer kostet {preisProNacht} Taler die Nacht.\nWillst du ein Zimmer mieten? (j = ja)\n" + Output.back2mainMenue);
                if (Console.ReadKey().KeyChar != 'j')
                    break;
                Animation.RPGPrint("\nFür wie viele Nächte willst du übernachten? (max = 5)\n" + Output.back2mainMenue);
                if (!InputCheck.CheckUInt(out uInput) || uInput > 5)
                    break;
                else if (t.pirate.GetCoins() < uInput * preisProNacht)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler für so viele Nächte!");
                    continue;
                }
                t.pirate.SetCoins(t.pirate.GetCoins() - (int)(uInput) * preisProNacht);

                Animation.Sleep();

                if (t.pirate.GetDrunkenness() > 0)
                {
                    for (int i = 0; i < uInput; i++)
                        //Betrunkenheitslevel sinkt um 1-3 Punkte pro Nacht
                        t.pirate.SetDrunkenness(t.pirate.GetDrunkenness() - Program.rnd.Next(1, 4));
                    if (t.pirate.GetDrunkenness() < 0)
                        t.pirate.SetDrunkenness(0);
                    Animation.RPGPrint($"Dein Betrunkenheitslevel ist auf {t.pirate.GetDrunkenness()} gesunken.");
                }
                Animation.RPGPrint($"Du hast {uInput} Nacht/Nächte geschlafen und fühlst dich ausgeruht.");
                break;
            }
        }
    }
}
