using MonkeyIsland1.Controllers;
using MonkeyIsland1.Models.Locations;
using MonkeyIsland1.Views;
using System;

namespace MonkeyIsland1.Models
{
    internal static class PirateHandler
    {
        public static Pirat CreatePirate()
        {
            string name;
            Pirat neuerPirat;
            Insel startInsel = Program.meer.GetInsel()[Program.rnd.Next(0, Program.meer.GetInsel().Length)]; //zufällige Startinsel
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
            neuerPirat = new Pirat(name, Program.meer, startInsel);
            Program.piraten.Add(neuerPirat);
            startInsel.AddBesucher(neuerPirat);
            Animation.RPGPrint($"Der Pirat {neuerPirat.GetName()} wurde erstellt.");
            return neuerPirat;
        }

        public static void ChangePirate()
        {
            if (Program.piraten.Count < 1)
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
                Program.currentPirat = CreatePirate();
            }
            else
            {
                int uinput;
                Animation.RPGPrint("Zu welchem Piraten willst Du wechseln?");
                for (int i = 0; i < Program.piraten.Count; i++)
                    Animation.RPGPrint($"{i + 1}) {Program.piraten[i].GetName()}");
                Animation.RPGPrint(Output.back2mainMenue);
                if (!InputCheck.CheckInt(out uinput) || uinput > Program.piraten.Count)
                    return;
                if (Program.currentPirat == Program.piraten[Convert.ToInt32(uinput) - 1])
                {
                    Animation.RPGPrint("Das bist du schon!");
                    return;
                }
                Program.currentPirat = Program.piraten[Convert.ToInt32(uinput) - 1];
                //foreach (Insel i in Program.meer.GetInsel())
                //{
                //    if (i.GetBesucher().Contains(Program.currentPirat))
                //    {
                //        Program.currentInsel = i;
                //        break;
                //    }
                //}
            }
            Program.currentInsel = Program.currentPirat.GetInsel();
            Program.currentLokation = Program.currentPirat.GetLokation();
            Animation.RPGPrint($"Du bist jetzt \"{Program.currentPirat.GetName()}\".");
        }
    }
}
