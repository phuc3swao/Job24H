using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public class ApplicantProfileServiceImpl : ApplicantProfileService
    {
        private readonly MyDbContext _context;

        public ApplicantProfileServiceImpl(MyDbContext context)
        {
            _context = context;
        }
        public ApplicantProfile GetApplicantProfileById(int id)
        {
            try
            {
                return _context.ApplicantProfiles.Include(_ => _.User).FirstOrDefault(a => a.Id == id);
            } catch (Exception ex)
            {
                Console.WriteLine("Có lỗi trong quá trình GetApplicantProfileById, chi tiết: " + ex.Message);
                return null;
            }
        }

        public List<ApplicantProfile> GetApplicantProfiles()
        {
            try
            {
                return _context.ApplicantProfiles.Include(_ => _.User).Select(ap => new ApplicantProfile {
                    Id = ap.Id,
                    UserId = ap.UserId,
                    JobName = ap.JobName,
                    JobTypeId = ap.JobTypeId,
                    Salary = ap.Salary,
                    WorkingForm = ap.WorkingForm,
                    Degree = ap.Degree,
                    Experence = ap.Experence,
                    WorkLocation = ap.WorkLocation,
                    CareerGoals = ap.CareerGoals,
                    Skills = ap.Skills,
                    InformationTechnology = ap.InformationTechnology,
                    IsPublic = ap.IsPublic,
                    NameReference = ap.NameReference,
                    PhoneReference = ap.PhoneReference,
                    CompanyReference = ap.CompanyReference,
                    PositionReference = ap.PositionReference,
                    CV_name = ap.CV_name,
                    User = new User {
                        Id = ap.User.Id,
                        Email = ap.User.Email,
                        Password = ap.User.Password,
                        CreatedAt = ap.User.CreatedAt,
                        UserName = ap.User.UserName,
                        Active = ap.User.Active,
                        RoleId = ap.User.RoleId,
                        Sex = ap.User.Sex,
                        Dob = ap.User.Dob,
                        Address = ap.User.Address,
                        Avatar = ap.User.Avatar,
                        Phone = ap.User.Phone,
                        CompanyName = ap.User.CompanyName,

                    }

                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi trong quá trình GetApplicantProfiles, chi tiết: " + ex.Message);
                return null;
            }
        }

        public async Task UpdateApplicantProfiles(ApplicantProfile applicantProfile)
        {
            ApplicantProfile oldApplicant = _context.ApplicantProfiles.Where(ap => ap.Id.Equals(applicantProfile.Id)).FirstOrDefault();
            if (oldApplicant != null)
            {
                oldApplicant.JobName = applicantProfile.JobName;
                oldApplicant.JobTypeId = applicantProfile.JobTypeId;
                oldApplicant.Salary = applicantProfile.Salary;
                oldApplicant.WorkingForm = applicantProfile.WorkingForm;
                oldApplicant.Degree = applicantProfile.Degree;
                oldApplicant.Experence = applicantProfile.Experence;
                oldApplicant.WorkLocation = applicantProfile.WorkLocation;
                oldApplicant.IsPublic = applicantProfile.IsPublic;
                oldApplicant.CV_name = applicantProfile.CV_name;
                oldApplicant.CareerGoals = applicantProfile.CareerGoals;
                oldApplicant.InformationTechnology = applicantProfile.InformationTechnology;
                oldApplicant.Skills = applicantProfile.Skills;
                oldApplicant.CompanyReference = applicantProfile.CompanyReference;
                oldApplicant.NameReference = applicantProfile.NameReference;
                oldApplicant.PhoneReference = applicantProfile.PhoneReference;
                oldApplicant.PositionReference = applicantProfile.PositionReference;

                _context.ApplicantProfiles.Update(oldApplicant);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Đối tượng update không hợp lệ.");
            }
        }
    }
}



