using System;
using System.Collections.Generic;
using System.Linq;

namespace MonkeyIsland1.Models.Lokations
{
    [Serializable]
    internal class Insel : Lokation
    {
        public Lokation[] standorte { get; private set; } = { new Kneipe(), new Strand(), new Schiff(), new Friedhof(), new Huette() };
        //private Strand strand = new Strand();
        //private Kneipe kneipe = new Kneipe();
        //private Friedhof friedhof = new Friedhof();
        //private Schiff schiff = new Schiff(); //Schiffe sind z.Z. der Einfachheit halber Kompositionen von Inseln
        //                                      //Jedes Schiff hat also einen festen Heimathafen, von dem es voll abhängig ist
        //private Huette huette = new Huette();

        public T GetLokation<T>() where T:Lokation //generische Get-Methode ersetzt einzelne Methoden und ist skalierbar
        {
            foreach (var item in this.standorte)
                if (item is T)
                    return (T)item;
            return null;
        }

        public void SetLokation<T>(Lokation l) where T:Lokation
        {
            this.standorte.SetValue(l, Array.IndexOf(standorte, GetLokation<T>()));
        }

        //public int GetLokIndex<T>() where T:Lokation
        //{
        //    return Array.IndexOf(standorte, )
        //}

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
