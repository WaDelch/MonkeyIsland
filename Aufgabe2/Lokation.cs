using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe2
{
    internal class Lokation
    {
        private string bezeichnung;
        private string art;
        private Insel insel;
        private List<Pirat> besucher = new List<Pirat>();

        public string GetBezeichnung()
        {
            return this.bezeichnung;
        }

        public void SetBezeichnung(string s)
        {
            this.bezeichnung = s;
        }

        public string GetArt()
        {
            return this.art;
        }

        public void SetArt(string s)
        {
            this.art = s;
        }

        public Insel GetInsel()
        {
            return this.insel;
        }

        public void SetInsel(Insel i)
        {
            this.insel = i;
        }

        public List<Pirat> GetBesucher()
        {
            return this.besucher;
        }

        public void SetBesucher(List<Pirat> p)
        {
            this.besucher = p;
        }

        public void AddBesucher(Pirat p)
        {
            this.besucher.Add(p);
        }

        public void DelBesucher(Pirat p)
        {
            this.besucher.Remove(p);
        }


    }
}
