using System;
using System.Collections.Generic;

namespace MonkeyIsland1.Models.Lokations
{
    [Serializable]
    internal class Lokation
    {
        private string bezeichnung;
        private List<Pirat> besucher = new List<Pirat>();
        public Random rnd = new Random();
        public int randomZahl;
        public uint uInput;

        public String GetBezeichnung()
        {
            return this.bezeichnung;
        }

        public void SetBezeichnung(string b)
        {
            this.bezeichnung = b;
        }

        public List<Pirat> GetBesucher()
        {
            return this.besucher;
        }

        public void SetBesucher(List<Pirat> p)
        {
            for (int i = 0; i < p.Count; i++)
                p[i].GetLokation().DelBesucher(p[i]);
            this.besucher = p;
        }

        public void AddBesucher(Pirat p)
        {
            //Pirat wechselt Standort -> Pirat aus der Liste des vorherigen Standortes löschen
            //if (this.besucher.Contains(p))
            //        this.besucher.Remove(p);
            //Pirat dem neuen Standort hinzufügen
            this.besucher.Add(p);
        }

        public void DelBesucher(Pirat p)
        {
            this.besucher.Remove(p);
        }

        //public virtual void Event()
        //{

        //}
    }
}
