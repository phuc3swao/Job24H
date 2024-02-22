using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using viecLam24hBE.Services;

namespace viecLam24hBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTypesController : ControllerBase
    {
        private readonly JobTypeService _jobTypeService;


        public JobTypesController(JobTypeService jobTypeService)
        {
            _jobTypeService = jobTypeService;
        }

        [Route("getAllJobTypes")]
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_jobTypeService.GetJobTypes().AsQueryable());
        }

    }
}
