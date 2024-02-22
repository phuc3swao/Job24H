using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hBE.Services;
using viecLam24hFE.Models;

namespace viecLam24hBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;


        public UsersController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Route("tuyen-dung/login")]
        [HttpPost]
        public IActionResult Login(TuyenDungLogin tuyenDungLogin)
        {
            var user = _userService.GetUserByEmailAndPassword(_mapper.Map<User>(tuyenDungLogin));
            

            return Ok(user);
        }



        [Route("tuyen-dung/dangKy")]
        [HttpPost]
        public IActionResult DangKyTuyenDung(DangkiTuyenDung dangkiTuyenDung)
        {
            if (dangkiTuyenDung == null) return BadRequest();
            User userTuyenDung = _mapper.Map<User>(dangkiTuyenDung);
            userTuyenDung.RoleId = Enums.TUYEN_DUNG_ROLE;
            userTuyenDung.CreatedAt = DateTime.Now;
            userTuyenDung.Active = true;

            _userService.InsertUser(userTuyenDung);
            return Ok();
        }


        [Route("tim-viec/login")]
        [HttpPost]
        public IActionResult Login(TimViecLogin timViecLogin)
        {
            var user = _userService.GetUserByEmailAndPassword(_mapper.Map<User>(timViecLogin));

            return Ok(user);
        }


        [Route("getUsers")]
        [HttpGet]
        [EnableQuery]
        public IActionResult getUsers()
        {
            return Ok(_userService.GetUsers().AsQueryable());
        }




        [Route("updateUser")]
        [HttpPut]
        public IActionResult UpdateUser(UngTuyenDetail ungTuyenDetail) {
            var user = _mapper.Map<User>(ungTuyenDetail);
            _userService.UpdateUser(user);
            return Ok();
        }
    }
}
