using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LitePaintingsRepository : IPaintingsRepository
    {
        private readonly string path = @"C:\Users\Jan\Documents\Visual Studio 2015\Projects\lab7\DAL\museum.db";

        public List<Painting> GetAll()
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Painting>("paintings");

                return repo.FindAll().ToList();
            }
        }

        public Painting Get(int Id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Painting>("paintings");

                return repo.FindById(Id);
            }
        }

        public int Create(Painting Painting)
        {
            using (var db = new LiteDatabase(path)) {
                var repo = db.GetCollection<Painting>("paintings");

                repo.Insert(Painting);

                return Painting.Id;
            }
        }

        public Painting Update(Painting Painting)
        {
            using(var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Painting>("paintings");

                if (repo.Update(Painting))
                    return Painting;
                else
                    return null;
            }
        }

        public void Delete(int Id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Painting>("paintings");

                repo.Delete(Id);
            }
        }
    }
}
