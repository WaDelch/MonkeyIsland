using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Schiff : Lokation
    {
        private Meer meer;

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
