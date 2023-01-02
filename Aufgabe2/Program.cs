using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


/* ##################################
 * ### Textbasiertes Piratenspiel ###
 * ### Author: WaDelch            ###
 * ################################## */

namespace MonkeyIsland1
{
    internal class Program
    {
        public static List<Pirat> piraten = new List<Pirat>(); //Liste aller lebenden Piraten
        static Meer meer = new Meer();
        static Pirat currentPirat;
        static Insel currentInsel;
        static Random rnd = new Random();
        public static Standort currentStandort;
        public enum Standort { Insel, Strand, Kneipe, Schiff, Friedhof, Huette };
        public static string menue = "Sonstige Eingabe = zurück zum Hauptmenü"; //Satz in var ausgelagert, weil oft verwendet

        static void Main(string[] args)
        {
            uint uInput, uInput2; //Benutzereingaben
            string standortBonusOption = string.Empty;
            Console.OutputEncoding = Encoding.UTF8; //notwendig für einige Animationen

            Animation.RPGPrint("Willkommen beim Piratenspiel! Yarr!");
            Animation.SkullBones();
            currentPirat = CreatePirate(); //Startpirat
            currentInsel = currentPirat.GetStandort(); //Startinsel
            Console.ReadLine();
            Kneipe currentKneipe;
            Strand currentStrand;
            Schiff currentSchiff;
            Friedhof currentFriedhof;
            Huette currentHuette;
            currentStandort = Standort.Insel;

            do //Hauptschleife, die das Spiel am Laufen hält
            {
                currentKneipe = currentInsel.GetKneipe();
                currentStrand = currentInsel.GetStrand();
                currentFriedhof = currentInsel.GetFriedhof();
                currentSchiff = currentInsel.GetSchiff();
                currentHuette = currentInsel.GetHuette();
                Console.Clear();
                Console.WriteLine("Du bist " + currentPirat.GetName() +
                    "\nauf der Insel: " + currentInsel.GetBezeichnung());
                if (currentKneipe.GetBesucher().Contains(currentPirat))
                {
                    currentStandort = Standort.Kneipe;
                    Console.WriteLine("in der Kneipe: " + currentKneipe.GetBezeichnung());
                    standortBonusOption = "Einen trinken!";
                }
                else if (currentStrand.GetBesucher().Contains(currentPirat))
                {
                    currentStandort = Standort.Strand;
                    Console.WriteLine("am Strand: " + currentStrand.GetBezeichnung());
                    standortBonusOption = "Nach Schätzen graben!";
                }
                else if (currentFriedhof.GetBesucher().Contains(currentPirat))
                {
                    currentStandort = Standort.Friedhof;
                    Console.WriteLine("auf dem Friedhof: " + currentFriedhof.GetBezeichnung());
                    standortBonusOption = "Gräber besichtigen.";
                }
                else if (currentSchiff.GetBesucher().Contains(currentPirat))
                {
                    currentStandort = Standort.Schiff;
                    Console.WriteLine("auf dem Schiff: " + currentSchiff.GetBezeichnung());
                    standortBonusOption = "Auf eine andere Insel fahren.";
                }
                else if (currentHuette.GetBesucher().Contains(currentPirat))
                {
                    currentStandort = Standort.Huette;
                    Console.WriteLine("in der Hütte: " + currentHuette.GetBezeichnung());
                    standortBonusOption = "Zimmer für die Nacht mieten";
                }
                else
                    currentStandort = Standort.Insel;

                Console.WriteLine("im Meer: " + meer.GetBezeichnung() +
                  "\nDein Betrunkenheitslevel: " + currentPirat.GetBetrunkenheit() + "/5" +
                  "\nDeine Taler: " + currentPirat.GetTaler());

                Animation.RPGPrint("\nWas möchtest Du als nächstes tun?\n" +
                    "1) Insel erkunden\n2) Piraten erstellen\n3) Piraten wechseln" +
                    "\n4) Wer ist alles hier?");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (currentStandort != Standort.Insel)
                    Animation.RPGPrint("5) " + standortBonusOption);
                Console.ForegroundColor = ConsoleColor.Gray;
                Animation.RPGPrint("Sonstige Eingabe = Programm beenden");

                if (!InputCheck.CheckUInt(out uInput) || currentStandort != Standort.Insel && uInput > 5
                    || currentStandort == Standort.Insel && uInput > 4)
                {
                    Animation.RPGPrint("Programm beendet.");
                    return;
                }
                switch (uInput)
                {
                    case 1:
                        Animation.RPGPrint("Wohin möchtest du gehen?\n" +
                            "1) In die Kneipe\n2) An den Strand\n3) Auf das Schiff\n" +
                            "4) Zum Friedhof\n5) Zur Hütte\n" + menue);
                        if (!InputCheck.CheckUInt(out uInput2) || uInput2 > 5)
                            continue;
                        switch (uInput2)
                        {
                            case 1:
                                if (currentStandort == Standort.Kneipe)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentKneipe.AddBesucher(currentPirat);
                                    currentStandort = Standort.Kneipe;
                                    Animation.RPGPrint($"Du bist zur Kneipe \"{currentKneipe.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 2:
                                if (currentStandort == Standort.Strand)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentStrand.AddBesucher(currentPirat);
                                    currentStandort = Standort.Strand;
                                    Animation.RPGPrint($"Du bist zum Strand \"{currentStrand.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 3:
                                if (currentSchiff == null) // WiP
                                    Animation.RPGPrint("Das Schiff ist zur Zeit nicht da!");
                                else if (currentStandort == Standort.Schiff)
                                    Animation.RPGPrint("Du bist schon auf dem Schiff!");
                                else
                                {
                                    currentSchiff.AddBesucher(currentPirat);
                                    currentStandort = Standort.Schiff;
                                    Animation.RPGPrint($"Du bist auf das Schiff \"{currentSchiff.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 4:
                                if (currentStandort == Standort.Friedhof)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentFriedhof.AddBesucher(currentPirat);
                                    currentStandort = Standort.Friedhof;
                                    Animation.RPGPrint($"Du bist zum Friedhof \"{currentFriedhof.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 5:
                                if (currentStandort == Standort.Huette)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentHuette.AddBesucher(currentPirat);
                                    currentStandort = Standort.Huette;
                                    Animation.RPGPrint($"Du bist zur Hütte \"{currentHuette.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                        }
                        break;

                    case 2:
                        CreatePirate();
                        break;

                    case 3:
                        ChangePirate();
                        continue;

                    case 4:
                        Animation.RPGPrint("Folgende Piraten befinden sich auf dieser Insel:");
                        foreach (Pirat p in currentInsel.GetBesucher())
                            Animation.RPGPrint(p.GetName());
                        break;

                    case 5:
                        switch (currentStandort)
                        {
                            case Standort.Kneipe:
                                currentInsel.GetKneipe().Event(ref currentPirat);
                                break;

                            case Standort.Strand:
                                currentInsel.GetStrand().Event(ref currentPirat);
                                break;

                            case Standort.Friedhof:
                                currentInsel.GetFriedhof().Event();
                                break;

                            case Standort.Schiff:
                                currentInsel.GetSchiff().Event(meer, ref currentInsel, ref currentPirat);
                                break;

                            case Standort.Huette:
                                currentInsel.GetHuette().Event(ref currentPirat);
                                break;

                            default:
                                continue;
                        }
                        break;
                    default:
                        continue;
                }
                Console.ReadLine();
            } while (true);
        }
        public static Pirat CreatePirate()
        {
            string name;
            Pirat neuerPirat;
            Insel startInsel = meer.GetInsel()[rnd.Next(0, meer.GetInsel().Length)]; //zufällige Startinsel
            while (true)
            {
                Animation.RPGPrint("\nWie soll der Pirat heißen?");
                name = Console.ReadLine();
                if (!InputCheck.CheckAlphaNum(name))
                {
                    Console.WriteLine("Der Name darf nur Zahlen und Buchstaben enthalten!");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                    break;
            }
            neuerPirat = new Pirat(name, meer, startInsel);
            piraten.Add(neuerPirat);
            startInsel.AddBesucher(neuerPirat);
            Animation.RPGPrint($"Der Pirat {neuerPirat.GetName()} wurde erstellt.");
            return neuerPirat;
        }

        public static void ChangePirate()
        {
            if (piraten.Count < 1)
            {
                Animation.RPGPrint("Es gibt keine lebenden Piraten mehr!\n" +
                    "Willst du einen neuen Piraten anlegen? (j = ja)" +
                    "\nSonstige Eingabe = Programm beenden");
                if (Console.ReadKey().KeyChar != 'j')
                {
                    Animation.RPGPrint("Programm beendet.");
                    Environment.Exit(0);
                }
                currentPirat = CreatePirate();
                currentInsel = currentPirat.GetStandort();
            }
            else
            {
                int uinput;
                Animation.RPGPrint("Zu welchem Piraten willst Du wechseln?");
                for (int i = 0; i < piraten.Count; i++)
                    Animation.RPGPrint($"{i + 1}) {piraten[i].GetName()}");
                Animation.RPGPrint(menue);
                if (!InputCheck.CheckInt(out uinput) || uinput > piraten.Count)
                    return;
                if (currentPirat == piraten[Convert.ToInt32(uinput) - 1])
                {
                    Animation.RPGPrint("Das bist du schon!");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    currentPirat = piraten[Convert.ToInt32(uinput) - 1];
                    currentInsel = currentPirat.GetStandort();
                }
            }
            Animation.RPGPrint($"Du bist jetzt \"{currentPirat.GetName()}\".");
            Console.ReadLine();
            SaveGame();
        }

        static void SaveGame()
        {
            using (FileStream fs = File.Open(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\misave.bin", FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, piraten);
            }
        }
    }
}
