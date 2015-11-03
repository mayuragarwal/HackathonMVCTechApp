using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using D2DTechSampleApplication.Models;

namespace D2DTechSampleApplication.Controllers
{
    public class TechnicianController : Controller
    {
        private TechnicianDBContext db = new TechnicianDBContext();

        // GET: WorkTasks
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var technician = db.Technicians.Include(t => t.WorkTasks)
                                .Include(t=>t.CurrentLocation)
                                .FirstOrDefault(t => t.TechnicianId == id);
            if (technician == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.TechnicianId = id.Value;
            return View(technician);
        }

        // GET: WorkTasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTask workTask = db.WorkTasks.Include(wt=>wt.Customer)
                                            .Include(wt=>wt.Customer.CustomerLocation)
                                            .First(wt=>wt.WorkTaskId == id);
            if (workTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerName = workTask.Customer.DisplayName;
            ViewBag.CustomerLocation = workTask.Customer.CustomerLocation.ToString();
            ViewBag.TechnicianId = workTask.TechId;
            return View(workTask);
        }

        // GET: WorkTasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTask workTask = db.WorkTasks.First(wt => wt.WorkTaskId == id);
            if (workTask == null)
            {
                return HttpNotFound();
            }
            ViewBag.TechnicianId = workTask.TechId;
            return View(workTask);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkTaskId,StartDateTime,EndDateTime,WorkStatus")] WorkTask workTask)
        {
            if (ModelState.IsValid)
            {
                var myWork = db.WorkTasks.Find(workTask.WorkTaskId);
                myWork.StartDateTime = workTask.StartDateTime;
                myWork.EndDateTime = workTask.EndDateTime;
                myWork.WorkStatus = workTask.WorkStatus;
                db.Entry(myWork).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index", new {id = myWork.TechId });
            }
            ViewBag.TechnicianId = workTask.TechId;
            return View(workTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Location currentLocation, Technician tech)
        {
            var technician = db.Technicians.Find(tech.TechnicianId);
            technician.CurrentLocation = currentLocation;
            db.Entry(technician).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", new { id = tech.TechnicianId });
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
