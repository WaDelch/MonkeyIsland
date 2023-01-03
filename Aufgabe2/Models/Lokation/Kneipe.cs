using MonkeyIsland1.Controllers;
using System;

namespace MonkeyIsland1.Models.Lokation
{
    [Serializable]
    internal class Kneipe : Lokation
    {
        string[] getraenkeListe = { "Wasser", "Bier", "Grog", "Selbstgebraute Hausmarke" };
        int[] getraenkePreise = { 1, 3, 4, 6 };     

        public void Event(ref Pirat ePirat)
        {
            char uInput2 = 'j';
            do
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetBezeichnung()} ===~~~");
                if (ePirat.GetTaler() < 1)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler für Getränke!\n" +
                        "Schnorrer fliegen vor die Tür!");
                    break;
                }
                Animation.RPGPrint("Hier ist die Getränkeliste: ");
                for (int i = 0; i < getraenkeListe.Length; i++)
                    Animation.RPGPrint($"{i + 1}) {getraenkeListe[i]} - {getraenkePreise[i]} Taler");
                Animation.RPGPrint(Program.menue + "\nDu hast zur Zeit " + ePirat.GetTaler() + " Taler.\nWas möchtest du bestellen?");
                if (!InputCheck.CheckUInt(out uInput) || uInput > getraenkeListe.Length)
                    break;

                if (getraenkePreise[uInput - 1] > ePirat.GetTaler())
                {
                    Animation.RPGPrint("Du hast nicht genug Taler!");
                    Console.ReadLine();
                    continue;
                }
                if (uInput > 1 && ePirat.GetBetrunkenheit() > 4)
                {
                    Animation.RPGPrint("Du bist zu betrunken, um noch mehr Alkohol zu trinken!");
                    Console.ReadLine();
                    continue;
                }
                ePirat.SetTaler(ePirat.GetTaler() - getraenkePreise[uInput - 1]);
                Animation.RPGPrint($"Du hebst einen Becher \"{getraenkeListe[uInput - 1]}\"!");
                if (uInput != 1) // Wasser macht nicht betrunken
                {
                    Animation.Drink(false);
                    ePirat.SetBetrunkenheit(ePirat.GetBetrunkenheit() + 1);
                    Animation.RPGPrint($"Dein Betrunkenheitslevel ist nun {ePirat.GetBetrunkenheit()}!");
                }
                else
                    Animation.Drink();
                Animation.RPGPrint("Willst du weitertrinken? (j = ja)\n" + Program.menue);
                uInput2 = Console.ReadKey().KeyChar;
            } while (uInput2 == 'j');
        }

    }
}
