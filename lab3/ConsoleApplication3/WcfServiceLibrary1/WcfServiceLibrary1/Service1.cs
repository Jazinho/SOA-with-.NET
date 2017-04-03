using CosmicAdventureDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        private List<SpaceSystem> _systems = new List<SpaceSystem>();

        public void InitializeGame()
        {
            Random r = new Random();
            _systems.Add(new SpaceSystem("Sys1",r.Next(10,40), r.Next(20, 120), r.Next(3000, 7000)));
            _systems.Add(new SpaceSystem("Sys2", r.Next(10, 40), r.Next(20, 120), r.Next(3000, 7000)));
            _systems.Add(new SpaceSystem("Sys3", r.Next(10, 40), r.Next(20, 120), r.Next(3000, 7000)));
            _systems.Add(new SpaceSystem("Sys4", r.Next(10, 40), r.Next(20, 120), r.Next(3000, 7000)));
        }

        public Starship SendStarship(Starship starship, string systemName)
        {
            foreach (SpaceSystem s in _systems)
            {
                if (s.Name.Equals(systemName))
                {
                    foreach (Person p in starship.Crew)
                    {
                        if (starship.ShipPower <= 20)
                        {
                            p.Age = p.Age + (2 * s.BaseDistance) / 12;
                        }else if (starship.ShipPower > 20 && starship.ShipPower <= 30)
                        {
                            p.Age = p.Age + (2 * s.BaseDistance) / 6;
                        }else
                        {
                            p.Age = p.Age + (2 * s.BaseDistance) / 4;
                        }
                        if (p.Age > 90) starship.Crew.Remove(p);
                    }
                    if(starship.ShipPower < s.MinShipPower)
                    {
                        starship.Gold = s.Gold;
                        _systems.Remove(s);
                    }
                }
                return starship;
            }
            starship.Crew.Clear();
            return starship;
        }

        public Starship GetStarship(int money)
        {
            Random r = new Random();
            int ShipPower = 0;
            if (money > 1000 && money <= 3000) ShipPower = r.Next(10, 25);
            if (money > 3001 && money <= 10000) ShipPower = r.Next(20, 35);
            if (money > 10000) ShipPower = r.Next(35, 60);
            Starship Starship = new Starship(0, ShipPower); 
            return Starship;
        }

        public SpaceSystem GetSystem()
        {
            return _systems.Count() > 0 ? _systems.First() : null;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
