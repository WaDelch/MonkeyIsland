using MonkeyIsland1.Controllers;
using MonkeyIsland1.Models;
using MonkeyIsland1.Models.Lokations;
using MonkeyIsland1.Views;
using System;
using System.Collections.Generic;
using System.Text;

/* ##################################
 * ### Textbasiertes Piratenspiel ###
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
            currentPirat = PirateHandler.CreatePirate();
            Console.ReadLine();
            //Kneipe currentKneipe;
            //Strand currentStrand;
            //Schiff currentSchiff;
            //Friedhof currentFriedhof;
            //Huette currentHuette;

            //currentStandort = Standort.Insel;

            do //Hauptschleife, die das Spiel am Laufen hält
            {
                //currentKneipe = currentInsel.GetKneipe();
                //currentStrand = currentInsel.GetStrand();
                //currentFriedhof = currentInsel.GetFriedhof();
                //currentSchiff = currentInsel.GetSchiff();
                //currentHuette = currentInsel.GetHuette();
                //currentStandort = currentPirat.GetStandort();
                currentInsel = currentPirat.GetInsel();
                currentLokation = currentPirat.GetLokation();
                Console.Clear();

                Console.WriteLine(currentInsel.GetBezeichnung());
                foreach (Pirat p in currentInsel.GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetLokation<Kneipe>().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetLokation<Kneipe>().GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetLokation<Strand>().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetLokation<Strand>().GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetLokation<Schiff>().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetLokation<Schiff>().GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetLokation<Friedhof>().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetLokation<Friedhof>().GetBesucher())
                    Console.WriteLine(p.GetName());

                Console.WriteLine(currentInsel.GetLokation<Huette>().GetBezeichnung());
                foreach (Pirat p in currentInsel.GetLokation<Huette>().GetBesucher())
                    Console.WriteLine(p.GetName());

                Output.ShowStats(currentPirat, meer, currentLokation);
                Animation.RPGPrint("Was möchtest Du als nächstes tun?");
                Output.ShowMenue(Output.mainMenueOptions, Array.IndexOf(currentInsel.standorte, currentLokation));//, (int)currentStandort);

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
                        //if (!(currentLokation is Insel)) //Pirat wechselt Standort, aber bleibt auf der Insel
                        //    currentLokation.DelBesucher(currentPirat);

                        if (currentInsel.standorte[uInput2 - 1].GetType() == currentLokation.GetType())
                            Animation.RPGPrint("Du bist schon hier!");
                        else
                        {
                            currentLokation = currentInsel.standorte[uInput2 - 1];
                            currentPirat.SetLokation(currentLokation);
                            Animation.RPGPrint($"Du hast den Standort gewechselt zu: {currentLokation.GetBezeichnung()}");
                        }

                        //switch (uInput2)
                        //{
                        //    case 1:
                        //        if (currentLokation is Kneipe)
                        //            Animation.RPGPrint("Du bist schon hier!");
                        //        else
                        //        {
                        //            currentLokation = currentInsel.GetLokation<Kneipe>();
                        //            //currentStandort = Standort.Kneipe;
                        //            Animation.RPGPrint($"Du bist zur Kneipe \"{currentLokation.GetBezeichnung()}\" gegangen.");
                        //        }
                        //        break;
                        //    case 2:
                        //        if (currentLokation is Strand)
                        //            Animation.RPGPrint("Du bist schon hier!");
                        //        else
                        //        {
                        //            currentLokation = currentInsel.GetLokation<Strand>();
                        //            //currentStandort = Standort.Strand;
                        //            Animation.RPGPrint($"Du bist zum Strand \"{currentLokation.GetBezeichnung()}\" gegangen.");
                        //        }
                        //        break;
                        //    case 3:
                        //        if (currentInsel.GetLokation<Schiff>() == null) // WiP
                        //            Animation.RPGPrint("Das Schiff ist zur Zeit nicht da!");
                        //        else if (currentLokation is Schiff)
                        //            Animation.RPGPrint("Du bist schon auf dem Schiff!");
                        //        else
                        //        {
                        //            currentLokation = currentInsel.GetLokation<Schiff>();
                        //            //currentStandort = Standort.Schiff;
                        //            Animation.RPGPrint($"Du bist auf das Schiff \"{currentLokation.GetBezeichnung()}\" gegangen.");
                        //        }
                        //        break;
                        //    case 4:
                        //        if (currentLokation is Friedhof)
                        //            Animation.RPGPrint("Du bist schon hier!");
                        //        else
                        //        {
                        //            currentLokation = currentInsel.GetLokation<Friedhof>();
                        //            //currentStandort = Standort.Friedhof;
                        //            Animation.RPGPrint($"Du bist zum Friedhof \"{currentLokation.GetBezeichnung()}\" gegangen.");
                        //        }
                        //        break;
                        //    case 5:
                        //        if (currentLokation is Huette)
                        //            Animation.RPGPrint("Du bist schon hier!");
                        //        else
                        //        {;
                        //            currentLokation = currentInsel.GetLokation<Huette>();
                        //            //currentStandort = Standort.Huette;
                        //            Animation.RPGPrint($"Du bist zur Hütte \"{currentLokation.GetBezeichnung()}\" gegangen.");
                        //        }
                        //        break;
                        //}

                        //currentLokation.AddBesucher(currentPirat);

                        break;

                    case 2:
                        PirateHandler.CreatePirate();
                        break;

                    case 3:
                        PirateHandler.ChangePirate();
                        continue;

                    case 4:
                        Animation.RPGPrint("Folgende Piraten befinden sich auf dieser Insel:");
                        foreach (Pirat p in currentInsel.GetBesucher())
                            Animation.RPGPrint(p.GetName());
                        break;

                    case 5:
                        if (currentLokation is Kneipe)
                            currentInsel.GetLokation<Kneipe>().Event(ref currentPirat);
                        else if (currentLokation is Strand)
                            currentInsel.GetLokation<Strand>().Event(ref currentPirat);
                        else if (currentLokation is Friedhof)
                            currentInsel.GetLokation<Friedhof>().Event();
                        else if (currentLokation is Schiff)
                            currentInsel.GetLokation<Schiff>().Event(meer, ref currentInsel, ref currentPirat);
                        else if (currentLokation is Huette)
                            currentInsel.GetLokation<Huette>().Event(ref currentPirat);
                        break;
                    default:
                        continue;
                }
                Console.ReadLine();
            } while (true);
        }
    }
}
