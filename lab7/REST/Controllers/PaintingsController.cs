using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace REST.Controllers
{
    public class PaintingsController : ApiController
    {
        private IPaintingsRepository repo;

        public PaintingsController(IPaintingsRepository _repo)
        {
            this.repo = _repo;
        }

        public PaintingsController() { }

        // GET api/Paintings
        public IQueryable<Painting> GetPaintings()
        {
            return repo.GetAll().AsQueryable();
        }

        // GET api/Paintings/5
        [ResponseType(typeof(Painting))]
        public IHttpActionResult GetPainting(int id)
        {
            Painting Painting = repo.Get(id);
            if (Painting == null)
            {
                return NotFound();
            }

            return Ok(Painting);
        }

        // PUT api/Paintings/5
        [ResponseType(typeof(Painting))]
        public IHttpActionResult PutPainting(int id, Painting Painting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Painting.Id)
            {
                return BadRequest();
            }

            repo.Update(Painting);

            return Ok(Painting);
        }

        // POST api/Paintings
        [ResponseType(typeof(Painting))]
        public IHttpActionResult PostPainting(Painting Painting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(Painting);

            return CreatedAtRoute("DefaultApi", new { id = Painting.Id }, Painting);
        }

        // DELETE api/Paintings/5
        [ResponseType(typeof(Painting))]
        public IHttpActionResult DeletePainting(int id)
        {
            Painting Painting = repo.Get(id);
            if (Painting == null)
            {
                return NotFound();
            }

            repo.Delete(id);

            return Ok(Painting);
        }
    }
}