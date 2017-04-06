using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace REST_Service.Controllers
{
    public class BooksController : ApiController
    {
        BooksRepository.Service1 _booksRepository;

        BooksController()
        {
            _booksRepository = new BooksRepository.Service1();
        }

        //GET api/books/5
        public Book Get(int id)
        {
            return _booksRepository.Get(id);
        }

        //GET api/books
        public List<Book> Get()
        {
            return _booksRepository.GetAll();
        }

        // GET api/books&search=title1
        public List<Book> Get([FromUri(Name = "search")]string param)
        {
            List<Book> data = _booksRepository.GetAll();
            var res = data.FindAll(x => x.BookTitle.Contains(param));
            return res;
        }

        // POST api/books
        public int Post([FromBody]Book Book)
        {
            return _booksRepository.Insert(Book);
        }

        // PUT api/books/5
        public void Put(int id, [FromBody]Book Book)
        {
            _booksRepository.Update(Book);
        }

        // DELETE api/books/5
        public void Delete(int id)
        {
            _booksRepository.Delete(id);
        }

        // GET: Books
        /*public ActionResult Index()
        {
            //return View();
        }*/
    }
}