using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public interface JobTypeService
    {
        public List<JobType> GetJobTypes();

        public JobType GetJobTypeById(int id);

        
    }
}
