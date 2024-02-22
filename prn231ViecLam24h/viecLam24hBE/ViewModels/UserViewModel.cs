using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using viecLam24hBE.Models;

namespace viecLam24hFE.Models
{
    public class UserViewModel
    {
    }

    public class TuyenDungLogin
    {
        [Required(ErrorMessage = ("Bạn cần phải nhập Email"))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = ("Bạn cần phải nhập Mật khẩu"))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class TimViecLogin : TuyenDungLogin
    {

    }

    public class UngTuyenDetail
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public string? Sex { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
    }

    public class DangkiTuyenDung : TuyenDungLogin
    {
        [Required(ErrorMessage =("Bạn cần phải nhập họ và tên"))]
        public string UserName { get; set; }

        [Required(ErrorMessage = ("Bạn cần phải nhập số điện thoại"))]
        [DataType(DataType.PhoneNumber, ErrorMessage = ("Bạn cần phải nhập đúng định dạng số điện thoại"))]
        public string Phone { get; set; }
        [Required(ErrorMessage = ("Bạn cần phải nhập tên công ty"))]
        public string CompanyName { get; set; }
    }

    public class TuyenDungDetail
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public string? Sex { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
    }

}
