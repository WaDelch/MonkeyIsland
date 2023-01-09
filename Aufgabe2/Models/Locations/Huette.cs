using System;
using MonkeyIsland1.Controllers;
using MonkeyIsland1.Views;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Huette : Lokation
    {
        public override void Event(Transporter t)
        {
            int preisProNacht = 8;
            while (true)
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetBezeichnung()} ===~~~");
                if (t.pirat.GetTaler() < preisProNacht)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler, um ein Zimmer zu mieten!\nDu wurdest vor die Tür geworfen!");
                    Console.ReadLine();
                    break;
                }
                Animation.RPGPrint($"Du hast zur Zeit {t.pirat.GetTaler()} Taler.\nEin Zimmer kostet {preisProNacht} Taler die Nacht.\nWillst du ein Zimmer mieten? (j = ja)\n" + Output.back2mainMenue);
                if (Console.ReadKey().KeyChar != 'j')
                    break;
                Animation.RPGPrint("\nFür wie viele Nächte willst du übernachten? (max = 5)\n" + Output.back2mainMenue);
                if (!InputCheck.CheckUInt(out uInput) || uInput > 5)
                    break;
                else if (t.pirat.GetTaler() < uInput * preisProNacht)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler für so viele Nächte!");
                    continue;
                }
                t.pirat.SetTaler(t.pirat.GetTaler() - (int)(uInput) * preisProNacht);

                Animation.Sleep();

                if (t.pirat.GetBetrunkenheit() > 0)
                {
                    for (int i = 0; i < uInput; i++)
                        //Betrunkenheitslevel sinkt um 1-3 Punkte pro Nacht
                        t.pirat.SetBetrunkenheit(t.pirat.GetBetrunkenheit() - rnd.Next(1, 4));
                    if (t.pirat.GetBetrunkenheit() < 0)
                        t.pirat.SetBetrunkenheit(0);
                    Animation.RPGPrint($"Dein Betrunkenheitslevel ist auf {t.pirat.GetBetrunkenheit()} gesunken.");
                }
                Animation.RPGPrint($"Du hast {uInput} Nacht/Nächte geschlafen und fühlst dich ausgeruht.");
                break;
            }
        }
    }
}
