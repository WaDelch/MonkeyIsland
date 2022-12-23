using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Huette : Lokation
    {
        public void Event(ref Pirat ePirat)
        {
            int preisProNacht = 8;
            while (true)
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetBezeichnung()} ===~~~");
                if (ePirat.GetTaler() < preisProNacht)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler, um ein Zimmer zu mieten!\nDu wurdest vor die Tür geworfen!");
                    Console.ReadLine();
                    break;
                }
                Animation.RPGPrint($"Du hast zur Zeit {ePirat.GetTaler()} Taler.\nEin Zimmer kostet {preisProNacht} Taler die Nacht.\nWillst du ein Zimmer mieten? (j = ja)\n" + Program.menue);
                if (Console.ReadKey().KeyChar != 'j')
                    break;
                Animation.RPGPrint("\nFür wie viele Nächte willst du übernachten? (max = 5)\n" + Program.menue);
                if (!InputCheck.CheckUInt(out uInput) || uInput > 5)
                    break;
                else if (ePirat.GetTaler() < uInput * preisProNacht)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler für so viele Nächte!");
                    continue;
                }
                ePirat.SetTaler(ePirat.GetTaler() - (int)(uInput) * preisProNacht);

                Animation.Sleep();

                if (ePirat.GetBetrunkenheit() > 0)
                {
                    for (int i = 0; i < uInput; i++)
                        //Betrunkenheitslevel sinkt um 1-3 Punkte pro Nacht
                        ePirat.SetBetrunkenheit(ePirat.GetBetrunkenheit() - rnd.Next(1, 4));
                    if (ePirat.GetBetrunkenheit() < 0)
                        ePirat.SetBetrunkenheit(0);
                    Animation.RPGPrint($"Dein Betrunkenheitslevel ist auf {ePirat.GetBetrunkenheit()} gesunken.");
                }
                Animation.RPGPrint($"Du hast {uInput} Nacht/Nächte geschlafen und fühlst dich ausgeruht.");
                break;
            }
        }

    }
}
