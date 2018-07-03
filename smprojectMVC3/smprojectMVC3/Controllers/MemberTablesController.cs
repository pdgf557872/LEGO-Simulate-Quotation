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
    WebApiConfig 類別可能需要其他變更以新增此控制器的路由，請將這些陳述式合併到 WebApiConfig 類別的 Register 方法。注意 OData URL 有區分大小寫。

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using smprojectMVC3.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<MemberTable>("MemberTables");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class MemberTablesController : ODataController
    {
        private SmProjectEntities db = new SmProjectEntities();

        // GET: odata/MemberTables
        [EnableQuery]
        public IQueryable<MemberTable> GetMemberTables()
        {
            return db.MemberTables;
        }

        // GET: odata/MemberTables(5)
        [EnableQuery]
        public SingleResult<MemberTable> GetMemberTable([FromODataUri] int key)
        {
            return SingleResult.Create(db.MemberTables.Where(memberTable => memberTable.MemberID == key));
        }

        // PUT: odata/MemberTables(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<MemberTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MemberTable memberTable = db.MemberTables.Find(key);
            if (memberTable == null)
            {
                return NotFound();
            }

            patch.Put(memberTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(memberTable);
        }

        // POST: odata/MemberTables
        public IHttpActionResult Post(MemberTable memberTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MemberTables.Add(memberTable);
            db.SaveChanges();

            return Created(memberTable);
        }

        // PATCH: odata/MemberTables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<MemberTable> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MemberTable memberTable = db.MemberTables.Find(key);
            if (memberTable == null)
            {
                return NotFound();
            }

            patch.Patch(memberTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(memberTable);
        }

        // DELETE: odata/MemberTables(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            MemberTable memberTable = db.MemberTables.Find(key);
            if (memberTable == null)
            {
                return NotFound();
            }

            db.MemberTables.Remove(memberTable);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MemberTableExists(int key)
        {
            return db.MemberTables.Count(e => e.MemberID == key) > 0;
        }
    }
}