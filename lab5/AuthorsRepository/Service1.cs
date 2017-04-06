using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AuthorsRepository
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private readonly string path = @"C:\Users\Jan\Documents\Visual Studio 2015\Projects\lab5\authors.db";

        public List<Author> GetAll()
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Author>("authors");
                var res = repo.FindAll();

                return res.ToList();
            }
        }

        public Author Get(int Id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Author>("authors");
                var res = repo.FindById(Id);

                return res;
            }
        }

        public int Insert(Author Author)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Author>("authors");
                var res = repo.Insert(Author);

                return Author.Id;
            }
        }

        public Author Update(Author Author)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Author>("authors");
                if (repo.Update(Author))
                    return Author;
                else
                    return null;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Author>("authors");
                var res = repo.Delete(id);

                return res;
            }
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
