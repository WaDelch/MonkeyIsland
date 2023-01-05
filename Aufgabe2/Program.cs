using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MonkeyIsland1.Models;
using MonkeyIsland1.Models.Lokations;
using MonkeyIsland1.Controllers;
using MonkeyIsland1.Views;
using System.Linq;

/* ##################################
 * ### Textbasiertes Piratenspiel ###
 * ### Author: WaDelch            ###
 * ################################## */

namespace MonkeyIsland1
{
    public enum Standort { Insel = -1, Strand, Kneipe, Schiff, Friedhof, Huette }; //Bonusoption ab Index >= 0

    internal class Program
    {
        public static List<Pirat> piraten = new List<Pirat>(); //Liste aller lebenden Piraten
        const int anzahlInsel = 5;
        static Meer meer = new Meer(anzahlInsel);
        static Pirat currentPirat;
        static Lokation currentLokation;
        static Insel currentInsel;
        static Random rnd = new Random();
        public static Standort currentStandort;
        static string savePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\save.bin";

        static void Main(string[] args)
        {
            uint uInput, uInput2; //Benutzereingaben
            Console.OutputEncoding = Encoding.UTF8; //notwendig für einige Animationen

            Animation.RPGPrint("Willkommen beim Piratenspiel! Yarr!");
            //Animation.SkullBones();
            //try
            //{
            //    LoadGame();
            //    currentPirat = piraten[0];
            //    Console.WriteLine("Spielstand wurde geladen.");
            //}
            //catch
            //{
            //    Console.WriteLine("Fehler! Spielstand konnte nicht geladen werden.\nNeues Spiel wird gestartet.");
            //    currentPirat = CreatePirate(); //Startpirat
            //}
            currentPirat = CreatePirate();
            Console.ReadLine();
            //Kneipe currentKneipe;
            //Strand currentStrand;
            //Schiff currentSchiff;
            //Friedhof currentFriedhof;
            //Huette currentHuette;
            //currentLokation = currentPirat.GetHeimat(); //Startinsel
            currentInsel = currentPirat.GetHeimat();
            //currentStandort = Standort.Insel;

            do //Hauptschleife, die das Spiel am Laufen hält
            {
                //currentKneipe = currentInsel.GetKneipe();
                //currentStrand = currentInsel.GetStrand();
                //currentFriedhof = currentInsel.GetFriedhof();
                //currentSchiff = currentInsel.GetSchiff();
                //currentHuette = currentInsel.GetHuette();
                //currentStandort = currentPirat.GetStandort();
                currentLokation = currentPirat.GetLokation();
                Console.Clear();
                Console.WriteLine(currentInsel.GetBezeichnung());
                foreach(Pirat p in currentInsel.GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetKneipe().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetKneipe().GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetStrand().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetStrand().GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetSchiff().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetSchiff().GetBesucher())
                    Console.WriteLine(p.GetName());

                Output.ShowStats(currentPirat, meer, currentLokation);
                Animation.RPGPrint("Was möchtest Du als nächstes tun?");
                Output.ShowMenue(Output.mainMenueOptions);//, (int)currentStandort);

                if (!InputCheck.CheckUInt(out uInput) || !(currentLokation is Insel) && uInput > 5
                    || (currentLokation is Insel) && uInput > 4)
                {
                    SaveGame();
                    Animation.RPGPrint("Programm beendet. Der Spielstand wurde gespeichert.");
                    return;
                }
                switch (uInput)
                {
                    case 1:
                        Animation.RPGPrint("Wohin möchtest du gehen?");
                        Output.ShowMenue(Output.exploreMenueOptions);
                        if (!InputCheck.CheckUInt(out uInput2) || uInput2 > 5)
                            continue;
                        if (!(currentLokation is Insel)) //Pirat wechselt Standort, aber bleibt auf der Insel
                            currentLokation.DelBesucher(currentPirat);
                        switch (uInput2)
                        {
                            case 1:
                                if (currentLokation is Kneipe)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentLokation = currentInsel.GetKneipe();
                                    //currentStandort = Standort.Kneipe;
                                    Animation.RPGPrint($"Du bist zur Kneipe \"{currentLokation.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 2:
                                if (currentLokation is Strand)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentLokation = currentInsel.GetStrand();
                                    //currentStandort = Standort.Strand;
                                    Animation.RPGPrint($"Du bist zum Strand \"{currentLokation.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 3:
                                if (currentInsel.GetSchiff() == null) // WiP
                                    Animation.RPGPrint("Das Schiff ist zur Zeit nicht da!");
                                else if (currentLokation is Schiff)
                                    Animation.RPGPrint("Du bist schon auf dem Schiff!");
                                else
                                {
                                    currentLokation = currentInsel.GetSchiff();
                                    //currentStandort = Standort.Schiff;
                                    Animation.RPGPrint($"Du bist auf das Schiff \"{currentInsel.GetSchiff().GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 4:
                                if (currentLokation is Friedhof)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentLokation = currentInsel.GetFriedhof();
                                    //currentStandort = Standort.Friedhof;
                                    Animation.RPGPrint($"Du bist zum Friedhof \"{currentInsel.GetFriedhof().GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 5:
                                if (currentLokation is Huette)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {;
                                    currentLokation = currentInsel.GetHuette();
                                    //currentStandort = Standort.Huette;
                                    Animation.RPGPrint($"Du bist zur Hütte \"{currentInsel.GetHuette().GetBezeichnung()}\" gegangen.");
                                }
                                break;
                        }

                        currentLokation.AddBesucher(currentPirat);
                        currentPirat.SetLokation(currentLokation);
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
                        if (currentLokation is Kneipe)
                            currentInsel.GetKneipe().Event(ref currentPirat);
                        else if (currentLokation is Strand)
                            currentInsel.GetStrand().Event(ref currentPirat);
                        else if (currentLokation is Friedhof)
                            currentInsel.GetFriedhof().Event();
                        else if (currentLokation is Schiff)
                            currentInsel.GetSchiff().Event(meer, ref currentInsel, ref currentPirat);
                        else if (currentLokation is Huette)
                            currentInsel.GetHuette().Event(ref currentPirat);
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
            neuerPirat = new Pirat(name, meer, startInsel, startInsel);
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
                currentInsel = currentPirat.GetHeimat();
                currentLokation = currentPirat.GetHeimat();
            }
            else
            {
                int uinput;
                Animation.RPGPrint("Zu welchem Piraten willst Du wechseln?");
                for (int i = 0; i < piraten.Count; i++)
                    Animation.RPGPrint($"{i + 1}) {piraten[i].GetName()}");
                Animation.RPGPrint(Output.back2mainMenue);
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
                    currentLokation = currentPirat.GetLokation();
                    for (int i = 0; i < meer.GetInsel().Length; i++)
                    {
                        if (meer.GetInsel()[i].GetBesucher().Contains(currentPirat))
                        {
                            currentInsel = meer.GetInsel()[i];
                            break;
                        }
                    }
                    //currentInsel = (Insel)currentPirat.GetLokation();
                }
            }
            Animation.RPGPrint($"Du bist jetzt \"{currentPirat.GetName()}\".");
            Console.ReadLine();
        }

        static void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(savePath, FileMode.Create))
            {
                bf.Serialize(fs, piraten);
            }
        }

        static void LoadGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(savePath, FileMode.Open))
            {
                piraten = (List<Pirat>)bf.Deserialize(fs);
            }
        }
    }
}
