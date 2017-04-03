using ConsoleApplication3.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        public static List<Starship> _starships = new List<Starship>();
        public static bool _anySystem = true;
        public static int _gold = 1000;
        public static int _imperiumMoneyAskCount = 4;
        public static bool condition = true;

        static void Main(string[] args)
        {
            ConsoleApplication3.ServiceReference1.Service1Client cosmos = new ConsoleApplication3.ServiceReference1.Service1Client();
            ConsoleApplication3.ServiceReference2.Service1Client imperium = new ConsoleApplication3.ServiceReference2.Service1Client();
            cosmos.InitializeGame();
            string line;

            while(condition)
            {
                Console.WriteLine("Gold: "+ Program._gold);
                Console.WriteLine("Imperium money asks left: " + Program._imperiumMoneyAskCount);
                Console.WriteLine("To ask imperium for money press \'a\'.");
                Console.WriteLine("To buy a starship press \'b\'.");
                Console.WriteLine("To send a starship to a space system press \'s\'");
                Console.WriteLine("To exit the game press \'x\'");
                line = Console.ReadLine();
                switch (line)
                {
                    case "a":
                        if (Program._imperiumMoneyAskCount > 0)
                        {
                            Program._gold += imperium.GetMoneyFromImperium();
                            Program._imperiumMoneyAskCount -= 1;
                        }
                        break;
                    case "b":
                        Console.WriteLine("Current gold: {0}. Type an amount of money you want to spend for new starship", Program._gold);
                        int kwota = Int16.Parse(Console.ReadLine());
                        if (kwota > 0 && kwota < Program._gold) {
                            Program._starships.Add(cosmos.GetStarship(kwota));
                            _gold -= kwota;
                        }else
                        {
                            Console.WriteLine("You passed wrong input!");
                        }
                        break;
                    case "s":
                        SpaceSystem s = cosmos.GetSystem();
                        if(s == null)
                        {
                            Console.WriteLine("No systems.");
                            Program._anySystem = false;
                            break;
                        }
                        Console.WriteLine("System: {0}, distance: {1}", s.Name, s.BaseDistance);
                        int num = Program._starships.Count;
                        if(num == 0)
                        {
                            Console.WriteLine("No starships owned.");
                            break;
                        }
                        Console.WriteLine("Starships ready to trip: {0}", num);
                        Console.WriteLine("Wybierz statek wpisując jego numer(albo wyjdź wpisując literę e)");
                        int count = 1;
                        foreach (Starship st in _starships)
                        {
                            String crew="";
                            foreach(Person p in st.Crew)
                            {
                                crew += p.Name + " " + p.Nick + " " + p.Age + ", ";
                            }
                            crew.Remove(crew.Length - 1, 1);
                            Console.WriteLine("{0}. {1}, {2}", count, st.ShipPower, crew);
                            count += 1;
                        }
                        line = Console.ReadLine();
                        switch (line)
                        {
                            case "e":
                                break;
                            default:
                                int x = 0;
                                if(Int32.TryParse(line, out x) && Int32.Parse(line) > 0)
                                {
                                    int choice = Int32.Parse(line);
                                    Starship newShip = cosmos.SendStarship(_starships.ElementAt(choice-1), s.Name);
                                    _starships.RemoveAt(choice - 1);
                                    if (newShip.Gold > 0)
                                    {
                                        _gold += newShip.Gold;
                                        newShip.Gold = 0;
                                    }
                                    if (newShip.Crew.Length > 0) _starships.Add(newShip);
                                }else
                                {
                                    Console.WriteLine("Typed invalid ship number");
                                }
                                break;
                        }
                        break;
                    case "x":
                        condition = false;
                        break;
                    default:
                        Console.WriteLine("Received unknown command.");
                        break;
                }
            }
            if (_anySystem == false)
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lost!");
            }
            Console.ReadLine();
        }
    }
}
