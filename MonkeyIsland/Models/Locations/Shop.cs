using MonkeyIsland1.Controllers;
using MonkeyIsland1.Views;
using System;

namespace MonkeyIsland1.Models.Locations
{
        public enum pItem { Sword, EyePatch, Parrot, WoodenLeg }

    [Serializable]
    internal class Shop : Location
    {
        public static string[] shopList = { "Schwert", "Augenklappe", "Papagei", "Holzbein" };
        public static int nItems = shopList.Length;
        static int[] priceList = { 150, 15, 75, 100 };

        public override void Event(Transporter t)
        {
            while(true)
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetDescription()} ===~~~");
                Animation.RPGPrint("Welche Gegenstände willst Du kaufen?");
                Output.ShowShopList(shopList, priceList);
                Animation.RPGPrint($"Deine Taler: {t.pirate.GetCoins()}");
                if (!InputCheck.CheckUInt(out uInput) || uInput > shopList.Length)
                    return;
                uInput--;
                if (t.pirate.GetInventory()[uInput])
                {
                    Animation.RPGPrint("Du hast diesen Gegenstand bereits.");
                    continue;
                }
                else if (t.pirate.GetCoins() < priceList[uInput])
                {
                    Animation.RPGPrint("Du hast nicht genug Taler, um diesen Gegenstand zu kaufen.");
                    Console.ReadLine();
                    continue;
                }
                else
                {
                    t.pirate.SetCoins(t.pirate.GetCoins() - priceList[uInput]);
                    t.pirate.AddItem((int)uInput);
                    Animation.RPGPrint($"{shopList[uInput]} wurde deinem Inventar hinzugefügt.");
                    Animation.RPGPrint("Willst du weitershoppen? (j = ja)\n" + Output.back2mainMenue);
                    if (!InputCheck.CheckString())
                        break;
                }
            }
        }
    }
}
