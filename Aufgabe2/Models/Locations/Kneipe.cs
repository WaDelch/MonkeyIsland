using System;
using MonkeyIsland1.Controllers;
using MonkeyIsland1.Views;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Kneipe : Lokation
    {
        string[] getraenkeListe = { "Wasser", "Bier", "Grog", "Selbstgebraute Hausmarke" };
        int[] getraenkePreise = { 1, 3, 4, 6 };     

        public override void Event(Transporter t)
        {
            char uInput2 = 'j';
            do
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetBezeichnung()} ===~~~");
                if (t.pirat.GetTaler() < 1)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler für Getränke!\n" +
                        "Schnorrer fliegen vor die Tür!");
                    break;
                }
                Animation.RPGPrint("Hier ist die Getränkeliste: ");
                for (int i = 0; i < getraenkeListe.Length; i++)
                    Animation.RPGPrint($"{i + 1}) {getraenkeListe[i]} - {getraenkePreise[i]} Taler");
                Animation.RPGPrint(Output.back2mainMenue + "\nDu hast zur Zeit " + t.pirat.GetTaler() + " Taler.\nWas möchtest du bestellen?");
                if (!InputCheck.CheckUInt(out uInput) || uInput > getraenkeListe.Length)
                    break;

                if (getraenkePreise[uInput - 1] > t.pirat.GetTaler())
                {
                    Animation.RPGPrint("Du hast nicht genug Taler!");
                    Console.ReadLine();
                    continue;
                }
                if (uInput > 1 && t.pirat.GetBetrunkenheit() > 4)
                {
                    Animation.RPGPrint("Du bist zu betrunken, um noch mehr Alkohol zu trinken!");
                    Console.ReadLine();
                    continue;
                }
                t.pirat.SetTaler(t.pirat.GetTaler() - getraenkePreise[uInput - 1]);
                Animation.RPGPrint($"Du hebst einen Becher \"{getraenkeListe[uInput - 1]}\"!");
                if (uInput != 1) // Wasser macht nicht betrunken
                {
                    Animation.Drink(false);
                    t.pirat.SetBetrunkenheit(t.pirat.GetBetrunkenheit() + 1);
                    Animation.RPGPrint($"Dein Betrunkenheitslevel ist nun {t.pirat.GetBetrunkenheit()}!");
                }
                else
                    Animation.Drink();
                Animation.RPGPrint("Willst du weitertrinken? (j = ja)\n" + Output.back2mainMenue);
                uInput2 = Console.ReadKey().KeyChar;
            } while (uInput2 == 'j');
        }

    }
}
