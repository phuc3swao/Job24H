using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;
using viecLam24hBE.Models;

namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class XemChiTietHoSoModel : PageModel
    {

        private readonly HttpClient client = null;
        private string applicantProfileApiUrl = "";
        private string jobApplicationApiUrl = "";
        private readonly IMapper _mapper;
        private JsonSerializerOptions option;

        [BindProperty]
        public ApplicantProfile ApplicantProfileModel { get; set; }

        public XemChiTietHoSoModel(IMapper mapper)
        {
            ApplicantProfileModel = new ApplicantProfile();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            applicantProfileApiUrl = "http://localhost:5000/api/ApplicantProfile";
            jobApplicationApiUrl = "http://localhost:5000/api/JobApplicant";
            _mapper = mapper;
            option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }


        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                var urlJobApp = "/getJobApplications?$filter=Id eq " + id;
                HttpResponseMessage responseJobApp = await client.GetAsync(jobApplicationApiUrl + urlJobApp);
                string strDataJobApp = await responseJobApp.Content.ReadAsStringAsync();
                List<JobApplication> lstJobApp = JsonSerializer.Deserialize<List<JobApplication>>(strDataJobApp, option);

                var url = "/getApplicantProfiles?$filter=id eq " + lstJobApp.ElementAt(0).ApplicantId;
                HttpResponseMessage responseApplicantProfile = await client.GetAsync(applicantProfileApiUrl + url);
                string strData = await responseApplicantProfile.Content.ReadAsStringAsync();

                List<ApplicantProfile> applicantProfile = JsonSerializer.Deserialize<List<ApplicantProfile>>(strData, option);

                ApplicantProfileModel = applicantProfile.ElementAt(0);

                return Page();
            } catch (Exception ex)
            {
                return BadRequest("Có lỗi tại hàm onGet, Page XemChiTietHoSo, chi tiết: " + ex.Message);
            }
            
        }
    }
}
