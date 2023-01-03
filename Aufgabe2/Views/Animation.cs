using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MonkeyIsland1
{
    internal static class Animation
    {
        public static void Rocket()
        {
            int tempCursX = Console.CursorLeft;
            int tempCursY = Console.CursorTop;
            int buff = 0;
            Console.CursorVisible = false;
            for (int i = 0; i < 22; i++)
            {
                Console.SetCursorPosition(70 + buff, 3);
                if (Console.CursorLeft > 20)
                    buff -= 2;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" _____/");
                Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
                Console.SetCursorPosition(71 + buff, 4);
                Console.Write("<|_____|");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(">>>>>");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
                Console.SetCursorPosition(72 + buff, 5);
                Console.Write("      \\");
                Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft));
                Thread.Sleep(35);
            }
            Console.SetCursorPosition(tempCursX, tempCursY);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.CursorVisible = true;
        }

        public static void Ship(bool cancelAnim = false)
        {
            int tempCursorX = Console.CursorLeft;
            int tempCursorY = Console.CursorTop;
            string[] shipParts = { " ||", " |\\__‡___||______/|", " |                |", " \\________________/", "¯\\", "  \\", "   \\", "    \\", "_____\\" };

            Console.CursorVisible = false; //verringert Flackern beim Zeichnen
            Console.SetCursorPosition(0, 10);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("____\n    \\__\n       \\"); //Linker Strand
            Console.SetCursorPosition(95, 10); //
            Console.Write("___");              //
            Console.SetCursorPosition(88, 11); // Rechter Strand
            Console.Write("______/");          //
            Console.SetCursorPosition(87, 12); //
            Console.Write("/");                //

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition(8, 12);        //
            Console.WriteLine(new string('~', 79));  // Wasser zeichnen
            Console.WriteLine(new string('~', 98));  //
            Console.WriteLine(new string('~', 98));  //

            //Schiffsanimation zeichnen
            for (int k = 0; k < 61; k++)
            {
                if(k == 30 && cancelAnim == true) //Animation bricht in der Mitte ab
                                                  //falls der Pirat vom Schiff gefallen ist
                    break;

                //Windsegel zeichnen
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int i = 4; i < 9; i++)
                {
                    Console.SetCursorPosition(19 + k, i + 1);
                    Console.WriteLine(shipParts[i]);
                }
                //Mast Zeichnen
                Console.ForegroundColor = ConsoleColor.DarkRed;
                for (int i = 0; i < 5; i++)
                {
                    Console.SetCursorPosition(16 + k, i + 5);
                    Console.Write(shipParts[0]);
                }
                //Schiffsrumpf zeichnen
                for (int i = 1; i < 4; i++)
                {
                    Console.SetCursorPosition(8 + k, 9 + i);
                    Console.WriteLine(shipParts[i]);
                }
                Console.SetCursorPosition(8 + k, 12); //nachfolgendes Wasser zeichnen
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write('~');
                Thread.Sleep(55);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorLeft = tempCursorX;
            Console.CursorTop = tempCursorY;
            Console.CursorVisible = true;
        }

        public static void Sleep()
        {
            int tempCursorX = Console.CursorLeft;
            int tempCursorY = Console.CursorTop;
            string sleeper = "" +
                "o_______o    \n" +
                "| &&&,  |  \n" +
                "|&& = > |\n" +
                "\\_&__C___\\\n" +
                "|\\________\\\n" +
                "|'\\        \\\n" +
                "  '\\ (      \\\n" +
                "   '\\   `    \\\n" +
                "    '\\o_______\\o\n" +
                "     '|________|\n" +
                "      |        |";
            string[] sleeperParts = sleeper.Split('\n');
            string window = "" +
                " _____________\n" +
                "| * (  *    . |\n" +
                "|.      . *  *|\n" +
                "|  *     *  . |\n" +
                "|_____________|";
            string[] windowParts = window.Split('\n');

            Console.CursorVisible = false;
            //zeichne Bett mit Schläfer
            for (int i = 0; i < sleeperParts.Length; i++)
            {
                Console.SetCursorPosition(65, i + 5);
                Console.WriteLine(sleeperParts[i]);
            }
            //zeichne Fenster
            for (int i = 0; i < windowParts.Length; i++)
            {
                Console.SetCursorPosition(85, i + 1);
                Console.WriteLine(windowParts[i]);
            }

            //Schlafanimation zeichnen
            Console.SetCursorPosition(75, 7);
            for (int i = 0; i < 16; i++)
            {
                if (i % 4 == 3)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Console.CursorLeft -= 2;
                        Console.CursorTop++;
                        Console.Write(" ");
                        Console.CursorLeft--;
                    }
                    Console.SetCursorPosition(75, 7);
                }
                Console.Write("Z");
                Console.CursorLeft++;
                Console.CursorTop--;
                Thread.Sleep(650);
            }
            Console.SetCursorPosition(tempCursorX, tempCursorY);
            Console.CursorVisible = true;
        }

        public static void DigSite()
        {
            int tempCursorX = Console.CursorLeft;
            int tempCursorY = Console.CursorTop;
            Console.CursorVisible = false; //verringert Flackern beim Zeichnen
            string[] sandParts = {"______", "/      \\", "_____        ______/        \\____",
                                "|    __|", "|   |", "\\___/"};
            int[] sandPosX = { 67, 66, 46, 51, 51, 51 };
            string[] shovelParts = { "     ", "==== ", " || ", " ---- ", "|    |", "\\    /", " \\__/", "\\    /", " \\_" };

            //Sand
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < sandParts.Length; i++)
            {
                Console.SetCursorPosition(sandPosX[i], i + 14);
                Console.WriteLine(sandParts[i]);
            }

            //Schaufelanimation
            for (int k = 0; k < 10; k++)
            {
                Thread.Sleep(55); //Delay am Start für schnelleren Grafikfix (s.u.)
                Console.ForegroundColor = ConsoleColor.DarkRed;

                //Schaufelgriff zeichnen
                for (int i = 0; i < 2; i++) //
                {
                    Console.SetCursorPosition(53, k + i);
                    Console.WriteLine(shovelParts[i]);
                }
                //Schaufelstiel zeichnen
                for (int i = 1; i < 5; i++)
                {
                    Console.SetCursorPosition(53, k + i + 1);
                    Console.WriteLine(shovelParts[2]);
                }
                //Schaufel zeichnen
                Console.ForegroundColor = ConsoleColor.DarkGray;
                for (int i = 3; i < 7; i++)
                {
                    Console.SetCursorPosition(52, k + i + 3);
                    if (k > 7 && i > 4)
                        Console.WriteLine(shovelParts[i + 2]);
                    else
                        Console.WriteLine(shovelParts[i]);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(56, 17); //kleiner Grafikfix
            Console.Write('_');
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(tempCursorX, tempCursorY);
            Console.CursorVisible = true;
        }

        public static void SkullBones()
        {

            string skullBones = "" +
                "                 uuuuuuu\n" +
                "             uu$$$$$$$$$$$uu\n" +
                "          uu$$$$$$$$$$$$$$$$$uu\n" +
                "         u$$$$$$$$$$$$$$$$$$$$$u\n" +
                "        u$$$$$$$$$$$$$$$$$$$$$$$u\n" +
                "       u$$$$$$$$$$$$$$$$$$$$$$$$$u\n" +
                "       u$$$$$$$$$$$$$$$$$$$$$$$$$u\n" +
                "       u$$$$$$\"   \"$$$\"   \"$$$$$$u\n" +
                "       \"$$$$\"      u$u       $$$$\"\n" +
                "        $$$u       u$u       u$$$\n" +
                "        $$$u      u$$$u      u$$$\n" +
                "         \"$$$$uu$$$   $$$uu$$$$\"\n" +
                "          \"$$$$$$$\"   \"$$$$$$$\"\n" +
                "            u$$$$$$$u$$$$$$$u\n" +
                "             u$\"$\"$\"$\"$\"$\"$u\n" +
                "  uuu        $$u$ $ $ $ $u$$       uuu\n" +
                " u$$$$        $$$$$u$u$u$$$       u$$$$\n" +
                "  $$$$$uu      \"$$$$$$$$$\"     uu$$$$$$\n" +
                "u$$$$$$$$$$$uu    \"\"\"\"\"    uuuu$$$$$$$$$$\n" +
                "$$$$\"\"\"$$$$$$$$$$uuu   uu$$$$$$$$$\"\"\"$$$\"\n" +
                " \"\"\"      \"\"$$$$$$$$$$$uu \"\"$\"\"\"\n" +
                "           uuuu \"\"$$$$$$$$$$uuu\n" +
                "  u$$$uuu$$$$$$$$$uu \"\"$$$$$$$$$$$uuu$$$\n" +
                "  $$$$$$$$$$\"\"\"\"           \"\"$$$$$$$$$$$\"\n" +
                "   \"$$$$$\"                      \"\"$$$$\"\"\n" +
                "     $$$\"                         $$$$\"";


            string[] skullParts = skullBones.Split('\n');
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < skullParts.Length; i++)
                if (i % 2 == 0)
                    RPGPrint(skullParts[i]);
                else
                    RPGPrint(skullParts[i], 10, true);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorVisible = true;
        }

        public static void Drink(bool water = true, int aniSpeed = 375)
        {
            string beerTop = "" +
                "           _, . '__ .\n" +
                "      ,o(__),_)o(_)O,o\n" +
                "    o(_, -o(_)(), (__(_)o\n" +
                "    .O(  )o,   ).( )o( )O\n" +
                "      |  |  |  |  |  |,_)\n" +
                " .----|  |  |  |  |  |o(_)\n" +
                "/ .---|  |  |  |  |  |O_)";

            string waterTop = "\n\n\n" +
                "       ______________\n" +
                "      |  |  |  |  |  |\n" +
                " .----|  |  |  |  |  |\n" +
                "/ .---|  |  |  |  |  |";

            string lowerGlass = "" +
                "| |   |  |  |  |  |  |\n" +
                "| |   |  |  |  |  |  |\n" +
                "\\ '---|  |  |  |  |  |\n" +
                " '----|  |  |  |  |  |\n" +
                "      |  |  |  |  |  |\n" +
                "      \\  \\  \\  /  /  /\n" +
                "       `\"\"\"\"\"\"\"\"\"\"\"\"\"";

            string[] beerTopParts = beerTop.Split('\n');
            string[] waterTopParts = waterTop.Split('\n');
            string[] lowerGlassParts = lowerGlass.Split('\n');

            string[] fullGlass;
            ConsoleColor liquidColor;
            if (water)
            {
                fullGlass = waterTopParts.Concat(lowerGlassParts).ToArray();
                liquidColor = ConsoleColor.DarkCyan;
            }
            else
            {
                fullGlass = beerTopParts.Concat(lowerGlassParts).ToArray();
                liquidColor = ConsoleColor.DarkYellow;
            }
            Console.CursorVisible = false;
            int tempCursorX = Console.CursorLeft;
            int tempCursorY = Console.CursorTop;

            //Glas zeichnen
            for (int i = 0; i < fullGlass.Length; i++)
            {
                Console.SetCursorPosition(53, i + 1);
                Console.WriteLine(fullGlass[i]);
            }

            //Glasinhalt zeichnen
            Console.BackgroundColor = liquidColor;
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(60, 5 + i);
                for (int k = 0; k < 5; k++)
                {
                    Console.Write("  ");
                    Console.CursorLeft++;
                }
            }
            Thread.Sleep(aniSpeed);
            //Trinkanimation zeichnen
            Console.BackgroundColor = ConsoleColor.Black;
            //Schaum bei alkoholischen Getränken entfernen
            if (!water)
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.SetCursorPosition(57, i + 1);
                    Console.WriteLine(new String(' ', 21));
                    Thread.Sleep(aniSpeed);
                }
                Console.SetCursorPosition(53, 4);
                Console.WriteLine(waterTopParts[3] + "    ");
            }
            //Glasinhalt schrittweise leeren
            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(aniSpeed);
                Console.SetCursorPosition(60, 5 + i);
                for (int k = 0; k < 5; k++)
                {
                    Console.Write("  ");
                    Console.CursorLeft++;
                }
            }
            Console.SetCursorPosition(tempCursorX, tempCursorY);
        }

        //Platzhaler für mögliche Zeichnungen
        static void drawThis(int posX, int posY, char drawMe, ConsoleColor dColor)
        {
            ConsoleColor tColor = Console.ForegroundColor;
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = dColor;
            Console.Write(drawMe);
            Console.ForegroundColor = tColor;
        }

        public static void RPGPrint(string text, int cDelay = 10, bool rev = false)
        {
            if (!rev)
            {
                foreach (char c in text)
                {
                    Console.Write(c);
                    if (c != ' ') //kein Delay bei Leerzeichen = fluessigeres Zeichnen
                        Thread.Sleep(cDelay);
                }
            }
            else
            {
                int len = text.Length - 1;
                Console.CursorLeft = len;
                for (int i = len; i >= 0; i--)
                {
                    if (text[i] == ' ') //Leerzeichen links der Zeichnung auslassen
                    {
                        if (Console.CursorLeft > 1)
                            Console.CursorLeft--;
                        continue;
                    }
                    else
                    {
                        Console.Write(text[i]);
                        Thread.Sleep(cDelay);
                        if (Console.CursorLeft > 2)
                            Console.CursorLeft -= 2;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}