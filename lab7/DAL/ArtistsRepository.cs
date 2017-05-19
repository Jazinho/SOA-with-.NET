using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ArtistsRepository : IArtistsRepository
    {
        MuseumContext db = new MuseumContext();

        public List<Artist> GetAll()
        {
            return db.Artists.ToList();
        }

        public Artist Get(int Id)
        {
            return db.Artists.Find(Id);
        }

        public int Create(Artist Artist)
        {
            var Created = db.Artists.Add(Artist);
            db.SaveChanges();

            return Created.Id;
        }

        public Artist Update(Artist Artist)
        {
            db.Entry(Artist).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Artist;
        }

        public void Delete(int Id)
        {
            var art = db.Artists.Find(Id);
            db.Artists.Remove(art);
            db.SaveChanges();
        }
    }
}
