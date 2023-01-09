using MonkeyIsland1.Controllers;
using System;
using static MonkeyIsland1.Program;
using MonkeyIsland1.Views;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Schiff : Lokation
    {
        public override void Event(Transporter t)
        {
            uint uInput2;
            Animation.RPGPrint("Zu welcher Insel möchtest du fahren?");
            for (int i = 0; i < t.meer.GetInsel().Length; i++) //length = 3
                Animation.RPGPrint($"{i + 1}) {t.meer.GetInsel()[i].GetBezeichnung()}");
            Animation.RPGPrint(Output.back2mainMenue);
            if (!InputCheck.CheckUInt(out uInput) || uInput > t.meer.GetInsel().Length)
                return;

            if (uInput - 1 == Array.IndexOf(t.meer.GetInsel(), t.insel))
            {
                Animation.RPGPrint("Du bist schon hier!");
                return;
            }

            //Schiff fährt los
            Console.Clear();
            t.insel.DelBesucher(t.pirat); //Pirat nicht mehr auf der alten Insel
            t.insel.GetLokation<Schiff>().DelBesucher(t.pirat); //Pirat nicht mehr auf dem Schiff (Resultat aller derzeit implementierten, folgenden Aktionen)
            Console.SetCursorPosition(0, 6);
            Console.WriteLine(t.insel.GetBezeichnung()); //Startinsel anzeigen
            Console.SetCursorPosition(99 - t.insel.GetBezeichnung().Length, 6);
            t.insel = t.meer.GetInsel()[uInput - 1]; //wechsle Insel
            Console.WriteLine(t.insel.GetBezeichnung()); //Zielinsel anzeigen

            randomZahl = rnd.Next(1, 101) + 5 * t.pirat.GetBetrunkenheit(); //Chance vom Schiff zu fallen, +5% pro Betrunkenheitslevel
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
                    if (!InputCheck.CheckUInt(out uInput2) || uInput2 > 3)
                    {
                        Animation.RPGPrint("Du treibst im Wasser und wartest auf Hilfe, aber vergeblich...\n" +
                            "Deine Körpertemperatur und Überlebenschance sinkt!");
                        Console.ReadLine();
                        drownChance += 5;
                        return;
                    }
                    switch (uInput2)
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
                    if (uInput2 == 3) //Versuch sich an Land zu retten ist die letzte Entscheidung
                    {
                        Animation.RPGPrint("Du glaubst Land am Horizont erblicken zu können\n" +
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
                if (drownChance + 5 * t.pirat.GetBetrunkenheit() > rnd.Next(1, 101)) //prüfen, ob ertrunken
                {
                    Animation.RPGPrint("Das letzte was du spürst ist eine allumfassende Kälte,\n" +
                        "die dich langsam verschlingt...");
                    Program.piraten.Remove(t.pirat); //Pirat nicht mehr in der Liste der lebenden Piraten

                    t.pirat.GetInsel().GetLokation<Friedhof>().AddDauerbesucher(t.pirat); //Pirat auf dem Friedhof seiner Heimatsinsel beerdigt
                    Console.ReadLine();
                    Console.Clear();
                    Animation.RPGPrint("Du bist gestorben!");
                    Animation.SkullBones();
                    Animation.RPGPrint("Taste drücken...");
                    Console.ReadLine();
                    Console.Clear();
                    Animation.RPGPrint("Was willst du jetzt tun?\n1) Piraten wechseln\n2) Piraten erstellen und wechseln\n" +
                        "Sonstige Eingabe = Programm beenden");
                    if (!InputCheck.CheckUInt(out uInput2) || uInput2 > 2)
                    {
                        Animation.RPGPrint("Programm beendet.");
                        return;
                    }
                    if (uInput2 == 1)
                    {
                        PirateHandler.ChangePirate();
                        return;
                    }
                    else if (uInput2 == 2)
                    {
                        PirateHandler.CreatePirate();
                        PirateHandler.ChangePirate();
                        return;
                    }
                }
                else //Pirat konnte sich retten
                {
                    t.insel = t.meer.GetInsel()[rnd.Next(0, t.meer.GetInsel().Length)]; //auf zufällige Insel gerettet
                    t.pirat.SetInsel(t.insel);
                    t.pirat.SetLokation(t.insel.GetLokation<Strand>()); //Pirat konnte sich an den Inselstrand retten
                    Console.Clear();
                    Animation.RPGPrint("...\n....\n.....", 150);
                    Animation.RPGPrint("Das Rauschen des t.meeres weckt dich auf.\nEs dauert einen Moment\n" +
                        "bis du zu dir kommst und dich orientierst...\n" +
                        "Schließlich stellst du fest, dass du an Land gespült wurdest!\n" +
                        $"Du befindest dich jetzt auf dem Strand der Insel \"{t.insel.GetBezeichnung()}\"");
                    return;
                }
            }
            //sichere Überfahrt
            t.pirat.SetInsel(t.insel); //Pirat hat neue Insel als Standort 
            Animation.Ship();
            Console.SetCursorPosition(0, 0);
            Animation.RPGPrint($"Du bist zur Insel \"{t.insel.GetBezeichnung()}\" gefahren.");
            return;
        }

    }
}
