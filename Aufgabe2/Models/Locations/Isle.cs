using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal class Isle : Location
    {
        public Location[] locations { get; private set; } = { new Bar(), new Beach(), new Ship(), new Graveyard(), new Hut(), new Shop() };
        //private Strand strand = new Strand();
        //private Kneipe kneipe = new Kneipe();
        //private Friedhof friedhof = new Friedhof();
        //private Schiff schiff = new Schiff(); //Schiffe sind z.Z. der Einfachheit halber Kompositionen von Inseln
        //                                      //Jedes Schiff hat also einen festen Heimathafen, von dem es voll abhängig ist
        //private Huette huette = new Huette();

        public T GetLocation<T>() where T:Location//generische Get-Methode ersetzt einzelne Methoden und ist leichter skalierbar
        {
            foreach (var item in this.locations)
                if (item is T)
                    return (T)item;
            return null;
        }

        public void SetLocation<T>(Location l) where T:Location
        {
            this.locations.SetValue(l, Array.IndexOf(locations, GetLocation<T>()));
        }

        //public Strand GetStrand()
        //{
        //    return (Strand)this.standorte.OfType<Strand>();
        //}

        //public void SetStrand(Strand s)
        //{
        //    this.standorte.SetValue(s, Array.IndexOf(standorte, GetStrand()));
        //}

        //public Kneipe GetKneipe()
        //{
        //    return this.kneipe;
        //}

        //public void SetKneipe(Kneipe k)
        //{
        //    this.kneipe = k;
        //}

        //public Schiff GetSchiff()
        //{
        //    return this.schiff;
        //}

        //public void SetSchiff(Schiff s)
        //{
        //    this.schiff = s;
        //}

        //public Friedhof GetFriedhof()
        //{
        //    return this.friedhof;
        //}

        //public void SetFriedhof(Friedhof f)
        //{
        //    this.friedhof = f;
        //}

        //public Huette GetHuette()
        //{
        //    return this.huette;
        //}

        //public void SetHuette(Huette h)
        //{
        //    this.huette = h;
        //}
    }
}
