using MonkeyIsland1.Models.Lokation;
using System;
namespace MonkeyIsland1.Models
{
    [Serializable]
    internal class Pirat
    {
        private string name;
        private Insel standort;
        private Insel heimat;
        private int betrunkenheit;
        private int taler;
        private Meer meer;

        public Pirat(string n, Meer m, Insel i)
        {
            this.name = n;
            this.standort = i;
            this.heimat = i;
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

        public Insel GetStandort()
        {
            return this.standort;
        }

        public void SetStandort(Insel l)
        {
            this.standort = l;
        }

        public Insel GetHeimat()
        {
            return this.heimat;
        }

        public void SetHeimt(Insel i)
        {
            this.heimat = i;
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
