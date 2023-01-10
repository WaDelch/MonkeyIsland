using System;
using System.Collections.Generic;
using MonkeyIsland1.Views;
using MonkeyIsland1.Controllers;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Graveyard : Location
    {
        private List<string> permanentVisitors = new List<string>();

        public List<string> GetPermanentVisitors() => this.permanentVisitors;

        public void SetPermanentVisitors(List<string> p)
        {
            this.permanentVisitors = p;
        }

        public void AddPermanentVisitor(Pirate p)
        {
            this.permanentVisitors.Add(p.GetName());    //Pirat wird hier beerdigt
            p.GetSea().DelPirate(p);                    //Pirat nicht mehr im Meer
            p = null;                                   //Pirat-Objekt wird gelöscht
        }

        public override void Event(Transporter t)
        {
            Console.Clear();
            Animation.RPGPrint($"~~~=== {this.GetDescription()} ===~~~");
            Animation.RPGPrint($"Du besuchst die Gräber auf dem Friedhof." +
                "\nFolgende Piraten liegen hier begraben:");
            foreach (string name in this.permanentVisitors)
                Animation.RPGPrint(name);
        }
    }
}
