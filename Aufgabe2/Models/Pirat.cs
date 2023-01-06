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
            return this.lokation;
        }

        public void SetLokation(Lokation l)
        {
            this.lokation = l;
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
            this.insel = i;
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
