using MonkeyIsland1.Models;
using MonkeyIsland1.Models.Locations;
using System;

namespace MonkeyIsland1.Views
{
    internal static class Output
    {
        public static string[] mainMenueOptions = { "Insel erkunden", "Piraten erstellen", "Piraten wechseln", "Wer ist alles hier?" };
        public static string[] bonusOptions = { "Einen trinken!", "Nach Schätzen graben", "Auf eine andere Insel fahren", "Gräber besichtigen", "Zimmer für dich Nacht mieten", "Shoppen" };
        public static string[] exploreMenueOptions = { "In die Kneipe", "An den Strand", "Auf das Schiff", "Zum Friedhof", "Zur Hütte", "Zum Shop" };
        public static string back2mainMenue = "Sonstige Eingabe = zurück zum Hauptmenü";
        public static string closeAndSaveGame = "Programm beendet. Der Spielstand wurde gespeichert.";

        public static void ShowMenue(string[] options, int bonusIndex = -1)
        {
            int i;
            for (i = 0; i < options.Length; i++)
            {
                Animation.RPGPrint($"{i + 1}) {options[i]}");
            }

            if (bonusIndex >= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Animation.RPGPrint(++i + ") " + bonusOptions[bonusIndex]);
                Console.ForegroundColor = ConsoleColor.Gray;
                Animation.RPGPrint("Sonstige Eingabe = Programm beenden");
            }
            else
                Animation.RPGPrint(back2mainMenue);
        }

        public static void ShowShopList(string[] items, int[] prices)
        {
            for (int i = 0; i < items.Length; i++)
                Animation.RPGPrint($"{i + 1}) {items[i]} - {prices[i]} Taler");
            Console.WriteLine(back2mainMenue);
        }

        public static void ShowStats(Pirate p, Sea m, Location l)
        {
            string tLine = new String('-', 50 + p.GetName().Length);
            string[] statNames = { "Name", "Meer", "Standort", "Betrunkenheitslevel", "Taler" };
            string[] stats = { p.GetName(), m.GetDescription(), l.GetDescription(), p.GetDrunkenness().ToString() + "/5", p.GetCoins().ToString() };
            Console.WriteLine(tLine);
            for (int i = 0; i < stats.Length; i++)
            {
                Console.Write($"| {statNames[i]}");
                Console.CursorLeft += 21 - statNames[i].Length;
                Console.Write($": {stats[i]}");
                Console.CursorLeft = tLine.Length - 1;
                Console.WriteLine("|");
                Console.WriteLine(tLine);
            }
            Console.WriteLine();

            //Console.WriteLine($"Name: {p.GetName()}\nMeer: {m.GetBezeichnung()}\nStandort: {l.GetBezeichnung()}\n" +
            //                  $"Betrunkenheitslevel: {p.GetBetrunkenheit()}/5\nTaler: {p.GetTaler()}\n");
        }
    }
}
