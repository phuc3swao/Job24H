using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using viecLam24hBE.Params;
using viecLam24hBE.Services;

namespace viecLam24hBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicantController : ControllerBase
    {
        private JobApplicationService _jobApplicationService;

        public JobApplicantController(JobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [Route("getJobApplications")]
        [HttpGet]
        [EnableQuery]
        public IActionResult GetJobApplications()
        {
            return Ok(_jobApplicationService.GetJobApplications().AsQueryable());
        }


        [Route("filterJobApplications")]
        [HttpPost]
        public IActionResult FilterJobApplications(FilterJobApplicationParam filterJob)
        {
            return Ok(_jobApplicationService.FilterJobApplications(filterJob));
        }


        [Route("changeStatus/{id}")]
        [HttpPut]
        public IActionResult ChangeStatusJobApplication(int id)
        {
            _jobApplicationService.ChangeStatus(id);
            return Ok();
        }


        [Route("deleteJobApplication/{id}")]
        [HttpDelete]
        public IActionResult DeleteJobPost(int id)
        {
            _jobApplicationService.DeleteJobApplication(id);
            return Ok();
        }

        [HttpPost("InsertNewJobApplicant")]
        public IActionResult InsertNewJobApplicant(int appProfileId , int jobPostId)
        {
            _jobApplicationService.InsertJobApplication(appProfileId, jobPostId);
            return Ok();
        }
    }
}
