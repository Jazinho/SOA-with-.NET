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

namespace BooksRepository
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private readonly string path = @"C:\Users\Jan\Documents\Visual Studio 2015\Projects\lab5\books.db";

        public List<Book> GetAll()
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Book>("books");
                var res = repo.FindAll();

                return res.ToList();
            }
        }

        public Book Get(int Id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Book>("books");
                var res = repo.FindById(Id);

                return res;
            }
        }

        public int Insert(Book Book)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Book>("books");
                var res = repo.Insert(Book);

                return Book.Id;
            }
        }

        public Book Update(Book Book)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Book>("books");
                if (repo.Update(Book))
                    return Book;
                else
                    return null;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(path))
            {
                var repo = db.GetCollection<Book>("books");
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
