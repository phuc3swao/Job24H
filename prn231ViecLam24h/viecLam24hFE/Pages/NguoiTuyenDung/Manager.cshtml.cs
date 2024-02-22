using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;

namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class ManagerModel : PageModel
    {
        public void OnGet()
        {
            //var userSession = HttpContext.Session.GetString(Enums.SESSION_KEY_USER);
            //if (userSession == null)
            //{
            //    return RedirectToPage("/NguoiTuyenDung/DangNhap");
            //}

            //return RedirectToPage("/NguoiTuyenDung/TaoTinTuyenDung");
        }

        public IActionResult OnGetDangTinNgay()
        {
            string userSession;
            
            try
            {
                userSession = HttpContext.Session.GetString(Enums.SESSION_KEY_USER);

            } catch (Exception ex) {

                userSession = null;
            }

            if (userSession == null)
            {
                return RedirectToPage("/NguoiTuyenDung/DangNhap");
            }

            return RedirectToPage("/NguoiTuyenDung/Manager");
        }
    }
}
