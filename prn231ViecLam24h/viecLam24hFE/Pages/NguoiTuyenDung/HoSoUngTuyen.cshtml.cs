using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hBE.Params;
using viecLam24hBE.ViewModels;

namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class HoSoUngTuyenModel : PageModel
    {
        private readonly HttpClient client = null;
        private string jobApplicationApiUrl = "";
        private string jobPostApiUrl = "";
        private readonly IMapper _mapper;
        private JsonSerializerOptions option;

        [BindProperty]
        public List<JobApplication> JobApplications { get; set; }
        

        public HoSoUngTuyenModel(IMapper mapper)
        {
            JobApplications = new List<JobApplication>();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            jobApplicationApiUrl = "http://localhost:5000/api/JobApplicant";
            jobPostApiUrl = "http://localhost:5000/api/JobPosts";
            _mapper = mapper;
            option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["TimeSubmit"] = Enums.TimeSubmitEnums;
            var userSession = HttpContext.Session.GetString(Enums.SESSION_KEY_USER);
            if (userSession == null)
            {
                return RedirectToPage("/NguoiTuyenDung/DangNhap");
            }
            var user = JsonSerializer.Deserialize<User>(userSession);

            var url = "/getJobPosts" + "?$filter= UserId eq " + user.Id;
            HttpResponseMessage responseJobTypes = await client.GetAsync(jobPostApiUrl + url);
            string strData = await responseJobTypes.Content.ReadAsStringAsync();

            List<JobPost> jobPostData = JsonSerializer.Deserialize<List<JobPost>>(strData, option);
            if (jobPostData.Count == 0) return Page();

            var jobNames = jobPostData.GroupBy(x => x.JobName).Select(x => x);
            ViewData["JobNames"] = jobNames;
 


            var urlJobApp = "/getJobApplications?$filter=JobPostId eq " + jobPostData.ElementAt(0).Id;

            for(int i = 1; i < jobPostData.Count; i++)
            {
                urlJobApp += " or JobPostId eq " + jobPostData.ElementAt(i).Id;
            }

            HttpResponseMessage responseJobApplication = await client.GetAsync(jobApplicationApiUrl + urlJobApp);
            string strDataJobApp = await responseJobApplication.Content.ReadAsStringAsync();

            List<JobApplication> jobApplications = JsonSerializer.Deserialize<List<JobApplication>>(strDataJobApp, option);
            JobApplications = jobApplications;

            return Page();
        }


   
        public async Task<bool> GetDataForFilter ()
        {
            var userSession = HttpContext.Session.GetString(Enums.SESSION_KEY_USER);
            if (userSession == null)
            {
                return false;
            }
            var user = JsonSerializer.Deserialize<User>(userSession);

            var url = "/getJobPosts" + "?$filter= UserId eq " + user.Id;
            HttpResponseMessage responseJobTypes = await client.GetAsync(jobPostApiUrl + url);
            string strData = await responseJobTypes.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<JobPost> jobPostData = JsonSerializer.Deserialize<List<JobPost>>(strData, option);
            var jobNames = jobPostData.GroupBy(x => x.JobName).Select(x => x);

            ViewData["JobNames"] = jobNames;
            ViewData["TimeSubmit"] = Enums.TimeSubmitEnums;

            return true;
        }



        public async Task<IActionResult> OnPost(string slJobNames, string filterDate)
        {
            var userSession = HttpContext.Session.GetString(Enums.SESSION_KEY_USER);
            if (userSession == null)
            {
                return RedirectToPage("/NguoiTuyenDung/DangNhap");
            }
            var user = JsonSerializer.Deserialize<User>(userSession);

            var url = "/getJobPosts" + "?$filter= UserId eq " + user.Id;
            HttpResponseMessage responseJobTypes = await client.GetAsync(jobPostApiUrl + url);
            string strData = await responseJobTypes.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<JobPost> jobPostData = JsonSerializer.Deserialize<List<JobPost>>(strData, option);
            var jobNames = jobPostData.GroupBy(x => x.JobName).Select(x => x);

            ViewData["JobNames"] = jobNames;
            ViewData["TimeSubmit"] = Enums.TimeSubmitEnums;

            FilterJobApplicationParam filterJobApplicationParam = new FilterJobApplicationParam();
            filterJobApplicationParam.UserId = user.Id;
            filterJobApplicationParam.JobName = slJobNames ?? null;
            if (!string.IsNullOrEmpty(filterDate))
            {
                if (filterDate.Contains("day"))
                {
                    int day = Int32.Parse(filterDate.Substring(0, 1));
                    filterJobApplicationParam.Day = day;
                } else if (filterDate.Contains("week"))
                {
                    int week = Int32.Parse(filterDate.Substring(0, 1));
                    filterJobApplicationParam.Week = week;
                } else if (filterDate.Contains("month"))
                {
                    int month = Int32.Parse(filterDate.Substring(0, 1));
                    filterJobApplicationParam.Month = month;
                }
            }

            var stringContent = new StringContent(JsonSerializer.Serialize(filterJobApplicationParam), Encoding.UTF8, "application/json");
            var urlJobApp = "/filterJobApplications";

            HttpResponseMessage responseJobApplication = await client.PostAsync(jobApplicationApiUrl + urlJobApp, stringContent);
            string strDataJobApp = await responseJobApplication.Content.ReadAsStringAsync();

            List<JobApplication> jobApplications = JsonSerializer.Deserialize<List<JobApplication>>(strDataJobApp, option);
            JobApplications = jobApplications;
            ViewData["jobName"] = slJobNames;
            ViewData["filterDate"] = filterDate;

            return Page();
        }
    }
}
