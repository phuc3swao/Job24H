using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hBE.ViewModels;


namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class TaoTinTuyenDungModel : PageModel
    {

        private readonly HttpClient client = null;
        private string jobTypeApiUrl = "";
        private string jobPostApiUrl = "";
        private readonly IMapper _mapper;

        public TaoTinTuyenDungModel(IMapper mapper)
        {
            JobPostViewModel = new JobPostViewModel();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            jobTypeApiUrl = "http://localhost:5000/api/JobTypes";
            jobPostApiUrl = "http://localhost:5000/api/JobPosts";
            _mapper = mapper;
        }

        [BindProperty]
        public JobPostViewModel JobPostViewModel { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var url = "/getAllJobTypes";
            HttpResponseMessage responseJobTypes = await client.GetAsync(jobTypeApiUrl + url);
            string strData = await responseJobTypes.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<JobType> jobTypes = JsonSerializer.Deserialize<List<JobType>>(strData, option);
            ViewData["jobTypes"] = jobTypes;
            GetDataForList();
            GetUserSession();

            return Page();
        }

        public void GetDataForList()
        {
            ViewData["SexEnums"] = Enums.SexEnums;
            ViewData["WorkingFormEnums"] = Enums.WorkingFormEnums;
            ViewData["DegreeEnums"] = Enums.DegreeEnums;
            ViewData["ExperenceEnums"] = Enums.ExperenceEnums;
        }

        public void GetUserSession ()
        {
            User userSession = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString(Enums.SESSION_KEY_USER).ToString());
            ViewData["userSession"] = userSession;
        }

        public async Task<IActionResult> OnPost()
        {
            var urlSecond = "/getAllJobTypes";
            HttpResponseMessage responseJobTypes = await client.GetAsync(jobTypeApiUrl + urlSecond);
            string strData = await responseJobTypes.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<JobType> jobTypes = JsonSerializer.Deserialize<List<JobType>>(strData, option);

            if (!ModelState.IsValid)
            {
                if (JobPostViewModel.JobTypeId == 0)
                {
                    ModelState.AddModelError("JobTypeId", "Vui lòng chọn nghề nghiệp");
                }
                if (JobPostViewModel.Quantity == 0)
                {
                    ModelState.AddModelError("Quantity", "Số lượng tuyển phải lớn hơn 0");
                }
                if (JobPostViewModel.Salary == 0)
                {
                    ModelState.AddModelError("Salary", "Mức lương phải lớn hơn 0");
                }
                if (JobPostViewModel.Deadline == null)
                {
                    ModelState.AddModelError("Deadline", "Bạn cần chọn hạn nộp hồ sơ");
                }

                ViewData["jobTypes"] = jobTypes;
                GetDataForList();
                GetUserSession();
                return Page();
            }

            //var url = "/addJobPost";
            //var stringContentJobPost = new StringContent(JsonSerializer.Serialize(JobPostViewModel), Encoding.UTF8, "application/json");
            //await client.PostAsync(jobPostApiUrl + url, stringContentJobPost);
            JobType jobType = jobTypes.FirstOrDefault(j => j.Id == JobPostViewModel.JobTypeId);
            JobPostViewModel.JobTypeViewModel = _mapper.Map<JobTypeViewModel>(jobType);
            HttpContext.Session.SetString(Enums.SESSION_KEY_JOBPOST, JsonSerializer.Serialize(JobPostViewModel));

            return RedirectToPage("/NguoiTuyenDung/TaoTinTuyenDungDetail", "JobPostDetai");

        }
    }
}
