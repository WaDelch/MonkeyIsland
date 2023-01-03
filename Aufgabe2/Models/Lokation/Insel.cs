using System;
using System.Collections.Generic;

namespace MonkeyIsland1.Models.Lokation
{
    [Serializable]
    internal class Insel
    {
        private string bezeichnung;
        private Strand strand = new Strand();
        private Kneipe kneipe = new Kneipe();
        private Friedhof friedhof = new Friedhof();
        private List<Pirat> besucher = new List<Pirat>();
        private Schiff schiff = new Schiff(); //Schiffe sind z.Z. der Einfachheit halber Kompositionen von Inseln
                                              //Jedes Schiff hat also einen festen Heimathafen, von dem es voll abhängig ist
        private Huette huette = new Huette();

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
            List<Pirat> tempB = besucher;
            for (int i = 0; i < kneipe.GetBesucher().Count; i++)
                if (!besucher.Contains(kneipe.GetBesucher()[i]))
                    tempB.Add(kneipe.GetBesucher()[i]);
            for (int i = 0; i < strand.GetBesucher().Count; i++)
                if (!besucher.Contains(strand.GetBesucher()[i]))
                    tempB.Add(strand.GetBesucher()[i]);
            return tempB;
        }

        public void SetBesucher(List<Pirat> p)
        {
            for (int i = 0; i < p.Count; i++)
                p[i].GetStandort().DelBesucher(p[i]);
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

        public Strand GetStrand()
        {
            return this.strand;
        }

        public void SetStrand(Strand s)
        {
            this.strand = s;
        }

        public Kneipe GetKneipe()
        {
            return this.kneipe;
        }

        public void SetKneipe(Kneipe k)
        {
            this.kneipe = k;
        }

        public Schiff GetSchiff()
        {
            return this.schiff;
        }

        public void SetSchiff(Schiff s)
        {
            this.schiff = s;
        }

        public Friedhof GetFriedhof()
        {
            return this.friedhof;
        }

        public void SetFriedhof(Friedhof f)
        {
            this.friedhof = f;
        }

        public Huette GetHuette()
        {
            return this.huette;
        }

        public void SetHuette(Huette h)
        {
            this.huette = h;
        }
    }
}
