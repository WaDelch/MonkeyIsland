using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Program
    {
        static List<Pirat> piraten = new List<Pirat>();
        static Meer meer = new Meer();
        static Pirat currentPirat;
        static Insel currentInsel;
        enum standort { Insel, Strand, Kneipe, Schiff, Friedhof };
        static string menue = "Sonstige Eingabe = zurück zum Hauptmenü"; //Satz in var ausgelagert, weil oft verwendet

        static Pirat CreatePirate(Insel insel)
        {
            string name;
            Pirat neuerPirat;
            while (true)
            {
                Animation.RPGPrint("Wie soll der Pirat heißen?");
                name = Console.ReadLine();
                if (!InputCheck.CheckAlphaNum(name))
                {
                    Console.WriteLine("Der Name darf nur Zahlen und Buchstaben enthalten!");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }
                neuerPirat = new Pirat(name, meer, insel);
                piraten.Add(neuerPirat);
                meer.GetInsel()[0].AddBesucher(neuerPirat);
                Animation.RPGPrint($"Der Pirat {neuerPirat.GetName()} wurde erstellt.");
                break;
            }
            return neuerPirat;
        }

        static void ChangePirate()
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
                currentPirat = CreatePirate(meer.GetInsel()[0]);
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
        }

        static void Main(string[] args)
        {
            uint uinput, uinput2, uinput3; //Benutzereingaben
            char uinput4 = 'j';
            Random rnd = new Random();
            int randomZahl;
            string[] getraenkeListe = { "Wasser", "Bier", "Grog", "Selbstgebraute Hausmarke" };
            //int[] getraenkePreise = { 2, 4, 6 };

            Console.OutputEncoding = Encoding.UTF8;
            currentInsel = meer.GetInsel()[0]; //Startinsel ist Insel 1
            Animation.RPGPrint("Willkommen beim Piratenspiel! Yarr!");
            Animation.SkullBones();
            currentPirat = CreatePirate(currentInsel); //Startpirat startet auf Insel 1
            Console.ReadLine();
            Kneipe currentKneipe;
            Strand currentStrand;
            Schiff currentSchiff;
            Friedhof currentFriedhof;
            standort currentStandort = standort.Insel;

            do
            {
                currentKneipe = currentInsel.GetKneipe();
                currentStrand = currentInsel.GetStrand();
                currentFriedhof = currentInsel.GetFriedhof();
                currentSchiff = currentInsel.GetSchiff();
                if (currentKneipe.GetBesucher().Contains(currentPirat))
                    currentStandort = standort.Kneipe;
                else if (currentStrand.GetBesucher().Contains(currentPirat))
                    currentStandort = standort.Strand;
                else if (currentFriedhof.GetBesucher().Contains(currentPirat))
                    currentStandort = standort.Friedhof;
                else if (currentSchiff.GetBesatzung().Contains(currentPirat))
                    currentStandort = standort.Schiff;
                else
                    currentStandort = standort.Insel;

                Console.Clear();
                Console.WriteLine("Du bist " + currentPirat.GetName() +
                    "\nauf der Insel: " + currentInsel.GetBezeichnung());
                if (currentStandort == standort.Kneipe)
                    Console.WriteLine("in der Kneipe: " + currentKneipe.GetBezeichnung());
                else if (currentStandort == standort.Strand)
                    Console.WriteLine("am Strand: " + currentStrand.GetBezeichnung());
                else if (currentStandort == standort.Schiff)
                    Console.WriteLine("auf dem Schiff: " + currentSchiff.GetBezeichnung());
                else if (currentStandort == standort.Friedhof)
                    Console.WriteLine("auf dem Friedhof: " + currentFriedhof.GetBezeichnung());
                Console.WriteLine("im Meer: " + meer.GetBezeichnung() +
                  "\nDein Betrunkenheitslevel: " + currentPirat.GetBetrunkenheit() +
                  "\nDeine Taler: " + currentPirat.GetTaler());

                Animation.RPGPrint("\nWas möchtest Du als nächstes tun?\n" +
                    "1) Insel erkunden\n2) Piraten erstellen\n3) Piraten wechseln" +
                    "\n4) Wer ist alles hier?");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (currentStandort == standort.Kneipe)
                    Animation.RPGPrint("5) Einen trinken!");
                else if (currentStandort == standort.Strand)
                    Animation.RPGPrint("5) Nach Schätzen graben!");
                else if (currentStandort == standort.Schiff)
                    Animation.RPGPrint("5) Auf eine andere Insel fahren.");
                else if (currentStandort == standort.Friedhof)
                    Animation.RPGPrint("5) Gräber besichtigen.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Animation.RPGPrint("Sonstige Eingabe = Programm beenden");

                if (!InputCheck.CheckUInt(out uinput) || currentStandort != standort.Insel && uinput > 5
                    || currentStandort == standort.Insel && uinput > 4)
                {
                    Animation.RPGPrint("Programm beendet.");
                    return;
                }
                switch (uinput)
                {
                    case 1:
                        Animation.RPGPrint("Wohin möchtest du gehen?\n" +
                            "1) In die Kneipe\n2) An den Strand\n3) Auf das Schiff\n" +
                            "4) Zum Friedhof\n" + menue);
                        if (!InputCheck.CheckUInt(out uinput2) || uinput2 > 4)
                            continue;
                        switch (uinput2)
                        {
                            case 1:
                                if (currentStandort == standort.Kneipe)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentKneipe.AddBesucher(currentPirat);
                                    currentStandort = standort.Kneipe;
                                    Animation.RPGPrint($"Du bist zur Kneipe \"{currentKneipe.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 2:
                                if (currentStandort == standort.Strand)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentStrand.AddBesucher(currentPirat);
                                    currentStandort = standort.Strand;
                                    Animation.RPGPrint($"Du bist zum Strand \"{currentStrand.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 3:
                                if (currentSchiff == null)
                                    Animation.RPGPrint("Das Schiff ist zur Zeit nicht da!");
                                else if (currentStandort == standort.Schiff)
                                    Animation.RPGPrint("Du bist schon auf dem Schiff!");
                                else
                                {
                                    currentSchiff.AddBesatzung(currentPirat);
                                    currentStandort = standort.Schiff;
                                    Animation.RPGPrint($"Du bist auf das Schiff \"{currentSchiff.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                            case 4:
                                if (currentStandort == standort.Friedhof)
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    currentFriedhof.AddBesucher(currentPirat);
                                    currentStandort = standort.Friedhof;
                                    Animation.RPGPrint($"Du bist zum Friedhof \"{currentFriedhof.GetBezeichnung()}\" gegangen.");
                                }
                                break;
                        }
                        break;

                    case 2:
                        CreatePirate(meer.GetInsel()[0]);
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
                            case standort.Kneipe:
                                do
                                {
                                    Console.Clear();
                                    if (currentPirat.GetTaler() < 2)
                                    {
                                        Animation.RPGPrint("Du hast nicht genug Taler für Getränke!\n" +
                                            "Schnorrer fliegen vor die Tür!");
                                        break;
                                    }
                                    Animation.RPGPrint("Hier ist die Getränkeliste: ");
                                    for (int i = 0; i < getraenkeListe.Length; i++)
                                        Animation.RPGPrint($"{i + 1}) {getraenkeListe[i]} - {(i + 1) * 2} Taler");

                                    Animation.RPGPrint(menue + "\nDu hast zur Zeit " + currentPirat.GetTaler() + " Taler.\nWas möchtest du bestellen?");
                                    if (!InputCheck.CheckUInt(out uinput2) || uinput2 > getraenkeListe.Length)
                                        break;
                                    if (uinput2 * 2 > currentPirat.GetTaler())
                                    {
                                        Animation.RPGPrint("Du hast nicht genug Taler!");
                                        Console.ReadLine();
                                        continue;
                                    }
                                    if (uinput2 > 1 && currentPirat.GetBetrunkenheit() > 4)
                                    {
                                        Animation.RPGPrint("Du bist zu betrunken, um noch mehr Alkohol zu trinken!");
                                        Console.ReadLine();
                                        continue;
                                    }
                                    currentPirat.SetTaler(currentPirat.GetTaler() - (int)(uinput2 * 2));
                                    Animation.RPGPrint($"Du hebst einen Becher \"{getraenkeListe[uinput2 - 1]}\"!");
                                    if (uinput2 != 1) // Wasser macht nicht betrunken
                                    {
                                        Animation.Drink(false);
                                        currentPirat.SetBetrunkenheit(currentPirat.GetBetrunkenheit() + 1);
                                        Animation.RPGPrint($"Dein Betrunkenheitslevel ist nun {currentPirat.GetBetrunkenheit()}!");
                                    }
                                    else
                                        Animation.Drink();
                                    Animation.RPGPrint("Willst du weitertrinken? (j = ja)\n" + menue);
                                    uinput4 = Console.ReadKey().KeyChar;
                                } while (uinput4 == 'j');
                                break;

                            case standort.Strand:

                                do
                                {
                                    Console.Clear();
                                    Animation.RPGPrint("Du gräbst nach Schätzen.");
                                    System.Threading.Thread.Sleep(200);
                                    Animation.DigSite();
                                    randomZahl = rnd.Next(1, 101);
                                    if (randomZahl < 5) // 4% Chance 10 Taler zu finden
                                    {
                                        Animation.RPGPrint("Du hast einen Sack mit zehn Talern gefunden!"); //WiP
                                        currentPirat.SetTaler(currentPirat.GetTaler() + 10);
                                    }
                                    else if (randomZahl < 31) // 30% Chance einen Taler zu finden
                                    {
                                        Animation.RPGPrint("Du hast einen Taler gefunden!");
                                        currentPirat.SetTaler(currentPirat.GetTaler() + 1);
                                    }
                                    else
                                        Animation.RPGPrint("Du hast nichts gefunden.");
                                    Animation.RPGPrint($"Du hast jetzt {currentPirat.GetTaler()} Taler.");
                                    Animation.RPGPrint("Willst du weitergraben? (j = ja)\n" + menue);
                                } while (Console.ReadKey().KeyChar == 'j');
                                break;

                            case standort.Friedhof:

                                Console.Clear();
                                Animation.RPGPrint("Du besuchst die Gräber auf dem Friedhof." +
                                    "\nFolgende Piraten liegen hier begraben:");
                                for (int i = 0; i < currentFriedhof.GetDauerbesucher().Count; i++)
                                    Animation.RPGPrint(currentFriedhof.GetDauerbesucher()[i].GetName());
                                break;

                            case standort.Schiff:

                                Animation.RPGPrint("Zu welcher Insel möchtest du fahren?");
                                for (int i = 0; i < meer.GetInsel().Length; i++) //length = 3
                                    Animation.RPGPrint($"{i + 1}) {meer.GetInsel()[i].GetBezeichnung()}");
                                Animation.RPGPrint(menue);
                                if (!InputCheck.CheckUInt(out uinput3) || uinput3 > meer.GetInsel().Length)
                                    continue;

                                if (uinput3 - 1 == Array.IndexOf(meer.GetInsel(), currentInsel))
                                    Animation.RPGPrint("Du bist schon hier!");
                                else
                                {
                                    randomZahl = rnd.Next(1, 101) + 5 * currentPirat.GetBetrunkenheit();
                                    if (randomZahl > 95)
                                    {
                                        //WiP, Es soll eine Chance bestehen, dass Piraten vom Schiff fallen
                                        //Je betrunkener der Pirat, desto höher die Chance
                                        //Piraten im Meer bekommen eine weitere Chance an Land zu schwimmen
                                        //Je betrunkender der Pirat, desto niedriger die Erfolgschance
                                        //Piraten sollen also ertrinken können
                                        //Tote Piraten werden automatisch auf dem Friedhof ihrer Heimatsinsel begraben
                                        //und sind unspielbar für den Rest des Spielverlaufs
                                        //Der Kapitän steuert das Schiff automatisch zum HeimatsPort zurück
                                    }
                                    Console.Clear();
                                    Console.SetCursorPosition(0, 6);
                                    Console.WriteLine(currentInsel.GetBezeichnung());
                                    currentInsel.DelBesucher(currentPirat); //Pirat nicht mehr auf der alten Insel
                                    currentInsel.GetSchiff().DelBesatzung(currentPirat); //Pirat nicht mehr auf dem Schiff
                                    currentInsel = meer.GetInsel()[uinput3 - 1];
                                    currentInsel.AddBesucher(currentPirat); //Pirat auf neuer Insel
                                    currentPirat.SetStandort(currentInsel); //Pirat hat neue Insel als Standort 
                                    currentStandort = standort.Insel;
                                    Console.SetCursorPosition(99 - currentInsel.GetBezeichnung().Length, 6);
                                    Console.WriteLine(currentInsel.GetBezeichnung());
                                    Animation.Ship();
                                    Console.SetCursorPosition(0, 0);
                                    Animation.RPGPrint($"Du bist zur Insel \"{currentInsel.GetBezeichnung()}\" gefahren.");
                                }
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
    }
}
