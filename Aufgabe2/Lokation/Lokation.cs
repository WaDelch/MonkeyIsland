using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyIsland1
{
    internal abstract class Lokation
    {
        public string bezeichnung;
        public List<Pirat> besucher = new List<Pirat>();
        public Random rnd = new Random();
        public int randomZahl;
        public uint uInput;

        public String GetBezeichnung()
        {
            return this.bezeichnung;
        }

        public void SetBezeichnung(string b)
        {
            this.bezeichnung = b;
        }

        public List<Pirat> GetBesucher()
        {
            return this.besucher;
        }

        public void AddBesucher(Pirat p)
        {
            //Pirat wechselt Standort -> Pirat aus der Liste des vorherigen Standortes löschen
            if (p.GetStandort().GetFriedhof().GetBesucher().Contains(p))
                p.GetStandort().GetFriedhof().DelBesucher(p);
            else if (p.GetStandort().GetStrand().GetBesucher().Contains(p))
                p.GetStandort().GetStrand().DelBesucher(p);
            else if (p.GetStandort().GetKneipe().GetBesucher().Contains(p))
                p.GetStandort().GetKneipe().DelBesucher(p);
            else if (p.GetStandort().GetSchiff().GetBesucher().Contains(p))
                p.GetStandort().GetSchiff().DelBesucher(p);
            else if (p.GetStandort().GetHuette().GetBesucher().Contains(p))
                p.GetStandort().GetHuette().DelBesucher(p);
            //Pirat dem neuen Standort hinzufügen
            this.besucher.Add(p);
        }

        public void DelBesucher(Pirat p)
        {
            this.besucher.Remove(p);
        }
    }
}
