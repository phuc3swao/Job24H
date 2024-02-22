using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class TestUploadPDFModel : PageModel
    {
        private readonly IWebHostEnvironment _environment;

        public TestUploadPDFModel(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [BindProperty]
        public IFormFile PdfFile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (PdfFile != null && PdfFile.Length > 0)
            {
                var apiEndpoint = "http://localhost:5000/api/UngTuyen/textPDF";
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
                        return RedirectToPage("/NguoiTimViec/SuccessPage");
                    }
                    else
                    {
                        // Xử lý khi gặp lỗi từ API
                        // Ví dụ: hiển thị thông báo lỗi cho người dùng
                        ModelState.AddModelError("", "Có lỗi xảy ra khi gọi API.");
                    }
                }
            }

            return Page();
        }
    }
}