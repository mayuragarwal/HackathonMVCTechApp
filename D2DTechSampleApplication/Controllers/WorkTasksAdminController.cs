using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using D2DTechSampleApplication.Models;

namespace D2DTechSampleApplication.Controllers
{
    public class WorkTasksAdminController : Controller
    {
        private TechnicianDBContext db = new TechnicianDBContext();

        // GET: WorkTasksAdmin
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var technician = db.Technicians.Include(t => t.WorkTasks)
                                .FirstOrDefault(t => t.TechnicianId == id);
            if (technician == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.TechnicianId = technician.TechnicianId;
            ViewBag.TechnicianDisplayName = technician.DisplayName;
            return View(technician.WorkTasks);
        }

        // GET: WorkTasksAdmin/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customers = db.Customers.ToList();
            ViewBag.CustomerListItems = from customer in customers
                                                            select new SelectListItem()
                                                            {
                                                                Text = customer.DisplayName,
                                                                Value = customer.CustomerId.ToString()
                                                            };
            CompleteWorkTaskVM completeWorkTaskVM = new CompleteWorkTaskVM()
            {
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(2)
            };
            completeWorkTaskVM.TechnicianId = id.Value;
            return View(completeWorkTaskVM);
        }

        // POST: WorkTasksAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkTaskId,StartDateTime,EndDateTime,WorkStatus,CustomerId,CustomerName,TechnicianId")] CompleteWorkTaskVM completeWorkTaskVM)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Find(completeWorkTaskVM.CustomerId);
                var workTask = new WorkTask()
                {
                    WorkTaskId = completeWorkTaskVM.WorkTaskId,
                    StartDateTime = completeWorkTaskVM.StartDateTime,
                    EndDateTime = completeWorkTaskVM.EndDateTime,
                    WorkStatus = completeWorkTaskVM.WorkStatus,
                    Customer = customer,
                    TechId = completeWorkTaskVM.TechnicianId
                };
                db.WorkTasks.Add(workTask);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = completeWorkTaskVM.TechnicianId });
            }

            return View(completeWorkTaskVM);
        }

        // GET: WorkTasksAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workTask = db.WorkTasks.Include(wt=>wt.Customer)
                                        .FirstOrDefault(wt=>wt.WorkTaskId == id);
            if (workTask == null)
            {
                return HttpNotFound();
            }

            var completeWorkTaskVM = new CompleteWorkTaskVM()
            {
                WorkTaskId = workTask.WorkTaskId,
                StartDateTime = workTask.StartDateTime,
                EndDateTime = workTask.EndDateTime,
                WorkStatus = workTask.WorkStatus,
                CustomerId = workTask.Customer.CustomerId,
                CustomerName = workTask.Customer.DisplayName,
                TechnicianId = workTask.TechId
            };
            return View(completeWorkTaskVM);
        }

        // GET: WorkTasksAdmin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workTask = db.WorkTasks.Include(wt => wt.Customer)
                                        .FirstOrDefault(wt => wt.WorkTaskId == id);
            if (workTask == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customers = db.Customers.ToList();
            ViewBag.CustomerListItems = from customer in customers
                                        select new SelectListItem()
                                        {
                                            Text = customer.DisplayName,
                                            Value = customer.CustomerId.ToString()
                                        };
            var completeWorkTaskVM = new CompleteWorkTaskVM()
            {
                WorkTaskId = workTask.WorkTaskId,
                StartDateTime = workTask.StartDateTime,
                EndDateTime = workTask.EndDateTime,
                WorkStatus = workTask.WorkStatus,
                CustomerId = workTask.Customer.CustomerId,
                CustomerName = workTask.Customer.DisplayName,
                TechnicianId = workTask.TechId
            };
            return View(completeWorkTaskVM);
        }

        // POST: WorkTasksAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkTaskId,StartDateTime,EndDateTime,WorkStatus,CustomerId,TechnicianId")] CompleteWorkTaskVM completeWorkTaskVM)
        {
            if (ModelState.IsValid)
            {
                var workTask = db.WorkTasks.Find(completeWorkTaskVM.WorkTaskId);

                workTask.StartDateTime = completeWorkTaskVM.StartDateTime;
                workTask.EndDateTime = completeWorkTaskVM.EndDateTime;
                workTask.WorkStatus = completeWorkTaskVM.WorkStatus;
                workTask.CustomerId = completeWorkTaskVM.CustomerId;
                
                db.Entry(workTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = completeWorkTaskVM.TechnicianId });
            }
            return View(completeWorkTaskVM);
        }

        // GET: WorkTasksAdmin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workTask = db.WorkTasks.Include(wt => wt.Customer)
                                        .FirstOrDefault(wt => wt.WorkTaskId == id);
            if (workTask == null)
            {
                return HttpNotFound();
            }

            var completeWorkTaskVM = new CompleteWorkTaskVM()
            {
                WorkTaskId = workTask.WorkTaskId,
                StartDateTime = workTask.StartDateTime,
                EndDateTime = workTask.EndDateTime,
                WorkStatus = workTask.WorkStatus,
                CustomerId = workTask.Customer.CustomerId,
                CustomerName = workTask.Customer.DisplayName,
                TechnicianId = workTask.TechId
            };
            return View(completeWorkTaskVM);
        }

        // POST: WorkTasksAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "WorkTaskId,TechnicianId")] CompleteWorkTaskVM completeWorkTaskVM)
        {
            var workTask = db.WorkTasks.Find(completeWorkTaskVM.WorkTaskId);
            if (workTask == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.WorkTasks.Remove(workTask);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = completeWorkTaskVM.TechnicianId });
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
