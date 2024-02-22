using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using viecLam24hBE.Models;
using viecLam24hBE.Services;

namespace viecLam24hBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UngTuyenController : ControllerBase
    {
        public readonly UngTuyenService _ungtuyen;

        public UngTuyenController(UngTuyenService ungtuyen)
        {
            _ungtuyen = ungtuyen;
        }

        [Route("homepage/AllJobPost")]
        [HttpGet]
        public async Task<IActionResult> GetAllJobPost()
        {
            var result = await _ungtuyen.JobPosts();
            return Ok(result);
        }

        [Route("homepage/SearchJobPost")]
        [HttpGet]
        public async Task<IActionResult> GetSearchPost(string? profileName, string? jobType, string? jobLocation)
        {
            return Ok(await _ungtuyen.JobPosts(profileName != null ? profileName : "", 
                jobType != null ? jobType : "", 
                jobLocation != null ? jobLocation : ""  ));
        }

        [Route("searchpage/FilterJobPost")]
        [HttpGet]
        public async Task<IActionResult> GetFiterPost(string? exp_level, string? salary, string? jobType, string? jobLocation)
        {
            return Ok(await _ungtuyen.JobPosts(exp_level != null ? exp_level : "Tất cả kinh nghiệm", 
                salary != null ? salary : "Tất cả mức lương", 
                jobType != null ? jobType : "", 
                jobLocation != null ? jobLocation : ""));
        }

        [Route("searchpage/ListExp_level")]
        [HttpGet]
        public async Task<IActionResult> GetListExp_Level()
        {
            return Ok(await _ungtuyen.Exp_Level());
        }

        [Route("searchpage/ListSalary_level")]
        [HttpGet]
        public async Task<IActionResult> GetListSalary_Level()
        {
            return Ok(await _ungtuyen.Salary_Level());
        }

        [Route("Detail/GetDetailJobPost")]
        [HttpGet]
        public async Task<IActionResult> GetDetailJobPost(int id)
        {
            return Ok(await _ungtuyen.JobPost(id));
        }


        [HttpPost("textPDF")]
        public async Task<IActionResult> UploadPdf()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "CV_Save", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok("PDF saved successfully!");
                }
                else
                {
                    return BadRequest("No file uploaded.");
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("textPDF_123")]
        public async Task<IActionResult> UploadPdf(string file_name)
        {
            try
            {
                // Xử lý thông tin User tại đây

                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    //var fileName = Path.GetFileName(file.FileName);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "CV_Save", file_name);


                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok("PDF saved successfully!");
                }
                else
                {
                    return BadRequest("No file uploaded.");
                }
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("SubmitCV")]
        public async Task<IActionResult> SubmitCV(int userid, string file_name, int jobpostId)
        {
            try
            {
                _ungtuyen.SubmitCV(userid, file_name,jobpostId);
            }
            catch(Exception ex)
            {
                return Conflict(ex);
            }
            return Ok();
        }

        //Manager 
        [HttpPost("TaoHoSo")]
        public async Task <IActionResult> TaoHoSo(string applicantProfile)
        {
            if(applicantProfile != null)
            {
                ApplicantProfile newapplicantProfile = JsonSerializer.Deserialize<ApplicantProfile>(applicantProfile);
                try
                {
                    _ungtuyen.TaoHoSo(newapplicantProfile);
                }
                catch (Exception ex)
                {

                }
                return Ok();
            }
            return BadRequest();
            
        }

        [HttpGet("listHoSo")]
        public async Task<IActionResult> listHoSo(int userid)
        {
            return Ok(await _ungtuyen.ListHoSo(userid));
        }

        [HttpGet("HoSoDetail")]
        public async Task<IActionResult> ChiTietHoSo(int id)
        {
            return Ok(await _ungtuyen.GetHoSo(id));
        }

        [HttpDelete("DeleteHoSo")]
        public async Task<IActionResult> DeleteHoSo(int id)
        {
            _ungtuyen.XoaHoSo(id);
            return Ok();
        }

        [HttpGet("ListLocation")]
        public async Task<IActionResult> ListLocation()
        {
            return Ok(await _ungtuyen.ListLocation());
        }
    }
}
