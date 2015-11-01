using D2DTechSampleApplication.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace D2DTechSampleApplication.Controllers
{
    public class TechniciansController : Controller
    {
        private TechnicianDBContext db = new TechnicianDBContext();

        // GET: Technicians
        public ActionResult Index()
        {
            return View(db.Technicians.ToList());
        }

        // GET: Technicians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technician technician = db.Technicians.Include(t => t.CurrentLocation)
                                                    .Include(t => t.Skills)
                                                    .Include(t => t.WorkTasks)
                                                    .First(t => t.TechnicianId == id);
            if (technician == null)
            {
                return HttpNotFound();
            }
            return View(technician);
        }

        // GET: Technicians/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Technicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Technician technician, Location location, Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.Skills.Add((Skill)skill);
                db.SaveChanges();

                //var skill = db.Skills.First((System.Linq.Expressions.Expression<System.Func<Skill, bool>>)(s => s.Name == skill.Name && s.SkillType == skill.SkillType));
                technician.CurrentLocation = location;
                technician.Skills = new List<Skill>();
                technician.Skills.Add(skill);
                db.Technicians.Add(technician);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(technician);
        }

        // GET: Technicians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technician technician = db.Technicians.Include(t => t.CurrentLocation)
                                                    .Include(t => t.Skills)
                                                    .Include(t => t.WorkTasks)
                                                    .First(t => t.TechnicianId == id);
            if (technician == null)
            {
                return HttpNotFound();
            }
            return View(technician);
        }

        // POST: Technicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Technician technician, Location location, Skill skill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(technician).State = EntityState.Modified;
                db.Entry(location).State = EntityState.Modified;
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(technician);
        }

        // GET: Technicians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Technician technician = db.Technicians.Find(id);
            if (technician == null)
            {
                return HttpNotFound();
            }
            return View(technician);
        }

        // POST: Technicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Technician technician = db.Technicians.Find(id);
            db.Technicians.Remove(technician);
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
