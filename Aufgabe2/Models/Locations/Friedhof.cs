using System;
using System.Collections.Generic;
using MonkeyIsland1.Views;
using MonkeyIsland1.Controllers;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Friedhof : Lokation
    {
        private List<string> dauerbesucher = new List<string>();

        public List<string> GetDauerbesucher()
        {
            return this.dauerbesucher;
        }

        public void SetDauerbesucher(List<string> p)
        {
            this.dauerbesucher = p;
        }

        public void AddDauerbesucher(Pirat p)
        {
            this.dauerbesucher.Add(p.GetName());    //Pirat wird hier beerdigt
            p.GetMeer().DelPirat(p);                //Pirat nicht mehr im Meer
            p = null;                               //Pirat-Objekt wird gelöscht
        }

        public override void Event(Transporter t)
        {
            Console.Clear();
            Animation.RPGPrint($"~~~=== {this.GetBezeichnung()} ===~~~");
            Animation.RPGPrint($"Du besuchst die Gräber auf dem Friedhof." +
                "\nFolgende Piraten liegen hier begraben:");
            foreach (string name in this.dauerbesucher)
                Animation.RPGPrint(name);
        }
    }
}
