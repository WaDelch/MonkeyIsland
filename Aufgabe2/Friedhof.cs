using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Friedhof
    {
        private string bezeichnung;
        private List<Pirat> besucher = new List<Pirat>();
        private List<Pirat> dauerbesucher = new List<Pirat>();

        public string GetBezeichnung()
        {
            return this.bezeichnung;
        }

        public void SetBezeichnung(string s)
        {
            this.bezeichnung = s;
        }

        public List<Pirat> GetBesucher()
        {
            return this.besucher;
        }

        public void SetBesucher(List<Pirat> p)
        {
            this.besucher = p;
            for (int i = 0; i < p.Count; i++)
            {
                p[i].GetStandort().GetStrand().DelBesucher(p[i]);
                p[i].GetStandort().GetSchiff().DelBesatzung(p[i]);
                p[i].GetStandort().GetKneipe().DelBesucher(p[i]);
            }
        }

        public void AddBesucher(Pirat p)
        {
            this.besucher.Add(p);
            p.GetStandort().GetKneipe().DelBesucher(p);
            p.GetStandort().GetSchiff().DelBesatzung(p);
            p.GetStandort().GetStrand().DelBesucher(p);
        }

        public void DelBesucher(Pirat p)
        {
            this.besucher.Remove(p);
        }

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
    }
}
