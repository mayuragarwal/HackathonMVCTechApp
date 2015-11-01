using D2DTechSampleApplication.Models;
using System.Linq;

namespace D2DTechSampleApplication.Code
{
    public class TechnicianRepository
    {
        TechnicianDBContext context;

        public TechnicianRepository()
        {
            context = new TechnicianDBContext();
        }
         
        public IQueryable<Technician> GetTechnicians()
        {
            return context.Technicians;
        }

        public Technician GetTechnicianByUserName(string userName)
        {
            return context.Technicians.FirstOrDefault(tech => tech.UserName == userName);
        }
    }
}