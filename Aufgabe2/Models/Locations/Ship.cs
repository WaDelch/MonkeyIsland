using MonkeyIsland1.Controllers;
using System;
using static MonkeyIsland1.Program;
using MonkeyIsland1.Views;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Ship : Location
    {
        public override void Event(Transporter t)
        {
            uint uInput2;
            Animation.RPGPrint("Zu welcher Insel möchtest du fahren?");
            for (int i = 0; i < t.sea.GetIsles().Length; i++) //length = 3
                Animation.RPGPrint($"{i + 1}) {t.sea.GetIsles()[i].GetDescription()}");
            Animation.RPGPrint(Output.back2mainMenue);
            if (!InputCheck.CheckUInt(out uInput) || uInput > t.sea.GetIsles().Length)
                return;

            if (uInput - 1 == Array.IndexOf(t.sea.GetIsles(), t.isle))
            {
                Animation.RPGPrint("Du bist schon hier!");
                return;
            }

            //Schiff fährt los
            Console.Clear();
            t.isle.DelVisitor(t.pirate); //Pirat nicht mehr auf der alten Insel
            t.isle.GetLocation<Ship>().DelVisitor(t.pirate); //Pirat nicht mehr auf dem Schiff (Resultat aller derzeit implementierten, folgenden Aktionen)
            Console.SetCursorPosition(0, 6);
            Console.WriteLine(t.isle.GetDescription()); //Startinsel anzeigen
            Console.SetCursorPosition(99 - t.isle.GetDescription().Length, 6);
            t.isle = t.sea.GetIsles()[uInput - 1]; //wechsle Insel
            Console.WriteLine(t.isle.GetDescription()); //Zielinsel anzeigen

            randomInt = rnd.Next(1, 101) + 5 * t.pirate.GetDrunkenness(); //Chance vom Schiff zu fallen, +5% pro Drunkennessslevel
            if (randomInt > 95) //Pirat vom Schiff gefallen
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
                if (drownChance + 5 * t.pirate.GetDrunkenness() > rnd.Next(1, 101)) //prüfen, ob ertrunken
                {
                    Animation.RPGPrint("Das letzte was du spürst ist eine allumfassende Kälte,\n" +
                        "die dich langsam verschlingt...");
                    Program.pirates.Remove(t.pirate); //Pirat nicht mehr in der Liste der lebenden Piraten

                    t.pirate.GetIsle().GetLocation<Graveyard>().AddPermanentVisitor(t.pirate); //Pirat auf dem Friedhof der zuletzt besuchten Insel beerdigt
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
                    t.isle = t.sea.GetIsles()[rnd.Next(0, t.sea.GetIsles().Length)]; //auf zufällige Insel gerettet
                    t.pirate.SetIsle(t.isle);
                    t.pirate.SetLocation(t.isle.GetLocation<Beach>()); //Pirat konnte sich an den Inselstrand retten
                    Console.Clear();
                    Animation.RPGPrint("...\n....\n.....", 150);
                    Animation.RPGPrint("Das Rauschen des t.seaes weckt dich auf.\nEs dauert einen Moment\n" +
                        "bis du zu dir kommst und dich orientierst...\n" +
                        "Schließlich stellst du fest, dass du an Land gespült wurdest!\n" +
                        $"Du befindest dich jetzt auf dem Strand der Insel \"{t.isle.GetDescription()}\"");
                    return;
                }
            }
            //sichere Überfahrt
            t.pirate.SetIsle(t.isle); //Pirat hat neue Insel als Standort 
            Animation.Ship();
            Console.SetCursorPosition(0, 0);
            Animation.RPGPrint($"Du bist zur Insel \"{t.isle.GetDescription()}\" gefahren.");
            return;
        }

    }
}
