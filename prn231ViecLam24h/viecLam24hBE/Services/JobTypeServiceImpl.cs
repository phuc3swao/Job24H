using viecLam24hBE.Commons;
using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public class JobTypeServiceImpl : JobTypeService
    {
        private readonly MyDbContext _context;

        public JobTypeServiceImpl(MyDbContext context)
        {
            _context = context;
        }

        public JobType GetJobTypeById(int id)
        {
            try
            {
                return _context.JobTypes.FirstOrDefault(j => j.Id == id);
            } catch (Exception ex) {
                Console.WriteLine(ex);

                return null;
            }
        }

        public List<JobType> GetJobTypes()
        {
            return _context.JobTypes.ToList();
        }
    }
}
