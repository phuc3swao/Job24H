using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using viecLam24hBE.Models;
using viecLam24hBE.ViewModels;

namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class DetaisJobPostModel : PageModel
    {
        private readonly HttpClient client = null;
        private string jobPostApiUrl = "";
        private string jobTypeApiUrl = "";
        private readonly IMapper _mapper;


        [BindProperty]
        public JobPostViewModel JobPostViewModel { get; set; }

        public DetaisJobPostModel(IMapper mapper)
        {
            JobPostViewModel = new JobPostViewModel();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            jobPostApiUrl = "http://localhost:5000/api/JobPosts";
            jobTypeApiUrl = "http://localhost:5000/api/JobTypes";
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            jobPostApiUrl += "/getJobPosts?$filter= Id eq " + id;


            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            HttpResponseMessage responseJobpost = await client.GetAsync(jobPostApiUrl);
            string strData = await responseJobpost.Content.ReadAsStringAsync();
            List<JobPost> jobPostData = JsonSerializer.Deserialize<List<JobPost>>(strData, option);
            var jobPost = jobPostData.ElementAt(0);

            jobTypeApiUrl += "/getAllJobTypes?$filter= Id eq " + jobPost.JobTypeId;
            HttpResponseMessage responseJobType = await client.GetAsync(jobTypeApiUrl);
            string strDataJobType = await responseJobType.Content.ReadAsStringAsync();
            List<JobType> lstJobTypes = JsonSerializer.Deserialize<List<JobType>>(strDataJobType, option);
            var jobPostViewModel = _mapper.Map<JobPostViewModel>(jobPost);
            jobPostViewModel.JobTypeViewModel = _mapper.Map<JobTypeViewModel>(lstJobTypes.ElementAt(0));

            JobPostViewModel = jobPostViewModel;

            return Page();
        }
    }
}
