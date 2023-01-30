using MonkeyIsland1.Models.Locations;
using System;
using System.Collections.Generic;

namespace MonkeyIsland1.Models
{
    [Serializable]
    internal class Sea
    {
        private string description;
        private Isle[] isles; //Inseln in diesem Meer
        private List<Ship> ships = new List<Ship>(); //WiP - Schiffe, die sich z.Z. in diesem Meer befinden
        private List<Pirate> pirates = new List<Pirate>(); //WiP - Piraten, die z.Z. in diesem Meer schwimmen

        public Sea(int nIsles) //Standardwerte
        {
            this.description = "Blutmeer";
            this.isles = new Isle[nIsles];
            for (int i = 0; i < nIsles; i++)
            {
                isles[i] = new Isle();
                isles[i].SetDescription($"Insel{i + 1}");
                isles[i].GetLocation<Bar>().SetDescription($"Kneipe{i + 1}");
                isles[i].GetLocation<Beach>().SetDescription($"Strand{i + 1}");
                isles[i].GetLocation<Ship>().SetDescription($"Schiff{i + 1}");
                isles[i].GetLocation<Graveyard>().SetDescription($"Friedhof{i + 1}");
                isles[i].GetLocation<Hut>().SetDescription($"Huette{i + 1}");
                isles[i].GetLocation<Shop>().SetDescription($"Shop{i + 1}");
            }
        }

        public string GetDescription() => this.description;

        public void SetDescription(string s)
        {
            this.description = s;
        }

        public Isle[] GetIsles() => this.isles;

        public void SetIsles(Isle[] i)
        {
            this.isles = i;
        }

        public List<Ship> GetShips() => this.ships;

        public void SetShips(List<Ship> s)
        {
            this.ships = s;
        }

        public void AddShip(Ship s)
        {
            this.ships.Add(s);
        }

        public void DelShip(Ship s)
        {
            this.ships.Remove(s);
        }
        public List<Pirate> GetPirates() => this.pirates;

        public void AddPirate(Pirate p)
        {
            this.pirates.Add(p);
            p.GetLocation().DelVisitor(p);
        }

        public void DelPirate(Pirate p)
        {
            this.pirates.Remove(p);
        }
    }
}
