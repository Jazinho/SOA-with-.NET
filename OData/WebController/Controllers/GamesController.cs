using Backend;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace WebController.Controllers
{
    public class GamesController : ODataController
    {
        GamesContext db = new GamesContext();

        [EnableQuery]
        public IQueryable<Game> Get()
        {
            return db.Games;
        }

        [EnableQuery]
        public SingleResult<Game> Get([FromODataUri] int key)
        {
            IQueryable<Game> result = db.Games.Where(g => g.Id == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Game Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(Game);
            await db.SaveChangesAsync();
            return Created(Game);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int key, Game Game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(key != Game.Id)
            {
                return BadRequest();
            }
            db.Entry(Game).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(Game);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var game = await db.Games.FindAsync(key);
            if (game == null)
                return NotFound();

            db.Games.Remove(game);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool GameExists(int key)
        {
            return db.Games.Any(g => g.Id == key);
        }
    }
}
