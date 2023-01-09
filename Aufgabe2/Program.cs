﻿using MonkeyIsland1.Controllers;
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
        public static List<Pirat> piraten = new List<Pirat>(); //Liste aller lebenden Piraten
        const int anzahlInsel = 5;
        public static Meer meer = new Meer(anzahlInsel);
        public static Pirat currentPirat;
        public static Lokation currentLokation;
        public static Insel currentInsel;
        public static Random rnd = new Random();

        static void Main(string[] args)
        {
            uint uInput, uInput2; //Benutzereingaben
            Console.OutputEncoding = Encoding.UTF8; //notwendig für einige Animationen

            Animation.RPGPrint("Willkommen beim Piratenspiel! Yarr!");
            //Animation.SkullBones();
            try
            {
                piraten = FileHandler.LoadGame();
                currentPirat = piraten[0];
                Console.WriteLine("Spielstand wurde geladen.");
            }
            catch
            {
                Console.WriteLine("Fehler! Spielstand konnte nicht geladen werden.\nNeues Spiel wird gestartet.");
                currentPirat = PirateHandler.CreatePirate(); //Startpirat
            }
            Console.ReadLine();

            do //Hauptschleife, die das Spiel am Laufen hält
            {
                currentInsel = currentPirat.GetInsel();
                currentLokation = currentPirat.GetLokation();
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

                Output.ShowStats(currentPirat, meer, currentLokation);
                Animation.RPGPrint("Was möchtest Du als nächstes tun?");
                Output.ShowMenue(Output.mainMenueOptions, Array.IndexOf(currentInsel.standorte, currentLokation));

                if (!InputCheck.CheckUInt(out uInput) || !(currentLokation is Insel) && uInput > 5
                    || (currentLokation is Insel) && uInput > 4)
                {
                    FileHandler.SaveGame(piraten);
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

                        if (currentInsel.standorte[uInput2 - 1].GetType() == currentLokation.GetType())
                            Animation.RPGPrint("Du bist schon hier!");
                        else
                        {
                            currentLokation = currentInsel.standorte[uInput2 - 1];
                            currentPirat.SetLokation(currentLokation);
                            Animation.RPGPrint($"Du hast den Standort gewechselt zu: {currentLokation.GetBezeichnung()}");
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
                        foreach (Pirat p in currentInsel.GetBesucher())
                            Animation.RPGPrint(p.GetName());
                        break;

                    case 5:
                        currentPirat.GetLokation().Event(new Transporter() { meer = meer, insel = currentInsel, pirat = currentPirat });                        
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
            } while (true);
        }
    }
}
