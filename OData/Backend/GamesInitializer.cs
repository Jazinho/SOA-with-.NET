using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class GamesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GamesContext>
    {
        protected override void Seed(GamesContext context)
        {
            var Games = new List<Game>()
            {
                new Game() {Id = 1, Title="Starcraft", Year=1999, CreatorCompany = "Blizzard Entertaiment", AgeRate = 123 },
                new Game() {Id = 2, Title="Stronghold", Year=2003, CreatorCompany = "Firefly Studios", AgeRate = 321 },
                new Game() {Id = 3, Title="The Sims", Year=2000, CreatorCompany = "Electronic Arts", AgeRate = 999 }
            };
            Games.ForEach(g => context.Games.Add(g));
            context.SaveChanges();

            var Stores = new List<Store>()
            {
                new Store() { Id = 1, Name = "Gry Świata", Address="Kraków ul.Wadowicka 59" },
                new Store() { Id = 2, Name = "Sweet Game o'Mine", Address="Warszawa ul.Krakowskie Przedmieście 33" },
                new Store() { Id = 3, Name = "Game'o'tronic", Address="Kraków ul.Lipska 40" }
            };
            Stores.ForEach(s => context.Stores.Add(s));
            context.SaveChanges();
        }
    }
}
