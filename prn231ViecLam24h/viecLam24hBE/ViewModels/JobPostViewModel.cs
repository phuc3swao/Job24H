using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using viecLam24hBE.Models;

namespace viecLam24hBE.ViewModels
{
    public class JobPostViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập chức danh")]
        public string JobName { get; set; }
        public int? UserId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nghề nghiệp")]
        public int JobTypeId { get; set; }
        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string JobDescription { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa điểm làm việc")]
        public string JobLocation { get; set; }
        public bool? Status { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mức lương")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn kinh nghiệm")]
        public string Experence { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hình thức làm việc")]
        public string WorkingForm { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn bằng cấp")]
        public string Degree { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }

        public string Sex { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số lượng tuyển")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string Address { get; set; }
        public DateTime? Deadline { get; set; }
        public JobTypeViewModel? JobTypeViewModel { get; set; }


    }
}
