using smprojectMVC3.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace smprojectMVC3.Controllers
{
    public class ProjectUsedTablesMVCController : Controller
    {
        private SmProjectEntities db = new SmProjectEntities();

        // GET: ProjectUsedTablesMVC
        public ActionResult Index()
        {
            var projectUsedTables = db.ProjectUsedTables.Include(p => p.LegoLibrary).Include(p => p.SaveTable);
            return View(projectUsedTables.ToList());
        }

        // GET: ProjectUsedTablesMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(id);
            if (projectUsedTable == null)
            {
                return HttpNotFound();
            }
            return View(projectUsedTable);
        }

        // GET: ProjectUsedTablesMVC/Create
        public ActionResult Create()
        {
            ViewBag.LegoID = new SelectList(db.LegoLibraries, "LegoID", "Name");
            ViewBag.ProjectID = new SelectList(db.SaveTables, "ProjectID", "ProjectName");
            return View();
        }

        // POST: ProjectUsedTablesMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SerialNumber,ProjectID,ProjectName,LegoID,Name,Weight,Type,ProjectUsed")] ProjectUsedTable projectUsedTable)
        {
            if (ModelState.IsValid)
            {
                db.ProjectUsedTables.Add(projectUsedTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LegoID = new SelectList(db.LegoLibraries, "LegoID", "Name", projectUsedTable.LegoID);
            ViewBag.ProjectID = new SelectList(db.SaveTables, "ProjectID", "ProjectName", projectUsedTable.ProjectID);
            return View(projectUsedTable);
        }

        // GET: ProjectUsedTablesMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(id);
            if (projectUsedTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.LegoID = new SelectList(db.LegoLibraries, "LegoID", "Name", projectUsedTable.LegoID);
            ViewBag.ProjectID = new SelectList(db.SaveTables, "ProjectID", "ProjectName", projectUsedTable.ProjectID);
            return View(projectUsedTable);
        }

        // POST: ProjectUsedTablesMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SerialNumber,ProjectID,ProjectName,LegoID,Name,Weight,Type,ProjectUsed")] ProjectUsedTable projectUsedTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectUsedTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LegoID = new SelectList(db.LegoLibraries, "LegoID", "Name", projectUsedTable.LegoID);
            ViewBag.ProjectID = new SelectList(db.SaveTables, "ProjectID", "ProjectName", projectUsedTable.ProjectID);
            return View(projectUsedTable);
        }

        // GET: ProjectUsedTablesMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(id);
            if (projectUsedTable == null)
            {
                return HttpNotFound();
            }
            return View(projectUsedTable);
        }

        // POST: ProjectUsedTablesMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectUsedTable projectUsedTable = db.ProjectUsedTables.Find(id);
            db.ProjectUsedTables.Remove(projectUsedTable);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}