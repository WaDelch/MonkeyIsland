using MonkeyIsland1.Controllers;
using MonkeyIsland1.Views;
using System;

namespace MonkeyIsland1.Models.Locations
{
    public enum pItem { Sword, EyePatch, Parrot, WoodenLeg }

    [Serializable]
    internal class Shop : Location
    {
        public override void Event(Transporter t)
        {
            string[] shopList = { "Schwert", "Augenklappe", "Papagei", "Holzbein" };
            int[] priceList = { 150, 15, 75, 100 };

            do
            {
                Console.Clear();
                Animation.RPGPrint($"~~~=== {this.GetDescription()} ===~~~");
                Animation.RPGPrint("Welche Gegenstände willst Du kaufen?");
                Output.ShowShopList(shopList, priceList);
                Console.ReadLine();
                break;
            } while (Console.ReadKey().KeyChar == 'j');
        }
    }
}
