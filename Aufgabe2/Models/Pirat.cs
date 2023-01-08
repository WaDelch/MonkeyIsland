using MonkeyIsland1.Models.Lokations;
using System;
namespace MonkeyIsland1.Models
{
    [Serializable]
    internal class Pirat
    {
        private string name;
        private Lokation lokation;
        //private Standort standort;
        private Insel insel;
        private int betrunkenheit;
        private int taler;
        private Meer meer;

        public Pirat(string n, Meer m, Insel i, Lokation l = null)
        {
            this.name = n;
            //this.standort = Standort.Insel;
            this.lokation = l;
            this.insel = i;
            this.meer = m;
            this.betrunkenheit = 0;
            if (n.ToLower() == "waldi")
                this.taler = 5000; //CHEATER!!!
            else
                this.taler = 0;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string n)
        {
            this.name = n;
        }

        public Lokation GetLokation()
        {
            if (this.lokation != null)
                return this.lokation;
            else
                return this.insel;
        }

        public void SetLokation(Lokation l)
        {
            if (this.lokation != null) //Pirat verlässt vorherigen Standort, falls vorhanden.
                this.lokation.DelBesucher(this);
            this.lokation = l; //Pirat wechselt zu neuem Standort
            this.lokation.AddBesucher(this); //Pirat wird in neuem Standort aufgenommen
        }

        //public Standort GetStandort()
        //{
        //    return this.standort;
        //}

        //public void SetStandort(Standort s)
        //{
        //    this.standort = s;
        //}

        public Insel GetInsel()
        {
            return this.insel;
        }

        public void SetInsel(Insel i)
        {
            this.insel.DelBesucher(this);   //Pirat ist nicht mehr auf der vorherigen Insel
            this.lokation.DelBesucher(this);//Pirat ist nicht mehr auf vorherigem Standort    
            this.insel = i;                 //Pirat wechselt zu neuer Insel
            this.insel.AddBesucher(this);   //Pirat wird auf neuer Insel aufgenommen
            this.lokation = null;           //Pirat befindet sich noch an keinem anderen Standort
        }

        public Meer GetMeer()
        {
            return this.meer;
        }

        public void SetMeer(Meer m)
        {
            this.meer = m;
        }

        public int GetBetrunkenheit()
        {
            return this.betrunkenheit;
        }

        public void SetBetrunkenheit(int i)
        {
            this.betrunkenheit = i;
        }

        public int GetTaler()
        {
            return this.taler;
        }

        public void SetTaler(int t)
        {
            this.taler = t;
        }
    }
}
