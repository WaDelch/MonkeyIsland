using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Friedhof : Lokation
    {
        private List<Pirat> dauerbesucher = new List<Pirat>();

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
