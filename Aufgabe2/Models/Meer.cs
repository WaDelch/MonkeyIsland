using MonkeyIsland1.Models.Locations;
using System;
using System.Collections.Generic;

namespace MonkeyIsland1.Models
{
    [Serializable]
    internal class Meer
    {
        private string bezeichnung;
        private Insel[] insel; //Inseln in diesem Meer
        private List<Schiff> schiff = new List<Schiff>(); //WiP - Schiffe, die sich z.Z. in diesem Meer befinden
        private List<Pirat> pirat = new List<Pirat>(); //WiP - Piraten, die z.Z. in diesem Meer schwimmen

        public Meer(int nInseln) //Standardwerte
        {
            this.bezeichnung = "Blutmeer";
            this.insel = new Insel[nInseln];
            for (int i = 0; i < nInseln; i++)
            {
                insel[i] = new Insel();
                insel[i].SetBezeichnung($"Insel{i + 1}");
                insel[i].GetLokation<Kneipe>().SetBezeichnung($"Kneipe{i + 1}");
                insel[i].GetLokation<Strand>().SetBezeichnung($"Strand{i + 1}");
                insel[i].GetLokation<Schiff>().SetBezeichnung($"Schiff{i + 1}");
                insel[i].GetLokation<Friedhof>().SetBezeichnung($"Friedhof{i + 1}");
                insel[i].GetLokation<Huette>().SetBezeichnung($"Huette{i + 1}");
            }
        }

        public string GetBezeichnung()
        {
            return this.bezeichnung;
        }
        public void SetBezeichnung(string s)
        {
            this.bezeichnung = s;
        }
        public Insel[] GetInsel()
        {
            return this.insel;
        }
        public void SetInsel(Insel[] i)
        {
            this.insel = i;
        }
        public List<Schiff> GetSchiff()
        {
            return this.schiff;
        }
        public void SetSchiff(List<Schiff> s)
        {
            this.schiff = s;
        }
        public void SetSchiff(Schiff[] s)
        {
            this.schiff.Clear();
            for (int i = 0; i < s.Length; i++)
                this.schiff.Add(s[i]);
        }
        public void AddSchiff(Schiff s)
        {
            this.schiff.Add(s);
        }
        public void DelSchiff(Schiff s)
        {
            this.schiff.Remove(s);
        }
        public List<Pirat> GetPirat()
        {
            return this.pirat;
        }
        public void AddPirat(Pirat p)
        {
            this.pirat.Add(p);
            p.GetLokation().DelBesucher(p);
        }
        public void DelPirat(Pirat p)
        {
            this.pirat.Remove(p);
        }
    }
}
