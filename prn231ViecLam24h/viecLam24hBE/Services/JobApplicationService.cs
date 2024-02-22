using viecLam24hBE.Models;
using viecLam24hBE.Params;

namespace viecLam24hBE.Services
{
    public interface JobApplicationService
    {
        List<JobApplication> GetJobApplications();

        List<JobApplication> FilterJobApplications(FilterJobApplicationParam jobApplicationParam);

        void ChangeStatus(int id);

        public void DeleteJobApplication(int id);

        Task InsertJobApplication(int appProfileId, int jobPostId);
    }
}
