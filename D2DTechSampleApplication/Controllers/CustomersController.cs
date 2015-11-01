using D2DTechSampleApplication.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace D2DTechSampleApplication.Controllers
{
    public class CustomersController : Controller
    {
        private TechnicianDBContext db = new TechnicianDBContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.Include(c=>c.CustomerLocation).ToList());
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer, Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);

                customer.CustomerLocation = location;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
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
