using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hBE.ViewModels;

namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class TaoTinTuyenDungDetailModel : PageModel
    {

        private readonly HttpClient client = null;
        private string jobPostApiUrl = "";
        private readonly IMapper _mapper;

        public TaoTinTuyenDungDetailModel(IMapper mapper)
        {
            JobPostViewModel = new JobPostViewModel();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            jobPostApiUrl = "http://localhost:5000/api/JobPosts";
            _mapper = mapper;
        }

        [BindProperty]
        public JobPostViewModel JobPostViewModel { get; set; }
        public IActionResult OnGetJobPostDetai()
        {
            var jobPostViewModel = JsonSerializer.Deserialize<JobPostViewModel>(HttpContext.Session.GetString(Enums.SESSION_KEY_JOBPOST));
            JobPostViewModel = jobPostViewModel != null ? jobPostViewModel : new JobPostViewModel();

            return Page();
        }

        public async Task<IActionResult> OnGetCreatePost()
        {
            var jobPostViewModel = JsonSerializer.Deserialize<JobPostViewModel>(HttpContext.Session.GetString(Enums.SESSION_KEY_JOBPOST));
            JobPostViewModel = jobPostViewModel != null ? jobPostViewModel : new JobPostViewModel();
            var userSession = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString(Enums.SESSION_KEY_USER));
            if (userSession == null)
            {
                return RedirectToPage("/NguoiTuyenDung/DangNhap");
            }
            jobPostViewModel.UserId = userSession.Id;
           
            if (jobPostViewModel.Id != 0 && jobPostViewModel.Id != null)
            {
                var url = "/editJobPost";
                var stringContent = new StringContent(JsonSerializer.Serialize(jobPostViewModel), Encoding.UTF8, "application/json");
                await client.PutAsync(jobPostApiUrl + url, stringContent);
            } else
            {
                jobPostViewModel.CreatedDate = DateTime.Now;
                var url = "/addJobPost";
                var stringContent = new StringContent(JsonSerializer.Serialize(jobPostViewModel), Encoding.UTF8, "application/json");
                await client.PostAsync(jobPostApiUrl + url, stringContent);
            }
 

            

            return RedirectToPage("/NguoiTuyenDung/DanhSachTinDang");
        }
    }
}
