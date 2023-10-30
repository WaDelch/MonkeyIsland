using System;
using System.Collections.Generic;
using MonkeyIsland1.Controllers;

namespace MonkeyIsland1.Models.Locations
{
    [Serializable]
    internal abstract class Location
    {
        private string description;
        private List<Pirate> visitors = new List<Pirate>();
        public int randomInt;
        public static uint uInput;

        public String GetDescription() => this.description;

        public void SetDescription(string b)
        {
            this.description = b;
        }

        public List<Pirate> GetVisitors() => this.visitors;

        public void SetVisitors(List<Pirate> p)
        {
            for (int i = 0; i < p.Count; i++)
                p[i].GetLocation().DelVisitor(p[i]);
            this.visitors = p;
        }

        public void AddVisitor(Pirate p)
        {
            this.visitors.Add(p);
        }

        public void DelVisitor(Pirate p)
        {
            this.visitors.Remove(p);
        }

        public virtual void Event(Transporter t)
        {

        }
    }
}
