using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LiteArtistsRepository : IArtistsRepository
    {
        private readonly string path = @"C:\Users\Jan\Documents\Visual Studio 2015\Projects\lab7\DAL\museum.db";

        public List<Artist> GetAll()
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Artist>("artists");

                return repo.FindAll().ToList();
            }
        }

        public Artist Get(int Id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Artist>("artists");

                return repo.FindById(Id);
            }
        }

        public int Create(Artist Artist)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Artist>("artists");

                repo.Insert(Artist);

                return Artist.Id;
            }
        }

        public Artist Update(Artist Artist)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Artist>("artists");

                if (repo.Update(Artist))
                    return Artist;
                else
                    return null;
            }
        }

        public void Delete(int Id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Artist>("artists");

                repo.Delete(Id);
            }
        }
    }
}
