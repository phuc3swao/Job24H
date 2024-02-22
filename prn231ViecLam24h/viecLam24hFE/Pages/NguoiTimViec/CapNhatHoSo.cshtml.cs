using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using viecLam24hBE.Models;
using viecLam24hFE.Service.CallAPI;
using viecLam24hFE.Service.Constant.URL_API;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class CapNhatHoSoModel : PageModel
    {
        [BindProperty]
        public ApplicantProfile applicantProfile { get; set; }

        public static int appProfileId { get; set; }

        public async Task OnGet(int id)
        {
            appProfileId= id;
            await LoadData();
        }

        public async Task LoadData()
        {
            applicantProfile = await ApiHelper.GetAsync<ApplicantProfile>(URL.DETAIL_HO_SO + appProfileId);
        }

        public async Task<IActionResult> OnPost()
        {
            string ApplicanProfileJson = System.Text.Json.JsonSerializer.Serialize(applicantProfile);
            await ApiHelper.PutAsync<Task>(URL.UPDATE_HO_SO + ApplicanProfileJson, null);
            return RedirectToPage("./DanhSachHoSo");
        }
    }
}
