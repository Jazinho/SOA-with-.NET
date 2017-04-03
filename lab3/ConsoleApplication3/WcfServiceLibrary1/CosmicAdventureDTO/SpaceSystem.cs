using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CosmicAdventureDTO
{
    [DataContract]
    public class SpaceSystem
    {
        public SpaceSystem() { }
        public SpaceSystem(String Name, int MinShipPower, int BaseDistance, int Gold)
        {
            this.Name = Name;
            this.MinShipPower = MinShipPower;
            this.BaseDistance = BaseDistance;
            this.Gold = Gold;
        }

        [DataMember]
        public String Name;

        public int MinShipPower;

        [DataMember]
        public int BaseDistance;

        public int Gold;
    }

    [DataContract]
    public class Starship{
        [DataMember]
        public List<Person> Crew = new List<Person>();

        [DataMember]
        public int Gold;

        [DataMember]
        public int ShipPower;

        public Starship() { }

        public Starship(int Gold, int ShipPower)
        {
            Crew.Add(new Person("Jacek", "Placek", 20));
            Crew.Add(new Person("Kuba", "Tuba", 20));
            Crew.Add(new Person("Janek", "Dzbanek", 20));
            this.Gold = Gold;
            this.ShipPower = ShipPower;
        }
    }

    public class Person{
        public string Name;
        public string Nick;
        public float Age;

        public Person() { }

        public Person(string Name, string Nick, float Age)
        {
            this.Name = Name;
            this.Nick = Nick;
            this.Age = Age;
        }
    }

}
