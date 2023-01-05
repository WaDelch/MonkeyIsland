using System;
using System.Collections.Generic;

namespace MonkeyIsland1.Models.Lokations
{
    [Serializable]
    internal class Insel : Lokation
    {
        private Strand strand = new Strand();
        private Kneipe kneipe = new Kneipe();
        private Friedhof friedhof = new Friedhof();
        private Schiff schiff = new Schiff(); //Schiffe sind z.Z. der Einfachheit halber Kompositionen von Inseln
                                              //Jedes Schiff hat also einen festen Heimathafen, von dem es voll abhängig ist
        private Huette huette = new Huette();

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
