using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hFE.Models;

namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class DangKyModel : PageModel
    {


        private readonly HttpClient client = null;
        private string userApiUrl = "";
        private readonly IMapper _mapper;
        private JsonSerializerOptions option;

        [BindProperty]
        public DangkiTuyenDung DangkiTuyenDung { get; set; }

        public DangKyModel(IMapper mapper)
        {
            DangkiTuyenDung = new DangkiTuyenDung();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            userApiUrl = "http://localhost:5000/api/Users";
            _mapper = mapper;
            option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            ViewData["responseMessage"] = "";
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var urlFilterUser = "/getUsers?$filter=email eq '" + DangkiTuyenDung.Email + "'";
            HttpResponseMessage response = await client.GetAsync(userApiUrl + urlFilterUser);
            string strData = await response.Content.ReadAsStringAsync();
            try
            {
                List<User> lstUser = JsonSerializer.Deserialize<List<User>>(strData);

                if (lstUser.Count > 0)
                {
                    ViewData["responseMessage"] = "Email này đã được sử dụng, vui lòng chọn Email khác";

                    return Page();
                }
            } catch (Exception ex)
            {

            }

            var urlDangKy = "/tuyen-dung/dangKy";
            var stringContent = new StringContent(JsonSerializer.Serialize(DangkiTuyenDung), Encoding.UTF8, "application/json");
            await client.PostAsync(userApiUrl + urlDangKy, stringContent);

            HttpResponseMessage responseCheckDangKy = await client.GetAsync(userApiUrl + urlFilterUser);
            string strDataCheck = await responseCheckDangKy.Content.ReadAsStringAsync();


            try
            {
                List<User> lstUserCheck = JsonSerializer.Deserialize<List<User>>(strDataCheck);

                if (lstUserCheck.Count > 0)
                {
                    ViewData["responseMessage"] = "Đăng ký tài khoản thành công, bạn có thể đăng nhập ngay bây giờ!";

                    return Page();
                }
            }
            catch (Exception ex)
            {
            }

            ViewData["responseMessage"] = "Đăng ký tài khoản thất bại, vui lòng thử lại!";

            return Page();
        }
    }
}
