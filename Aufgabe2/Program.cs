using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//gitfront url: https://gitfront.io/r/WaDelch/3EyEG5R5CtJw/MonkeyIsland/

namespace MonkeyIsland1
{
    internal class Program
    {
        static List<Pirat> piraten = new List<Pirat>(); //Liste aller lebenden Piraten
        static Meer meer = new Meer();
        static Pirat currentPirat;
        static Insel currentInsel;
        enum Standort { Insel, Strand, Kneipe, Schiff, Friedhof, Huette };
        static string menue = "Sonstige Eingabe = zurück zum Hauptmenü"; //Satz in var ausgelagert, weil oft verwendet

        static Pirat CreatePirate()
        {
            string name;
            Pirat neuerPirat;
            Random rnd = new Random();
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
                    continue;
                }
                neuerPirat = new Pirat(name, meer, startInsel);
                piraten.Add(neuerPirat);
                startInsel.AddBesucher(neuerPirat);
                Animation.RPGPrint($"Der Pirat {neuerPirat.GetName()} wurde erstellt.");
                break;
            }
            return neuerPirat;
        }

        static void ChangePirate()
        {
            if (piraten.Count < 1) //WiP
            {
                Random rnd = new Random();
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
        }

        static void Main(string[] args)
        {
            uint uinput, uinput2, uinput3, uinput4; //Benutzereingaben
            char uinput5 = 'j';
            Random rnd = new Random();
            int randomZahl;
            string[] getraenkeListe = { "Wasser", "Bier", "Grog", "Selbstgebraute Hausmarke" };
            //int[] getraenkePreise = { 2, 4, 6 };
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
            Standort currentStandort = Standort.Insel;

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
                  "\nDein Betrunkenheitslevel: " + currentPirat.GetBetrunkenheit() +
                  "\nDeine Taler: " + currentPirat.GetTaler());

                Animation.RPGPrint("\nWas möchtest Du als nächstes tun?\n" +
                    "1) Insel erkunden\n2) Piraten erstellen\n3) Piraten wechseln" +
                    "\n4) Wer ist alles hier?");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                if (currentStandort != Standort.Insel)
                    Animation.RPGPrint("5) " + standortBonusOption);
                Console.ForegroundColor = ConsoleColor.Gray;
                Animation.RPGPrint("Sonstige Eingabe = Programm beenden");

                if (!InputCheck.CheckUInt(out uinput) || currentStandort != Standort.Insel && uinput > 5
                    || currentStandort == Standort.Insel && uinput > 4)
                {
                    Animation.RPGPrint("Programm beendet.");
                    return;
                }
                switch (uinput)
                {
                    case 1:
                        Animation.RPGPrint("Wohin möchtest du gehen?\n" +
                            "1) In die Kneipe\n2) An den Strand\n3) Auf das Schiff\n" +
                            "4) Zum Friedhof\n5) Zur Hütte\n" + menue);
                        if (!InputCheck.CheckUInt(out uinput2) || uinput2 > 5)
                            continue;
                        switch (uinput2)
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
                                do
                                {
                                    Console.Clear();
                                    Animation.RPGPrint($"~~~=== {currentKneipe.GetBezeichnung()} ===~~~");
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
                                    uinput5 = Console.ReadKey().KeyChar;
                                } while (uinput5 == 'j');
                                break;

                            case Standort.Strand:

                                do
                                {
                                    Console.Clear();
                                    Animation.RPGPrint($"~~~=== {currentStrand.GetBezeichnung()} ===~~~");
                                    Animation.RPGPrint("Du gräbst nach Schätzen.");
                                    System.Threading.Thread.Sleep(200);
                                    Animation.DigSite();
                                    randomZahl = rnd.Next(1, 101);
                                    if (randomZahl < 16) // 15% Chance 10 Taler zu finden
                                    {
                                        Animation.RPGPrint("Du hast einen Sack mit zehn Talern gefunden!");
                                        currentPirat.SetTaler(currentPirat.GetTaler() + 10);
                                    }
                                    else if (randomZahl < 81) // 65% Chance einen Taler zu finden
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

                            case Standort.Friedhof:

                                Console.Clear();
                                Animation.RPGPrint($"~~~=== {currentFriedhof.GetBezeichnung()} ===~~~");
                                Animation.RPGPrint($"Du besuchst die Gräber auf dem Friedhof." +
                                    "\nFolgende Piraten liegen hier begraben:");
                                for (int i = 0; i < currentFriedhof.GetDauerbesucher().Count; i++)
                                    Animation.RPGPrint(currentFriedhof.GetDauerbesucher()[i].GetName());
                                break;

                            case Standort.Schiff:

                                Animation.RPGPrint("Zu welcher Insel möchtest du fahren?");
                                for (int i = 0; i < meer.GetInsel().Length; i++) //length = 3
                                    Animation.RPGPrint($"{i + 1}) {meer.GetInsel()[i].GetBezeichnung()}");
                                Animation.RPGPrint(menue);
                                if (!InputCheck.CheckUInt(out uinput3) || uinput3 > meer.GetInsel().Length)
                                    continue;

                                if (uinput3 - 1 == Array.IndexOf(meer.GetInsel(), currentInsel))
                                {
                                    Animation.RPGPrint("Du bist schon hier!");
                                    continue;
                                }

                                //Schiff fährt los
                                Console.Clear();
                                currentInsel.DelBesucher(currentPirat); //Pirat nicht mehr auf der alten Insel
                                currentSchiff.DelBesucher(currentPirat); //Pirat nicht mehr auf dem Schiff (Resultat aller derzeit implementierten, folgenden Aktionen)
                                Console.SetCursorPosition(0, 6);
                                Console.WriteLine(currentInsel.GetBezeichnung()); //Startinsel anzeigen
                                Console.SetCursorPosition(99 - currentInsel.GetBezeichnung().Length, 6);
                                currentInsel = meer.GetInsel()[uinput3 - 1]; //wechsle Insel
                                Console.WriteLine(currentInsel.GetBezeichnung()); //Zielinsel anzeigen

                                randomZahl = rnd.Next(1, 101) + 5 * currentPirat.GetBetrunkenheit(); //Chance vom Schiff zu fallen
                                if (randomZahl > 95) //Pirat vom Schiff gefallen
                                {
                                    Animation.Ship(true); //Schiffsanimation bricht ab, weil der Pirat vom Schiff gefallen ist
                                    int drownChance = 0; //Chance zu ertrinken, steigt mit jeder folgenden Aktion/Entscheidung
                                    Console.Clear();
                                    Animation.RPGPrint("Torkelnd bewegst du dich während der Überfahrt entlang der Reling.\n" +
                                        "Plötzlich wird das Schiff von einer großen Welle erwischt und\n" +
                                        "du fällst über Bord ins eiskalte Wasser! Nach einem kurzen Moment der Realisierung und Orientierung,\n" +
                                        "schwimmst du panisch an die Oberfläche und\n" +
                                        "musst zusehen, wie das Schiff ohne dich weiterfährt!\n");
                                    Console.ReadLine();
                                    do //Entscheidungsmöglichkeiten im Wasser; können wiederholt werden
                                    {
                                        Console.Clear();
                                        //WiP, evtl. Schwimmanimation/-bild ausgeben
                                        Animation.RPGPrint("Was willst du jetzt tun?\n1) Um Hilfe schreien!\n" +
                                            "2) Versuchen zum Schiff zu schwimmen\n" +
                                            "3) Versuchen dich an Land zu retten\n" +
                                            "Sonstige Eingabe = auf Hilfe warten");
                                        if (!InputCheck.CheckUInt(out uinput4) || uinput4 > 3)
                                        {
                                            Animation.RPGPrint("Du treibst im Wasser und wartest auf Hilfe, aber vergeblich...\n" +
                                                "Deine Körpertemperatur und Überlebenschance sinkt!");
                                            Console.ReadLine();
                                            drownChance += 5;
                                            continue;
                                        }
                                        switch (uinput4)
                                        {
                                            case 1:
                                                Animation.RPGPrint("Du schreist so laut du kannst um Hilfe,\n" +
                                                    "aber niemand kann dich hören...\nDeine Körpertemperatur und Überlebenschance sinkt!");
                                                Console.ReadLine();
                                                drownChance += 5;
                                                break;
                                            case 2:
                                                Animation.RPGPrint("Du ruderst so schnell du kannst mit Armen und Füßen\n" +
                                                    "und versuchst das Schiff einzuholen, aber vergeblich...\n" +
                                                    "Deine Körpertemperatur und Überlebenschance sinkt!");
                                                Console.ReadLine();
                                                drownChance += 5;
                                                break;
                                            default:
                                                break;
                                        }
                                        if (uinput4 == 3) //Versuch sich an Land zu retten ist die letzte Entscheidung
                                        {
                                            Animation.RPGPrint("Du glaubst Land am horizont erblicken zu können\n" +
                                                "und entscheidest dich in diese Richtung zu schwimmen.\n" +
                                                "Du kämpfst mit aller Kraft gegen Kälte und Erschöpfung an,\n" +
                                                "doch irgendwann gibt dein Körper auf und du siehst nur noch schwarz...");
                                            Console.ReadLine();
                                            drownChance += 35; //Mindestchance zu ertrinken
                                            break;
                                        }
                                        if (drownChance > 99) //zu viele falsche Entscheidungen = ertrunken
                                            break;
                                    } while (true);
                                    if (drownChance + 5 * currentPirat.GetBetrunkenheit() > rnd.Next(1, 101)) //prüfen, ob ertrunken
                                    {
                                        Animation.RPGPrint("Das letzte was du spürst ist eine allumfassende Kälte,\n" +
                                            "die dich langsam verschlingt...");
                                        piraten.Remove(currentPirat); //Pirat nicht mehr in der Liste der lebenden Piraten

                                        currentPirat.GetHeimat().GetFriedhof().AddDauerbesucher(currentPirat); //Pirat auf dem Friedhof seiner Heimatsinsel beerdigt
                                        Console.ReadLine();
                                        Console.Clear();
                                        Animation.RPGPrint("Du bist gestorben!");
                                        Animation.SkullBones();
                                        Animation.RPGPrint("Taste drücken...");
                                        Console.ReadLine();
                                        Console.Clear();
                                        Animation.RPGPrint("Was willst du jetzt tun?\n1) Piraten wechseln\n2) Piraten erstellen und wechseln\n" +
                                            "Sonstige Eingabe = Programm beenden");
                                        if (!InputCheck.CheckUInt(out uinput4) || uinput4 > 2)
                                        {
                                            Animation.RPGPrint("Programm beendet.");
                                            return;
                                        }
                                        if (uinput4 == 1)
                                        {
                                            ChangePirate();
                                            break;
                                        }
                                        else if (uinput4 == 2)
                                        {
                                            CreatePirate();
                                            ChangePirate();
                                            break;
                                        }
                                    }
                                    else //Pirat konnte sich retten
                                    {
                                        currentInsel = meer.GetInsel()[rnd.Next(0, 3)]; //auf zufällige Insel gerettet
                                        currentInsel.AddBesucher(currentPirat);
                                        currentPirat.SetStandort(currentInsel);
                                        currentStandort = Standort.Strand;
                                        Console.Clear();
                                        Animation.RPGPrint("...\n....\n.....", 150);
                                        Animation.RPGPrint("Das Rauschen des Meeres weckt dich auf.\nEs dauert einen Moment\n" +
                                            "bis du zu dir kommst und dich orientierst...\n" +
                                            "Schließlich stellst du fest, dass du an Land gespült wurdest!\n" +
                                            $"Du befindest dich jetzt auf dem Strand der Insel {currentInsel.GetBezeichnung()}");
                                        break;
                                    }
                                }
                                //sichere Überfahrt
                                currentInsel.AddBesucher(currentPirat); //Pirat auf neuer Insel
                                currentPirat.SetStandort(currentInsel); //Pirat hat neue Insel als Standort 
                                currentStandort = Standort.Insel;
                                Animation.Ship();
                                Console.SetCursorPosition(0, 0);
                                Animation.RPGPrint($"Du bist zur Insel \"{currentInsel.GetBezeichnung()}\" gefahren.");
                                break;

                            case Standort.Huette:

                                while (true)
                                {
                                    Console.Clear();
                                    Animation.RPGPrint($"~~~=== {currentHuette.GetBezeichnung()} ===~~~");
                                    if (currentPirat.GetTaler() < 10)
                                    {
                                        Animation.RPGPrint("Du hast nicht genug Taler, um ein Zimmer zu mieten!\nDu wurdest vor die Tür geworfen!");
                                        Console.ReadLine();
                                        break;
                                    }
                                    Animation.RPGPrint($"Du hast zur Zeit {currentPirat.GetTaler()} Taler.\nEin Zimmer kostet 10 Taler die Nacht.\nWillst du ein Zimmer mieten? (j = ja)\n" + menue);
                                    if (Console.ReadKey().KeyChar != 'j')
                                        break;
                                    Animation.RPGPrint("\nFür wie viele Nächte willst du übernachten? (max = 5)\n" + menue);
                                    if (!InputCheck.CheckUInt(out uinput3) || uinput3 > 5)
                                        break;
                                    else if (currentPirat.GetTaler() < uinput3 * 10)
                                    {
                                        Animation.RPGPrint("Du hast nicht genug Taler für so viele Nächte!");
                                        continue;
                                    }
                                    currentPirat.SetTaler(currentPirat.GetTaler() - (int)(uinput3) * 10);

                                    Animation.Sleep();

                                    if (currentPirat.GetBetrunkenheit() > 0)
                                    {
                                        for (int i = 0; i < uinput3; i++)
                                            //Betrunkenheitslevel sinkt um 1-3 Punkte pro Nacht
                                            currentPirat.SetBetrunkenheit(currentPirat.GetBetrunkenheit() - rnd.Next(1, 4));
                                        if (currentPirat.GetBetrunkenheit() < 0)
                                            currentPirat.SetBetrunkenheit(0);
                                        Animation.RPGPrint($"Dein Betrunkenheitslevel ist auf {currentPirat.GetBetrunkenheit()} gesunken.");
                                    }
                                    Animation.RPGPrint($"Du hast {uinput3} Nacht/Nächte geschlafen und fühlst dich ausgeruht.");
                                    break;
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
