using smprojectMVC3.Models;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.OData.Routing;

namespace smprojectMVC3.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using smprojectMVC3.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ProjectUsedTable>("ProjectUsedTables");
    builder.EntitySet<LegoLibrary>("LegoLibraries");
    builder.EntitySet<SaveTable>("SaveTables");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class ProjectUsedTablesController : ODataController
    {
        private SmProjectEntities db = new SmProjectEntities();

        // GET: odata/ProjectUsedTables
        [EnableQuery]
        public IQueryable<ProjectUsedTable> GetProjectUsedTables()
        {
            return db.ProjectUsedTables;
        }

        // GET: odata/ProjectUsedTables(5)
        [EnableQuery]
        public SingleResult<ProjectUsedTable> GetProjectUsedTable([FromODataUri] int key)
        {
            return SingleResult.Create(db.ProjectUsedTables.Where(projectUsedTable => projectUsedTable.SerialNumber == key));
        }

        // PUT: odata/ProjectUsedTables(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<ProjectUsedTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(key);
            if (projectUsedTable == null)
            {
                return NotFound();
            }

            patch.Put(projectUsedTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectUsedTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(projectUsedTable);
        }

        // POST: odata/ProjectUsedTables
        public IHttpActionResult Post(ProjectUsedTable projectUsedTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProjectUsedTables.Add(projectUsedTable);
            db.SaveChanges();

            return Created(projectUsedTable);
        }

        [EnableQuery]
        [ODataRoute("Products(ProjectID={key1},LegoID={key2})")]
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key1, [FromODataUri] string key2, Delta<ProjectUsedTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(key1, key2);
            if (projectUsedTable == null)
            {
                return NotFound();
            }

            patch.Patch(projectUsedTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!ProjectUsedTableExists(key1))
                //{
                //    return NotFound();
                //}
                //if
                //{
                //    throw;
                //}
            }

            return Updated(projectUsedTable);
        }




        // PATCH: odata/ProjectUsedTables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ProjectUsedTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(key);
            if (projectUsedTable == null)
            {
                return NotFound();
            }

            patch.Patch(projectUsedTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectUsedTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(projectUsedTable);
        }



  










        // DELETE: odata/ProjectUsedTables(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(key);
            if (projectUsedTable == null)
            {
                return NotFound();
            }

            db.ProjectUsedTables.Remove(projectUsedTable);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ProjectUsedTables(5)/LegoLibrary
        [EnableQuery]
        public SingleResult<LegoLibrary> GetLegoLibrary([FromODataUri] int key)
        {
            return SingleResult.Create(db.ProjectUsedTables.Where(m => m.SerialNumber == key).Select(m => m.LegoLibrary));
        }

        // GET: odata/ProjectUsedTables(5)/SaveTable
        [EnableQuery]
        public SingleResult<SaveTable> GetSaveTable([FromODataUri] int key)
        {
            return SingleResult.Create(db.ProjectUsedTables.Where(m => m.SerialNumber == key).Select(m => m.SaveTable));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectUsedTableExists(int key)
        {
            return db.ProjectUsedTables.Count(e => e.SerialNumber == key) > 0;
        }
    }
}