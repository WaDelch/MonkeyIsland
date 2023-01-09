using MonkeyIsland1.Controllers;
using MonkeyIsland1.Models;
using MonkeyIsland1.Models.Locations;
using MonkeyIsland1.Views;
using System;
using System.Collections.Generic;
using System.Text;

/* ##################################
 * ### Textbased pirate game      ###
 * ### Author: WaDelch            ###
 * ################################## */

namespace MonkeyIsland1
{
    //public enum Standort { Insel = -1, Strand, Kneipe, Schiff, Friedhof, Huette }; //Bonusoption ab Index >= 0

    internal class Program
    {
        public static List<Pirate> pirates = new List<Pirate>(); //Liste aller lebenden Piraten
        const int numberOfIsles = 5;
        public static Sea sea = new Sea(numberOfIsles);
        public static Pirate currentPirate;
        public static Location currentLocation;
        public static Isle currentIsle;
        public static Random rnd = new Random();

        static void Main(string[] args)
        {
            uint uInput, uInput2; //Benutzereingaben
            Console.OutputEncoding = Encoding.UTF8; //notwendig für einige Animationen

            Animation.RPGPrint("Willkommen beim Piratenspiel! Yarr!");
            //Animation.SkullBones();
            try
            {
                pirates = FileHandler.LoadGame();
                currentPirate = pirates[0];
                Console.WriteLine("Spielstand wurde geladen.");
            }
            catch
            {
                Console.WriteLine("Fehler! Spielstand konnte nicht geladen werden.\nNeues Spiel wird gestartet.");
                currentPirate = PirateHandler.CreatePirate(); //Startpirat
            }
            Console.ReadLine();

            do //Hauptschleife, die das Spiel am Laufen hält
            {
                currentIsle = currentPirate.GetIsle();
                currentLocation = currentPirate.GetLocation();
                Console.Clear();

                //Console.WriteLine(currentInsel.GetBezeichnung());
                //foreach (Pirat p in currentInsel.GetBesucher())
                //    Console.WriteLine(p.GetName());

                //Console.WriteLine(currentInsel.GetLokation<Kneipe>().GetBezeichnung());
                //foreach (Pirat p in currentInsel.GetLokation<Kneipe>().GetBesucher())
                //    Console.WriteLine(p.GetName());

                //Console.WriteLine(currentInsel.GetLokation<Strand>().GetBezeichnung());
                //foreach (Pirat p in currentInsel.GetLokation<Strand>().GetBesucher())
                //    Console.WriteLine(p.GetName());

                //Console.WriteLine(currentInsel.GetLokation<Schiff>().GetBezeichnung());
                //foreach (Pirat p in currentInsel.GetLokation<Schiff>().GetBesucher())
                //    Console.WriteLine(p.GetName());

                //Console.WriteLine(currentInsel.GetLokation<Friedhof>().GetBezeichnung());
                //foreach (Pirat p in currentInsel.GetLokation<Friedhof>().GetBesucher())
                //    Console.WriteLine(p.GetName());

                //Console.WriteLine(currentInsel.GetLokation<Huette>().GetBezeichnung());
                //foreach (Pirat p in currentInsel.GetLokation<Huette>().GetBesucher())
                //    Console.WriteLine(p.GetName());

                Output.ShowStats(currentPirate, sea, currentLocation);
                Animation.RPGPrint("Was möchtest Du als nächstes tun?");
                Output.ShowMenue(Output.mainMenueOptions, Array.IndexOf(currentIsle.locations, currentLocation));

                if (!InputCheck.CheckUInt(out uInput) || !(currentLocation is Isle) && uInput > 5
                    || (currentLocation is Isle) && uInput > 4)
                {
                    FileHandler.SaveGame();
                    Animation.RPGPrint("Programm beendet. Der Spielstand wurde gespeichert.");
                    return;
                }
                switch (uInput)
                {
                    case 1:
                        Animation.RPGPrint("Wohin möchtest du gehen?");
                        Output.ShowMenue(Output.exploreMenueOptions);
                        if (!InputCheck.CheckUInt(out uInput2) || uInput2 > Output.exploreMenueOptions.Length)
                            continue;

                        if (currentIsle.locations[uInput2 - 1].GetType() == currentLocation.GetType())
                            Animation.RPGPrint("Du bist schon hier!");
                        else
                        {
                            currentLocation = currentIsle.locations[uInput2 - 1];
                            currentPirate.SetLocation(currentLocation);
                            Animation.RPGPrint($"Du hast den Standort gewechselt zu: {currentLocation.GetDescription()}");
                        }
                        break;

                    case 2:
                        PirateHandler.CreatePirate();
                        break;

                    case 3:
                        PirateHandler.ChangePirate();
                        break;

                    case 4:
                        Animation.RPGPrint("Folgende Piraten befinden sich auf dieser Insel:");
                        foreach (Pirate p in currentIsle.GetVisitors())
                            Animation.RPGPrint(p.GetName());
                        break;

                    case 5:
                        currentPirate.GetLocation().Event(new Transporter() { sea = sea, isle = currentIsle, pirate = currentPirate });                        
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
            } while (true);
        }
    }
}
