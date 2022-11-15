using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal class Huette //WiP, Komposition von Insel, Piraten sollen hier gegen Taler ein Zimmer mieten und ihren Rausch ausschlafen können
    {
        private string bezeichnung;
        private List<Pirat> besucher = new List<Pirat>();

        public string GetBezeichnung()
        {
            return this.bezeichnung;
        }

        public void SetBezeichnung(string bezeichnung)
        {
            this.bezeichnung = bezeichnung;
        }
    }
}
