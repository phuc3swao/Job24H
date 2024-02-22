using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public interface JobPostService
    {
        bool AddJobPost(JobPost jobPost);

        bool EditJobPost(JobPost jobPost);

        List<JobPost> GetJobsPosts();

        JobPost GetJobPostById(int id);

        void UpdateDeadline(int id, DateTime newDealine);

        public void DeleteJobPost(int id);

        void ChangeStatus(int id);
    }
}
