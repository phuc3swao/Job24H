using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hFE.Service.CallAPI;
using viecLam24hFE.Service.Constant.URL_API;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class NopHoSoModel : PageModel
    {
        [BindProperty]
        public IFormFile PdfFile { get; set; }

        [BindProperty]
        public List<ApplicantProfile> Profiles { get; set; }

        [BindProperty]
        public int jobPostId { get; set; }

        public User user { get; set; } 


        public async Task<IActionResult> OnGet(int jobid)
        {
            jobPostId = jobid;

            var userData = HttpContext.Session.GetString(Enums.SESSION_KEY_USER_UNG_TUYEN);

            if (!string.IsNullOrEmpty(userData))
            {
                user = System.Text.Json.JsonSerializer.Deserialize<User>(userData);

                Profiles = await ApiHelper.GetAsync<List<ApplicantProfile>>((URL.LIST_HO_SO + user.Id));
            }
            else
            {
                return RedirectToPage("/NguoiTimViec/DangNhap");
            }

            return Page();
        }


        public async Task<IActionResult> OnPost()
        {
            var userJson = HttpContext.Session.GetString(Enums.SESSION_KEY_USER_UNG_TUYEN);
            if (!string.IsNullOrEmpty(userJson))
            {
                user = System.Text.Json.JsonSerializer.Deserialize<User>(userJson);
            }
            else
            {
                return RedirectToPage("/NguoiTimViec/DangNhap");
            }

            //xu ly file_name
            string filename = $"{user.Id}_{DateTime.Now:yyyyMMddHHmmss}_{PdfFile.FileName}";

            if (PdfFile != null && PdfFile.Length > 0)
            {
                var apiEndpoint = "http://localhost:5000/api/UngTuyen/textPDF_123?file_name=" + filename;
                var httpClient = new HttpClient();

                using (var content = new MultipartFormDataContent())
                {

                    var fileContent = new StreamContent(PdfFile.OpenReadStream());
                    content.Add(fileContent, "file", PdfFile.FileName);

                    var response = await httpClient.PostAsync(apiEndpoint, content);

                    // Xử lý kết quả trả về từ API (response)
                    if (response.IsSuccessStatusCode)
                    {
                        // Thực hiện các xử lý thành công
                        string submit_url = URL.SUBMIT_SV + "userid=" + user.Id + "&file_name=" + filename + "&jobpostId=" + jobPostId;
                        await ApiHelper.PostAsync<string>(submit_url, "");
                        return RedirectToPage("/NguoiTimViec/HomePage");
                    }
                    else
                    {
                        // Xử lý khi gặp lỗi từ API
                        // Ví dụ: hiển thị thông báo lỗi cho người dùng
                        ModelState.AddModelError("",    "Có lỗi xảy ra khi gọi API.");
                    }
                }
            }
            return Page();
        }


        public async Task<IActionResult> OnPostApplyJob(int id, int hJobPostId)
        {
            if (true)
            {
                //appProfileId=4&jobPostId=11
                await ApiHelper.PostAsync<Task>(URL.APPLY_JOB_HO_SO_SAN + "appProfileId=" + id + "&jobPostId=" + hJobPostId, null);
                return RedirectToPage("/NguoiTimViec/Success");
            }
            else
            {
                return await OnGet(jobPostId);
            }
        }

    }
}
