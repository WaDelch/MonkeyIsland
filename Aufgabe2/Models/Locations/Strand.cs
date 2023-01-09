using System;
using MonkeyIsland1.Views;
using MonkeyIsland1.Controllers;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Strand : Lokation
    {
        public override void Event(Transporter t)
        {
            do
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetBezeichnung()} ===~~~");
                Animation.RPGPrint("Du gräbst nach Schätzen.");
                System.Threading.Thread.Sleep(200);
                Animation.DigSite();
                randomZahl = rnd.Next(1, 101);
                if (randomZahl < 16) // 15% Chance 10 Taler zu finden
                {
                    Animation.RPGPrint("Du hast einen Sack mit zehn Talern gefunden!");
                    t.pirat.SetTaler(t.pirat.GetTaler() + 10);
                }
                else if (randomZahl < 81) // 65% Chance einen Taler zu finden
                {
                    Animation.RPGPrint("Du hast einen Taler gefunden!");
                    t.pirat.SetTaler(t.pirat.GetTaler() + 1);
                }
                else
                    Animation.RPGPrint("Du hast nichts gefunden.");
                Animation.RPGPrint($"Du hast jetzt {t.pirat.GetTaler()} Taler.");
                Animation.RPGPrint("Willst du weitergraben? (j = ja)\n" + Output.back2mainMenue);
            } while (Console.ReadKey().KeyChar == 'j');
        }
    }
}
