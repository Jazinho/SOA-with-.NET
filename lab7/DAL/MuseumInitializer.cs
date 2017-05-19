using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MuseumInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MuseumContext>
    {
        protected override void Seed(MuseumContext context)
        {
            var artists = new List<Artist>
            {
                new Artist() {ArtistName = "Vincent", ArtistSurname = "van Gogh" },
                new Artist() {ArtistName = "Leon", ArtistSurname = "Wyczolkowski" }
            };
            artists.ForEach(a => context.Artists.Add(a));
            context.SaveChanges();

            var paintings = new List<Painting>
            {
                new Painting() {Title = "Mona Lisa", Year = 1880 },
                new Painting() {Title = "Whistler's Mother", Year=1905 }
            };
            paintings.ForEach(p => context.Paintings.Add(p));
            context.SaveChanges();
        }
    }
}
