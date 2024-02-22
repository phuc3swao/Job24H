using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public class UngTuyenServiceImpl : UngTuyenService
    {
        public async Task<List<Tuple<string, string>>> Exp_Level() => UngTuyenDAO.getListLevel();

        public async Task<List<Tuple<string, double, double>>> Salary_Level() => UngTuyenDAO.getListSalary();

        public async Task<List<JobPost>> JobPosts() => UngTuyenDAO.getAllJobPosts();

        public async Task<List<JobPost>> JobPosts(string jobName, string jobType, string jobLocation) => UngTuyenDAO.searchJobPosts(jobName, jobType, jobLocation);

        public async Task<List<JobPost>> JobPosts(string exp_level, string salary, string jobType, string jobLocation) => UngTuyenDAO.jobPostsFilter(exp_level, salary, jobType, jobLocation);

        public Task<List<JobPost>> JobPosts(ref List<JobPost> jobPosts, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<List<JobPost>> UrgentJobs()
        {
            throw new NotImplementedException();
        }

        public async Task<JobPost> JobPost(int id) => UngTuyenDAO.GetDetailJobPost(id);

        public async Task SubmitCV(int userid, string filename, int jobpostId) => UngTuyenDAO.SubmitCv(userid, filename, jobpostId);

        public async Task TaoHoSo(ApplicantProfile applicantProfile) => UngTuyenDAO.InsertApplicantProfile(applicantProfile);

        public async Task<List<ApplicantProfile>> ListHoSo(int userid) => UngTuyenDAO.ListHoSo(userid);

        public async Task<ApplicantProfile> GetHoSo(int id) => UngTuyenDAO.GetHoSo(id);

        public async Task XoaHoSo(int profileid) => UngTuyenDAO.DeleteHoSo(profileid);

        public async Task<List<Tuple<string, int>>> ListLocation() => UngTuyenDAO.ListLocation();
    }

}
