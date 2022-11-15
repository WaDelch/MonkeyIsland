using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Schiff
    {
        private string bezeichnung;
        private List<Pirat> besatzung = new List<Pirat>();
        private Meer meer;

        public string GetBezeichnung()
        {
            return this.bezeichnung;
        }

        public void SetBezeichnung(string s)
        {
            this.bezeichnung = s;
        }

        public List<Pirat> GetBesatzung()
        {
            return this.besatzung;
        }

        public void SetBesatzung(List<Pirat> p)
        {
            this.besatzung = p;
            for (int i = 0; i < p.Count; i++)
            {
                p[i].GetStandort().GetKneipe().DelBesucher(p[i]);
                p[i].GetStandort().GetStrand().DelBesucher(p[i]);
                p[i].GetStandort().GetFriedhof().DelBesucher(p[i]);
            }
        }

        public void AddBesatzung(Pirat p)
        {
            this.besatzung.Add(p);
            p.GetStandort().GetStrand().DelBesucher(p);
            p.GetStandort().GetKneipe().DelBesucher(p);
            p.GetStandort().GetFriedhof().DelBesucher(p);
        }

        public void DelBesatzung(Pirat p)
        {
            this.besatzung.Remove(p);
        }

        public Meer GetMeer()
        {
            return this.meer;
        }

        public void SetMeer(Meer m)
        {
            this.meer = m;
        }

    }
}
