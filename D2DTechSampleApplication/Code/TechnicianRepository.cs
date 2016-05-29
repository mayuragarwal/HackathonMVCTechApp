using System.Data.Entity;
using D2DTechSampleApplication.Models;
using System.Linq;

namespace D2DTechSampleApplication.Code
{
    public class TechnicianRepository : IRepository<Technician>
    {
        readonly TechnicianDBContext _dbContext;

        public TechnicianRepository(DbContext dbContext)
        {
            _dbContext = dbContext as TechnicianDBContext;
        }
         
        public IQueryable<Technician> Retrieve()
        {
            return _dbContext.Technicians;
        }

        public void Update(Technician technician)
        {
            _dbContext.Entry(technician).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Create(Technician technician)
        {
            _dbContext.Technicians.Add(technician);
            _dbContext.SaveChanges();
        }
    }
}