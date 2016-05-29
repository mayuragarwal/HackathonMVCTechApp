using System.Data.Entity;
using System.Linq;
using D2DTechSampleApplication.Models;

namespace D2DTechSampleApplication.Code
{
    public class CustomerRepository : IRepository<Customer>
    {
        readonly TechnicianDBContext _dbContext;

        public CustomerRepository()
        {
            _dbContext = new TechnicianDBContext();
        }

        public IQueryable<Customer> Retrieve()
        {
            return _dbContext.Customers;
        }

        public void Update(Customer customer)
        {
            _dbContext.Entry(customer).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Create(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }
    }
}