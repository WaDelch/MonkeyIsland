using MonkeyIsland1.Controllers;
using MonkeyIsland1.Models.Locations;
using MonkeyIsland1.Views;
using System;

namespace MonkeyIsland1.Models
{
    internal static class PirateHandler
    {
        public static Pirate CreatePirate()
        {
            string name;
            Pirate neuerPirat;
            Isle startInsel = Program.sea.GetIsles()[Program.rnd.Next(0, Program.sea.GetIsles().Length)]; //zufällige Startinsel
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
            neuerPirat = new Pirate(name, Program.sea, startInsel);
            Program.pirates.Add(neuerPirat);
            startInsel.AddVisitor(neuerPirat);
            Animation.RPGPrint($"Der Pirat {neuerPirat.GetName()} wurde erstellt.");
            return neuerPirat;
        }

        public static void ChangePirate()
        {
            if (Program.pirates.Count < 1)
            {
                Animation.RPGPrint("Es gibt keine lebenden Piraten mehr!\n" +
                    "Willst du einen neuen Piraten anlegen? (j = ja)" +
                    "\nSonstige Eingabe = Programm beenden");
                if (Console.ReadKey().KeyChar != 'j')
                {
                    Animation.RPGPrint("Programm beendet.");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                Program.currentPirate = CreatePirate();
            }
            else
            {
                int uinput;
                Animation.RPGPrint("Zu welchem Piraten willst Du wechseln?");
                for (int i = 0; i < Program.pirates.Count; i++)
                    Animation.RPGPrint($"{i + 1}) {Program.pirates[i].GetName()}");
                Animation.RPGPrint(Output.back2mainMenue);
                if (!InputCheck.CheckInt(out uinput) || uinput > Program.pirates.Count)
                    return;
                if (Program.currentPirate == Program.pirates[Convert.ToInt32(uinput) - 1])
                {
                    Animation.RPGPrint("Das bist du schon!");
                    return;
                }
                Program.currentPirate = Program.pirates[Convert.ToInt32(uinput) - 1];
                //foreach (Insel i in Program.sea.GetInsel())
                //{
                //    if (i.GetBesucher().Contains(Program.currentPirate))
                //    {
                //        Program.currentInsel = i;
                //        break;
                //    }
                //}
            }
            Program.currentIsle = Program.currentPirate.GetIsle();
            Program.currentLocation = Program.currentPirate.GetLocation();
            Animation.RPGPrint($"Du bist jetzt \"{Program.currentPirate.GetName()}\".");
        }
    }
}
