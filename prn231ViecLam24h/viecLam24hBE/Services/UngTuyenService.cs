using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public interface UngTuyenService
    {
        // Common : No Login
        // 1.HomePage
        Task<List<JobPost>> JobPosts();

        Task<List<JobPost>> JobPosts(string profileName, string jobType, string jobLocation);

        Task<List<Tuple<string, int>>> ListLocation();

        Task<List<JobPost>> UrgentJobs();
        // 2.Filter
        Task<List<Tuple<string, string>>> Exp_Level();

        Task<List<Tuple<string, double, double>>> Salary_Level();

        Task<List<JobPost>> JobPosts( string exp_level, string salary, string jobType, string jobLocation);

        Task<List<JobPost>> JobPosts(ref List<JobPost> jobPosts, int pageIndex);

        // 3.DetailPage
        Task<JobPost> JobPost(int id);

        // 4.SubmitCV
        Task SubmitCV(int userid, string filename, int jobpostId);

        // 5.QuanLy
        Task TaoHoSo(ApplicantProfile applicantProfile);

        Task<List<ApplicantProfile>> ListHoSo(int userid);

        Task<ApplicantProfile> GetHoSo(int id);

        Task XoaHoSo(int profile);
    }
}
