using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hFE.Service.CallAPI;
using viecLam24hFE.Service.Constant.URL_API;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class DanhSachHoSoModel : PageModel
    {
        [BindProperty]
        public List<ApplicantProfile> applicantProfiles { get; set; }
        
        public User user { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var userData = HttpContext.Session.GetString(Enums.SESSION_KEY_USER_UNG_TUYEN);

            if (!string.IsNullOrEmpty(userData))
            {
                user = System.Text.Json.JsonSerializer.Deserialize<User>(userData);

                applicantProfiles = await ApiHelper.GetAsync<List<ApplicantProfile>>(URL.LIST_HO_SO + user.Id);

                return Page();
            }
            else
            {
                return RedirectToPage("/NguoiTimViec/DangNhap");
            }
        }

        public async Task OnPostDelete(int id)
        {
        }

        public async Task LoadData()
        {
            
        }
    }
}
