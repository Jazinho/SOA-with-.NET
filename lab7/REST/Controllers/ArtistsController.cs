using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace REST.Controllers
{
    public class ArtistsController : ApiController
    {
        private IArtistsRepository repo;

        public ArtistsController(IArtistsRepository _repo)
        {
            this.repo = _repo;
        }

        public ArtistsController() { }

        // GET api/Artists
        public IQueryable<Artist> GetArtists()
        {
            return repo.GetAll().AsQueryable();
        }

        // GET api/Artists/5
        [ResponseType(typeof(Artist))]
        public IHttpActionResult GetArtist(int id)
        {
            Artist artist = repo.Get(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        // PUT api/Artists/5
        [ResponseType(typeof(Artist))]
        public IHttpActionResult PutArtist(int id, Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artist.Id)
            {
                return BadRequest();
            }

            repo.Update(artist);

            return Ok(artist);
        }

        // POST api/Artists
        [ResponseType(typeof(Artist))]
        public IHttpActionResult PostArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Create(artist);

            return CreatedAtRoute("DefaultApi", new { id = artist.Id }, artist);
        }

        // DELETE api/Artists/5
        [ResponseType(typeof(Artist))]
        public IHttpActionResult DeleteArtist(int id)
        {
            Artist artist = repo.Get(id);
            if (artist == null)
            {
                return NotFound();
            }

            repo.Delete(id);

            return Ok(artist);
        }
    }
}