using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace REST_Service.Controllers
{
    public class AuthorsController : ApiController
    {
        private AuthorsRepository.Service1 _authorsRepository;

        AuthorsController()
        {
            _authorsRepository = new AuthorsRepository.Service1();
        }

        public List<Author> GetAll()
        {
            return _authorsRepository.GetAll();
        }

        public Author Get(int id)
        {
            return _authorsRepository.Get(id);
        }

        public int Post([FromBody]Author Author)
        {
            return _authorsRepository.Insert(Author);
        }

        public void Put(int id, Author Author)
        {
            _authorsRepository.Update(Author);
        }

        public void Delete(int id)
        {
            _authorsRepository.Delete(id);
        }

        // GET: Authors
        /*public ActionResult Index()
        {
            return View();
        }*/
    }
}