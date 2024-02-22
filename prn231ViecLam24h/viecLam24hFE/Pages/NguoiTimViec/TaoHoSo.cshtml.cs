using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hBE.ViewModels;
using viecLam24hFE.Models;
using viecLam24hFE.Service.CallAPI;
using viecLam24hFE.Service.Constant.URL_API;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    [BindProperties]
    public class TaoHoSoModel : PageModel
    {
        public User UngTuyenDetail { get; set; }
        public ApplicantProfile applicantProfile { get; set; }

        public async Task OnGet()
        {
            await LoadData();
        }

        public async Task LoadData()
        {
            UngTuyenDetail = System.Text.Json.JsonSerializer.Deserialize<User>(HttpContext.Session.GetString(Enums.SESSION_KEY_USER_UNG_TUYEN));
        }

        public async Task<IActionResult> OnPost()
        {
            await LoadData();
            applicantProfile.UserId = UngTuyenDetail.Id;
            string applicantProfileJson = System.Text.Json.JsonSerializer.Serialize(applicantProfile);
            string url = URL.TAO_HO_SO + "?applicantProfile=" + applicantProfileJson;
            try
            {
            await ApiHelper.PostAsync<Task>(url, null);
                return RedirectToPage("./DanhSachHoSo");
            }catch(Exception ex)
            {
                return RedirectToPage("./ServerFail");
            }
        }
    }
}
