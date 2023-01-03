using System;
using System.Collections.Generic;

namespace MonkeyIsland1.Models.Lokation
{
    [Serializable]
    internal class Friedhof : Lokation
    {
        private List<Pirat> dauerbesucher = new List<Pirat>();

        public List<Pirat> GetDauerbesucher()
        {
            return this.dauerbesucher;
        }

        public void SetDauerbesucher(List<Pirat> p)
        {
            this.dauerbesucher = p;
        }

        public void AddDauerbesucher(Pirat p)
        {
            this.dauerbesucher.Add(p);
            p.GetMeer().DelPirat(p);
        }

        public void Event()
        {
            Console.Clear();
            Animation.RPGPrint($"~~~=== {this.bezeichnung} ===~~~");
            Animation.RPGPrint($"Du besuchst die Gräber auf dem Friedhof." +
                "\nFolgende Piraten liegen hier begraben:");
            for (int i = 0; i < this.dauerbesucher.Count; i++)
                Animation.RPGPrint(this.dauerbesucher[i].GetName());
        }
    }
}
