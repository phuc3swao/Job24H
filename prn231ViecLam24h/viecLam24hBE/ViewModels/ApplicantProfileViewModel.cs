namespace viecLam24hBE.ViewModels
{
    public class ApplicantProfileViewModel
    {
        public int? Id { get; set; }

        //Add new
        public int UserId { get; set; }
        public string JobName { get; set; }
        public int JobTypeId { get; set; }
        public double Salary { get; set; }
        public string WorkingForm { get; set; }
        public string Degree { get; set; }
        public string Experence { get; set; }
        public string WorkLocation { get; set; }

        //add new
        public string CareerGoals { get; set; } // Mục tiêu nghề nghiệp
        public string? Skills { get; set; } // Kĩ năng mềm hoặc cứng
        public string? InformationTechnology { get; set; } // Tin học
        public string? NameReference { get; set; }
        public string? PhoneReference { get; set; }
        public string? CompanyReference { get; set; }
        public string? PositionReference { get; set; }
        //
        public bool? IsPublic { get; set; }
        public string? CV_name { get; set; }
    }
}
