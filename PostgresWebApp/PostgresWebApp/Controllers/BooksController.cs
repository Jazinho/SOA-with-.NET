using PostgresWebApp.DAL;
using PostgresWebApp.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace PostgresWebApp.Controllers
{
    public class BooksController : ApiController
    {
        StoreContext _storeContext = new StoreContext();

        public IQueryable<Book> GetBooks()
        {
            return _storeContext.Books;
        }

        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int Id)
        {
            var res = _storeContext.Books.Find(Id);
            if (res == null) return NotFound();

            return Ok(res);
        }

        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book Book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _storeContext.Books.Add(Book);
            _storeContext.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Book.Id }, Book);
        }

        public IHttpActionResult PutBook(int Id, Book Book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(Id != Book.Id)
            {
                return BadRequest();
            }

            _storeContext.Entry(Book).State = EntityState.Modified;

            try
            {
                _storeContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int Id)
        {
            Book Book = _storeContext.Books.Find(Id);

            if(Book == null)
            {
                return BadRequest();
            }

            _storeContext.Books.Remove(Book);
            _storeContext.SaveChanges();

            return Ok(Book);
        }

        public bool BookExists(int Id)
        {
            return _storeContext.Books.Count(b => b.Id == Id) > 0;
        }
    }
}