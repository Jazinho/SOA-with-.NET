using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PaintingsRepository : IPaintingsRepository
    {
        MuseumContext db = new MuseumContext();

        public List<Painting> GetAll()
        {
            return db.Paintings.ToList();
        }

        public Painting Get(int Id)
        {
            return db.Paintings.Find(Id);
        }

        public int Create(Painting Painting)
        {
            var Created = db.Paintings.Add(Painting);
            db.SaveChanges();

            return Created.Id;
        }

        public Painting Update(Painting Painting)
        {
            db.Entry(Painting).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Painting;
        }

        public void Delete(int Id)
        {
            var Deleted = db.Paintings.Find(Id);
            db.Paintings.Remove(Deleted);
            db.SaveChanges();
        }
    }
}
