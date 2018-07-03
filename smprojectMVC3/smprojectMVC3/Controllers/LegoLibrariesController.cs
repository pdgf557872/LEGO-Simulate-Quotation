using smprojectMVC3.Models;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;

namespace smprojectMVC3.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using smprojectMVC3.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<LegoLibrary>("LegoLibraries");
    builder.EntitySet<ProjectUsedTable>("ProjectUsedTables");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class LegoLibrariesController : ODataController
    {
        private SmProjectEntities db = new SmProjectEntities();

        // GET: odata/LegoLibraries
        [EnableQuery]
        public IQueryable<LegoLibrary> GetLegoLibraries()
        {
            return db.LegoLibraries;
        }

        // GET: odata/LegoLibraries(5)
        [EnableQuery]
        public SingleResult<LegoLibrary> GetLegoLibrary([FromODataUri] int key)
        {
            return SingleResult.Create(db.LegoLibraries.Where(legoLibrary => legoLibrary.LegoID == key));
        }

        // PUT: odata/LegoLibraries(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<LegoLibrary> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LegoLibrary legoLibrary = db.LegoLibraries.Find(key);
            if (legoLibrary == null)
            {
                return NotFound();
            }

            patch.Put(legoLibrary);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LegoLibraryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(legoLibrary);
        }

        // POST: odata/LegoLibraries
        public IHttpActionResult Post(LegoLibrary legoLibrary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LegoLibraries.Add(legoLibrary);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LegoLibraryExists(legoLibrary.LegoID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(legoLibrary);
        }

        // PATCH: odata/LegoLibraries(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<LegoLibrary> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LegoLibrary legoLibrary = db.LegoLibraries.Find(key);
            if (legoLibrary == null)
            {
                return NotFound();
            }

            patch.Patch(legoLibrary);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LegoLibraryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(legoLibrary);
        }

        // DELETE: odata/LegoLibraries(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            LegoLibrary legoLibrary = db.LegoLibraries.Find(key);
            if (legoLibrary == null)
            {
                return NotFound();
            }

            db.LegoLibraries.Remove(legoLibrary);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LegoLibraries(5)/ProjectUsedTables
        [EnableQuery]
        public IQueryable<ProjectUsedTable> GetProjectUsedTables([FromODataUri] int key)
        {
            return db.LegoLibraries.Where(m => m.LegoID == key).SelectMany(m => m.ProjectUsedTables);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LegoLibraryExists(int key)
        {
            return db.LegoLibraries.Count(e => e.LegoID == key) > 0;
        }
    }
}