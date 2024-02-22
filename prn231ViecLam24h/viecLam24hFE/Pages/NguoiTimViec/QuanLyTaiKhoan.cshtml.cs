using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hBE.ViewModels;
using viecLam24hFE.Models;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class QuanLyTaiKhoanModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly HttpClient client = null;
        private string userApiUrl = "";

        public QuanLyTaiKhoanModel(IMapper mapper)
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            userApiUrl = "http://localhost:5000/api/Users";
            _mapper = mapper;
        }

        [BindProperty]
        public UngTuyenDetail UngTuyenDetail { get; set; }
        public async Task<IActionResult> OnGet()
        {
            var userSession = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString(Enums.SESSION_KEY_USER_UNG_TUYEN));
            if (userSession == null) return BadRequest();

            var ungTuyenDetail = _mapper.Map<UngTuyenDetail>(userSession);
            UngTuyenDetail = ungTuyenDetail;


            return Page();
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            var url = "/updateUser";
            var stringContent = new StringContent(JsonSerializer.Serialize(UngTuyenDetail), Encoding.UTF8, "application/json");
            await client.PutAsync(userApiUrl + url, stringContent);
            User user = new User();
            user = _mapper.Map<User>(UngTuyenDetail);

            HttpContext.Session.SetString(Enums.SESSION_KEY_USER_UNG_TUYEN, JsonSerializer.Serialize(user));
            
            return Page();
        }
    }
}
