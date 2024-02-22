using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hFE.Models;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class DangNhapModel : PageModel
    {
        private readonly HttpClient client = null;
        private string userApiUrl = "";

        public DangNhapModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            userApiUrl = "http://localhost:5000/api/Users";
        }


        [BindProperty]
        public TimViecLogin TimViecLogin { get; set; }

        public void OnGet()
        {
            
        }

        //public IActionResult OnGetLogOut()
        //{
        //    HttpContext.Session.Remove(Enums.SESSION_KEY_USER_UNG_TUYEN);

        //    return RedirectToPage("/NguoiTimViec/DangNhap");
        //}

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var url = "/tim-viec/login";
                var stringContent = new StringContent(JsonConvert.SerializeObject(TimViecLogin), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(userApiUrl + url, stringContent);
                string strData = await response.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(strData);
            ViewData["responseMessage"] = "";

            if (user != null)
            {
                if (user.Active == true)
                {
                    if (user.RoleId == Enums.UNG_TUYEN_ROLE)
                    {
                        HttpContext.Session.SetString(Enums.SESSION_KEY_USER_UNG_TUYEN, System.Text.Json.JsonSerializer.Serialize(user));
                        HttpContext.Session.Remove(Enums.SESSION_KEY_USER);
                        return RedirectToPage("/NguoiTimViec/HomePage");
                    }
                    else
                    {
                        ViewData["responseMessage"] = "Tài khoản không tồn tại";
                        return Page();
                    }
                    
                } else
                {
                    ViewData["responseMessage"] = "Tài khoản của bạn hiện đang bị khóa";
                    return Page();
                }
            }
            ViewData["responseMessage"] = "Tài khoản hoặc mật khẩu không chính xác";
            return Page();
        }

        public async Task<IActionResult> OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/NguoiTimViec/HomePage");
        }
    }
}
