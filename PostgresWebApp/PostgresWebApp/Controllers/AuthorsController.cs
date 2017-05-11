using PostgresWebApp.DAL;
using PostgresWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace PostgresWebApp.Controllers
{
    public class AuthorsController : ApiController
    {
        StoreContext _storeContext = new StoreContext();

        public IQueryable<Author> GetAuthors()
        {
            return _storeContext.Authors;
        }

        [ResponseType(typeof(Author))]
        public IHttpActionResult GetAuthor(int Id)
        {
            var res = _storeContext.Authors.Find(Id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [ResponseType(typeof(Author))]
        public IHttpActionResult PostAuthor(Author Author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _storeContext.Authors.Add(Author);
            _storeContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Author.Id }, Author);
        }

        [ResponseType(typeof(Author))]
        public IHttpActionResult PutAuthor(int Id, Author Author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (Id != Author.Id) return BadRequest();

            _storeContext.Entry(Author).State = System.Data.Entity.EntityState.Modified;

            try
            {
                _storeContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Id)) return NotFound();
                else throw;
            }

            return Ok(Author);
        }

        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int Id)
        {
            Author Author = _storeContext.Authors.Find(Id);

            if (Author == null) return BadRequest();

            _storeContext.Authors.Remove(Author);
            _storeContext.SaveChanges();

            return Ok(Author);
        }

        private bool AuthorExists(int Id)
        {
            return _storeContext.Authors.Count(a => a.Id == Id) > 0;
        }
    }
}