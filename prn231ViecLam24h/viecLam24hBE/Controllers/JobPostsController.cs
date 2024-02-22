using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using viecLam24hBE.Models;
using viecLam24hBE.Services;
using viecLam24hBE.ViewModels;
using System.Linq.Expressions;
using viecLam24hBE.Params;

namespace viecLam24hBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostsController : ControllerBase
    {
        private readonly JobPostService _jobPostService;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly JobTypeService _jobTypeService;

        public JobPostsController(JobPostService jobPostService, IMapper mapper, UserService userService, JobTypeService jobTypeService)
        {
            _jobPostService = jobPostService;
            _mapper = mapper;
            _userService = userService;
            _jobTypeService = jobTypeService;
        }


        [Route("addJobPost")]
        [HttpPost]
        public IActionResult AddJobPost(JobPostViewModel jobPostViewModel)
        {
            var jobPost = _mapper.Map<JobPost>(jobPostViewModel);
            jobPost.User = _userService.GetUserById(jobPost.UserId);
            jobPost.JobType = _jobTypeService.GetJobTypeById(jobPost.JobTypeId);
            var rs = _jobPostService.AddJobPost(jobPost);

            return Ok(rs);
        }


        [Route("editJobPost")]
        [HttpPut]
        public IActionResult EditJobPost(JobPostViewModel jobPostViewModel)
        {
            var jobPost = _mapper.Map<JobPost>(jobPostViewModel);
            jobPost.User = _userService.GetUserById(jobPost.UserId);
            jobPost.JobType = _jobTypeService.GetJobTypeById(jobPost.JobTypeId);

            var rs = _jobPostService.EditJobPost(jobPost);

            return Ok(rs);
        }

        [Route("getJobPosts")]
        [HttpGet]
        [EnableQuery]
        public IActionResult GetAllJobPosts ()
        {
            return Ok(_jobPostService.GetJobsPosts().AsQueryable());
        }

        [Route("getJobPost/{id}")]
        [HttpGet]
        public IActionResult GetJobPost(int id)
        {
            return Ok(_jobPostService.GetJobPostById(id));
        }

        [Route("updateDealineJobPost")]
        [HttpPut]
        public IActionResult UpdateJobPost(UpdateDeadlineParam updateDeadlineParam)
        {
            DateTime NewDeadline = DateTime.Parse(updateDeadlineParam.NewDeadline);
            _jobPostService.UpdateDeadline(updateDeadlineParam.Id, NewDeadline);
            return Ok();
        }

        [Route("deleteJobPost/{id}")]
        [HttpDelete]
        public IActionResult DeleteJobPost(int id)
        {
            _jobPostService.DeleteJobPost(id);
            return Ok();
        }

        [Route("changeStatus/{id}")]
        [HttpPut]
        public IActionResult ChangeStatusJobPost(int id)
        {
            _jobPostService.ChangeStatus(id);
            return Ok();
        }



    }
}
