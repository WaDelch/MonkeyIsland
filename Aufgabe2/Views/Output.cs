using System;
using System.Linq;
using static MonkeyIsland1.Program;
using MonkeyIsland1.Models;
using MonkeyIsland1.Models.Lokations;

namespace MonkeyIsland1.Views
{
    internal static class Output
    {
        public static string[] mainMenueOptions = { "Insel erkunden", "Piraten erstellen", "Piraten wechseln", "Wer ist alles hier?" };
        public static string[] bonusOptions = { "Einen trinken!", "Nach Schätzen graben", "Auf eine andere Insel fahren", "Gräber besichtigen", "Zimmer für dich Nacht mieten" };
        public static string[] exploreMenueOptions = { "In die Kneipe", "An den Strand", "Auf das Schiff", "Zum Friedhof", "Zur Hütte" };
        public static string back2mainMenue = "Sonstige Eingabe = zurück zum Hauptmenü";

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

        public static void ShowStats(Pirat p, Meer m, Lokation l)
        {
            Console.WriteLine($"Name: {p.GetName()}\nMeer: {m.GetBezeichnung()}\nStandort: {l.GetBezeichnung()}\n" +
                              $"Betrunkenheitslevel: {p.GetBetrunkenheit()}\nTaler: {p.GetTaler()}");
        }
    }
}
