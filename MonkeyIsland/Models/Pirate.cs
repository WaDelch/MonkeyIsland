using System;
using System.Collections.Generic;
using System.Linq;
using MonkeyIsland1.Models.Locations;

namespace MonkeyIsland1.Models
{
    [Serializable]
    internal class Pirate
    {
        private string name;
        private Location location;
        public bool[] inventory = new bool[Shop.nItems];
        private Isle isle;
        private int drunkenness;
        private int coins;
        private Sea sea;

        public Pirate(string n, Sea s, Isle i, Location l = null)
        {
            this.name = n;
            this.location = l;
            this.isle = i;
            this.sea = s;
            this.drunkenness = 0;
            if (n.ToLower() == "waldi")
                this.coins = 5000; //CHEATER!!!
            else
                this.coins = 0;
        }

        public string GetName() => this.name;
        //{
        //    return this.name;
        //}

        public void SetName(string n)
        {
            this.name = n;
        }

        public Location GetLocation()
        {
            //if (this.location != null)
            //    return this.location;
            //else
            //    return this.isle;
            return this.location ?? this.isle;
        }

        public void SetLocation(Location l)
        {
            if (this.location != null)          //Pirat verlässt vorherigen Standort, falls vorhanden.
                this.location.DelVisitor(this);
            this.location = l;                  //Pirat wechselt zu neuem Standort
            this.location.AddVisitor(this);     //Pirat wird in neuem Standort aufgenommen
        }

        public bool[] GetInventory() => this.inventory;

        public void SetInventory(bool[] inv)
        {
            this.inventory = inv;
        }

        public void AddItem(int iPosition)
        {
            this.inventory[iPosition] = true;
        }

        public void DelItem(int iPosition)
        {
            this.inventory[iPosition] = false;
        }

        public Isle GetIsle() => this.isle;

        public void SetIsle(Isle i)
        {
            this.isle.DelVisitor(this);     //Pirat ist nicht mehr auf der vorherigen Insel
            this.location.DelVisitor(this); //Pirat ist nicht mehr auf vorherigem Standort    
            this.isle = i;                  //Pirat wechselt zur neuen Insel
            this.isle.AddVisitor(this);     //Pirat wird auf neuer Insel aufgenommen
            this.location = null;           //Pirat befindet sich noch an keinem anderen Standort
        }

        public Sea GetSea() => this.sea;

        public void SetSea(Sea s)
        {
            this.sea = s;
        }

        public int GetDrunkenness() => this.drunkenness;

        public void SetDrunkenness(int i)
        {
            this.drunkenness = i;
        }

        public int GetCoins() => this.coins;

        public void SetCoins(int t)
        {
            this.coins = t;
        }
    }
}
