using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using System.Net.Http.Headers;
using System.Text.Json;
using viecLam24hBE.Models;
using viecLam24hFE.Service.Constant.URL_API;
using viecLam24hBE.Services;
using System.Collections.Generic;
using viecLam24hBE.Commons;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text;
using viecLam24hBE.ViewModels;
using viecLam24hBE.Params;

namespace viecLam24hFE.Pages.NguoiTuyenDung
{
    public class DanhSachTinDangModel : PageModel
    {
        private readonly HttpClient client = null;
        private string jobPostApiUrl = "";
        private readonly IMapper _mapper;

        [BindProperty]
        public List<JobPost> JobPosts { get; set; }

        public DanhSachTinDangModel(IMapper mapper)
        {
            JobPosts = new List<JobPost>();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            jobPostApiUrl = "http://localhost:5000/api/JobPosts";
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGet()
        {
            var userSession = HttpContext.Session.GetString(Enums.SESSION_KEY_USER);
            if (userSession == null)
            {
                return RedirectToPage("/NguoiTuyenDung/DangNhap");
            }
            var user = JsonSerializer.Deserialize<User>(userSession);

            var urlGetAll = "/getJobPosts?$filter= UserId eq " + user.Id;
            HttpResponseMessage responseJobTypes = await client.GetAsync(jobPostApiUrl + urlGetAll);
            string strData = await responseJobTypes.Content.ReadAsStringAsync();

            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            List<JobPost> jobPostData = JsonSerializer.Deserialize<List<JobPost>>(strData, option);
            var jobNames = jobPostData.GroupBy(x => x.JobName).Select(x => x);

            ViewData["JobNames"] = jobNames;
            ViewData["NumberPost"] = jobPostData.Count;
            JobPosts = jobPostData;

            return Page();
        }



        [HttpPost]
        public async Task<IActionResult> OnPost(string slJobNames)
        {
      
            string reportname = $"danh_sach_tin_tuyen_dung_{Guid.NewGuid():N}.xlsx";
            User userSession;
            try
            {
                userSession = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString(Enums.SESSION_KEY_USER));
            } catch (Exception ex)
            {
                userSession = null;
                return RedirectToPage("/NguoiTuyenDung/DangNhap");
            }
            
            var companyName = userSession.CompanyName;
            var dateExport = DateTime.Now.ToString("d/M/yyyy");

            jobPostApiUrl += "/getJobPosts?$filter= UserId eq " + userSession.Id;
            var option = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            if (!string.IsNullOrEmpty(slJobNames))
            {
                jobPostApiUrl += "& JobName eq '" + slJobNames + "'";     
            }

            HttpResponseMessage responseJobTypes = await client.GetAsync(jobPostApiUrl);
            string strData = await responseJobTypes.Content.ReadAsStringAsync();
            List<JobPost> jobPostData = JsonSerializer.Deserialize<List<JobPost>>(strData, option);



            var exportbytes = ExporttoExcel(jobPostData, reportname, companyName, dateExport);
            
            return File(exportbytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", reportname);
            
        }

        public byte[] ExporttoExcel(List<JobPost> lstJobPost, string filename, string companyName, string dateExport)
        {
            using ExcelPackage pack = new ExcelPackage();
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
           
            ws.Cells["A1"].Value = "Danh sách tin tuyển dụng công ty: " + companyName;
            ws.Cells["A2"].Value = "Ngày xuất danh sách: " + dateExport;
            ws.Cells["A4"].Value = "STT";
            ws.Cells["B4"].Value = "Tiêu đề";
            ws.Cells["C4"].Value = "Ngày đăng";
            ws.Cells["D4"].Value = "Ngày hêt hạn";
            //ws.Cells["E4"].Value = "Lượt ứng tuyển";
            //ws.Cells["F4"].Value = "Lượt xem";
            ws.Cells["E4"].Value = "Trạng thái tin";


            ws.Cells["A1"].AutoFitColumns();
            ws.Cells["A2"].AutoFitColumns();
            ws.Cells["A4"].AutoFitColumns();
            ws.Cells["B4"].AutoFitColumns();
            ws.Cells["C4"].AutoFitColumns();
            ws.Cells["D4"].AutoFitColumns();
            //ws.Cells["E4"].AutoFitColumns();
            //ws.Cells["F4"].AutoFitColumns();
            ws.Cells["E4"].AutoFitColumns();



            var cellA1 = ws.Cells["A1"];
            var cellA2 = ws.Cells["A2"];
            var cellA4 = ws.Cells["A4"];
            var cellB4 = ws.Cells["B4"];
            var cellC4 = ws.Cells["C4"];
            var cellD4 = ws.Cells["D4"];
            //var cellE4 = ws.Cells["E4"];
            //var cellF4 = ws.Cells["F4"];
            var cellE4 = ws.Cells["E4"];

            cellA1.Style.Font.Bold = true;
            cellA2.Style.Font.Bold = true;
            cellA4.Style.Font.Bold = true;
            cellB4.Style.Font.Bold = true;
            cellC4.Style.Font.Bold = true;
            cellD4.Style.Font.Bold = true;
            //cellE4.Style.Font.Bold = true;
            //cellF4.Style.Font.Bold = true;
            cellE4.Style.Font.Bold = true;

            int rowIndex = 5;
            int serialNumber = 1;

            foreach (var item in lstJobPost)
            {
                ws.Cells["A" + rowIndex].Value = serialNumber;
                ws.Cells["B" + rowIndex].Value = item.JobName;
                ws.Cells["C" + rowIndex].Value = item.CreatedDate.ToString("d-M-yyyy");
                ws.Cells["D" + rowIndex].Value = item.Deadline.ToString("d-M-yyyy");
                //ws.Cells["E" + rowIndex].Value = "";
                //ws.Cells["F" + rowIndex].Value = "";
                ws.Cells["E" + rowIndex].Value = item.Status? "Đang hiện" : "Đang ẩn";

                rowIndex++;
                serialNumber++;
            }


            // Thêm đường viền cho các ô
            int startRow = 4;
            int endRow = startRow + serialNumber - 1;
            int startColumn = 1;
            int endColumn = 5;

            for (int row = startRow; row <= endRow; row++)
            {
                for (int col = startColumn; col <= endColumn; col++)
                {
                    // Lấy ô tại dòng row và cột col
                    var cell = ws.Cells[row, col];

                    // Thêm đường viền cho ô
                    var border = cell.Style.Border;
                    border.Top.Style = ExcelBorderStyle.Thin;
                    border.Bottom.Style = ExcelBorderStyle.Thin;
                    border.Left.Style = ExcelBorderStyle.Thin;
                    border.Right.Style = ExcelBorderStyle.Thin;

                }
            }


            pack.Save();

            return pack.GetAsByteArray();
        }


        [HttpPost]
        public async Task<IActionResult> OnPostUpdateDealine(DateTime txtDateSubmit, int hIdJobPost)
        {
            var url = "/updateDealineJobPost";
            var stringContent = new StringContent(JsonSerializer.Serialize(new UpdateDeadlineParam { Id = hIdJobPost, NewDeadline = txtDateSubmit.ToString() }), Encoding.UTF8, "application/json");
            await client.PutAsync(jobPostApiUrl + url, stringContent);

            return RedirectToPage("/NguoiTuyenDung/DanhSachTinDang");
        }
    }
}
