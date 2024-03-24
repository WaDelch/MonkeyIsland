using System;
using MonkeyIsland1.Controllers;
using MonkeyIsland1.Views;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Bar : Location
    {
        string[] drinksList = { "Wasser", "Bier", "Grog", "Selbstgebraute Hausmarke" };
        int[] drinksPrices = { 1, 3, 4, 6 };

        public override void Event(Transporter t)
        {
            while(true)
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetDescription()} ===~~~");
                if (t.pirate.GetCoins() < 1)
                {
                    Animation.RPGPrint("Du hast nicht genug Taler für Getränke!\n" +
                        "Schnorrer fliegen vor die Tür!");
                    break;
                }
                Animation.RPGPrint("Hier ist die Getränkeliste: ");
                Output.ShowShopList(drinksList, drinksPrices);
                Animation.RPGPrint("\nDu hast zur Zeit " + t.pirate.GetCoins() + " Taler.\nWas möchtest du bestellen?");
                if (!InputCheck.CheckUInt(out uInput) || uInput > drinksList.Length)
                    break;

                if (drinksPrices[uInput - 1] > t.pirate.GetCoins())
                {
                    Animation.RPGPrint("Du hast nicht genug Taler!");
                    Console.ReadLine();
                    continue;
                }
                if (uInput > 1 && t.pirate.GetDrunkenness() > 4)
                {
                    Animation.RPGPrint("Du bist zu betrunken, um noch mehr Alkohol zu trinken!");
                    Console.ReadLine();
                    continue;
                }
                t.pirate.SetCoins(t.pirate.GetCoins() - drinksPrices[uInput - 1]);
                Animation.RPGPrint($"Du hebst einen Becher \"{drinksList[uInput - 1]}\"!");
                if (uInput != 1) // Wasser macht nicht betrunken
                {
                    Animation.Drink(false);
                    t.pirate.SetDrunkenness(t.pirate.GetDrunkenness() + 1);
                    Animation.RPGPrint($"Dein Betrunkenheitslevel ist nun {t.pirate.GetDrunkenness()}!");
                }
                else
                    Animation.Drink();
                Animation.RPGPrint("Willst du weitertrinken? (j = ja)\n" + Output.back2mainMenue);
                if (!InputCheck.CheckString())
                    break;
            }
        }
    }
}
