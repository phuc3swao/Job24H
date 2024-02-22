using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Text.Json;
using viecLam24hBE.Models;
using viecLam24hBE.Services;
using viecLam24hBE.ViewModels;

namespace viecLam24hBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private ApplicantProfileService _applicantProfileService;

        public ApplicantProfileController(ApplicantProfileService applicantProfileService)
        {
            _applicantProfileService = applicantProfileService;
        }

        [Route("getApplicantProfile/{id}")]
        [HttpGet]
        public IActionResult getApplicantProfileById(int id)
        {
            return Ok(_applicantProfileService.GetApplicantProfileById(id));
        }


        [Route("getApplicantProfiles")]
        [HttpGet]
        [EnableQuery]
        public IActionResult GetApplicantProfiles()
        {
            return Ok(_applicantProfileService.GetApplicantProfiles().AsQueryable());
        }

        [HttpPut("UpdateApplicantProfiles")]
        public IActionResult UpdateApplicantProfiles(string  applicantProfile)
        {
            if(!string.IsNullOrEmpty(applicantProfile))
            {
                ApplicantProfile profile = JsonSerializer.Deserialize<ApplicantProfile>(applicantProfile);
                _applicantProfileService.UpdateApplicantProfiles(profile);
            }
            return Ok();
        }
    }
}
