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
    public class StoresController : ODataController
    {
        GamesContext db = new GamesContext();

        [EnableQuery]
        public IQueryable<Store> Get()
        {
            return db.Stores;
        }

        [EnableQuery]
        public SingleResult<Store> Get([FromODataUri] int key)
        {
            IQueryable<Store> result = db.Stores.Where(g => g.Id == key);
            return SingleResult.Create(result);
        }

        public async Task<IHttpActionResult> Post(Store Store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stores.Add(Store);
            await db.SaveChangesAsync();
            return Created(Store);
        }

        public async Task<IHttpActionResult> Put([FromODataUri] int key, Store Store)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Store.Id)
            {
                return BadRequest();
            }
            db.Entry(Store).State = System.Data.Entity.EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(Store);
        }

        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var Store = await db.Stores.FindAsync(key);
            if (Store == null)
                return NotFound();

            db.Stores.Remove(Store);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool StoreExists(int key)
        {
            return db.Stores.Any(g => g.Id == key);
        }
    }
}
