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
    builder.EntitySet<SaveTable>("SaveTables");
    builder.EntitySet<ProjectUsedTable>("ProjectUsedTables");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class SaveTablesController : ODataController
    {
        private SmProjectEntities db = new SmProjectEntities();

        // GET: odata/SaveTables
        [EnableQuery]
        public IQueryable<SaveTable> GetSaveTables()
        {
            return db.SaveTables;
        }

        // GET: odata/SaveTables(5)
        [EnableQuery]
        public SingleResult<SaveTable> GetSaveTable([FromODataUri] int key)
        {
            return SingleResult.Create(db.SaveTables.Where(saveTable => saveTable.ProjectID == key));
        }

        // PUT: odata/SaveTables(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<SaveTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SaveTable saveTable = db.SaveTables.Find(key);
            if (saveTable == null)
            {
                return NotFound();
            }

            patch.Put(saveTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaveTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(saveTable);
        }

        // POST: odata/SaveTables
        public IHttpActionResult Post(SaveTable saveTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SaveTables.Add(saveTable);
            db.SaveChanges();

            return Created(saveTable);
        }

        // PATCH: odata/SaveTables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<SaveTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            SaveTable saveTable = db.SaveTables.Find(key);
            if (saveTable == null)
            {
                return NotFound();
            }

            patch.Patch(saveTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaveTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(saveTable);
        }

        // DELETE: odata/SaveTables(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            SaveTable saveTable = db.SaveTables.Find(key);
            if (saveTable == null)
            {
                return NotFound();
            }

            db.SaveTables.Remove(saveTable);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/SaveTables(5)/ProjectUsedTables
        [EnableQuery]
        public IQueryable<ProjectUsedTable> GetProjectUsedTables([FromODataUri] int key)
        {
            return db.SaveTables.Where(m => m.ProjectID == key).SelectMany(m => m.ProjectUsedTables);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaveTableExists(int key)
        {
            return db.SaveTables.Count(e => e.ProjectID == key) > 0;
        }
    }
}