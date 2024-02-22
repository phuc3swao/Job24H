using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;
using viecLam24hBE.Params;

namespace viecLam24hBE.Services
{
    public class JobApplicationServiceImpl : JobApplicationService
    {
        private readonly MyDbContext _context;

        public JobApplicationServiceImpl(MyDbContext context)
        {
            _context = context;
        }

        public void ChangeStatus(int id)
        {
            try
            {
                var jobApplication = _context.JobApplications.FirstOrDefault(j => j.Id == id);
                if (jobApplication == null)
                {
                    Console.WriteLine("Function ChangeStatus, jobApplication == null");
                    return;
                }
                bool newStatus = (bool) jobApplication.Status ? false : true;
                jobApplication.Status = newStatus;
                _context.JobApplications.Update(jobApplication);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thay đổi trạng thái Job Application");
            }
        }

        public void DeleteJobApplication(int id)
        {
            try
            {
                var jobApp = _context.JobApplications.FirstOrDefault(j => j.Id == id);
                if (jobApp == null) return;
                _context.JobApplications.Remove(jobApp);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa Job Application");
            }
        }

        public List<JobApplication> FilterJobApplications(FilterJobApplicationParam jobApplicationParam)
        {
            try
            {
            
                List<JobPost> jobPosts = _context.JobPosts.Where(j => j.UserId == jobApplicationParam.UserId).ToList();
                if (!string.IsNullOrEmpty(jobApplicationParam.JobName))
                {
                    string jobName = jobApplicationParam.JobName.ToLower();
                    jobPosts = jobPosts.Where(j => j.JobName.ToLower().Equals(jobName)).ToList();
                }
                List<JobApplication> jobApplications = new List<JobApplication>();
                foreach (JobPost item in jobPosts)
                {
                    List<JobApplication> filJobApplication = _context.JobApplications.Where(ja => ja.JobPostId == item.Id)
                        .Select(ja => new JobApplication {
                            Id = ja.Id,
                            ApplicantId = ja.ApplicantId,
                            JobPostId = ja.JobPostId,
                            Status = ja.Status,
                            CreatedAt = ja.CreatedAt,
                            ProfileName = ja.ProfileName,
                            IsSave = ja.IsSave,
                            JobPost = new JobPost
                            {
                                JobName = ja.JobPost.JobName
                            },
                            ApplicantProfile = null}).ToList();

                    if (jobApplicationParam.Day != 0)
                    {
                        double days = (double) jobApplicationParam.Day * -1;
                        //{ 7 / 19 / 2023 4:25:22 PM}
                        var dateOld = DateTime.Now.AddDays(days);
                        DateTime truncatedDateTime = new DateTime(dateOld.Year, dateOld.Month, dateOld.Day, 0, 0, 0);
                        filJobApplication = filJobApplication.Where(ja => ja.CreatedAt >= truncatedDateTime && ja.CreatedAt <= DateTime.Now).ToList();
                    } else if (jobApplicationParam.Week != 0)
                    {
                        var daysNumber = jobApplicationParam.Week * 7;
                        var dateOld = DateTime.Now.AddDays(daysNumber * -1);
                        DateTime truncatedDateTime = new DateTime(dateOld.Year, dateOld.Month, dateOld.Day, 0, 0, 0);
                        filJobApplication = filJobApplication.Where(ja => ja.CreatedAt >= truncatedDateTime && ja.CreatedAt <= DateTime.Now).ToList();
                    } else if (jobApplicationParam.Month != 0)
                    {
                        var dateOld = DateTime.Now.AddMonths(jobApplicationParam.Month * -1);
                        DateTime truncatedDateTime = new DateTime(dateOld.Year, dateOld.Month, dateOld.Day, 0, 0, 0);
                        filJobApplication = filJobApplication.Where(ja => ja.CreatedAt >= truncatedDateTime && ja.CreatedAt <= DateTime.Now).ToList();
                    }
                    jobApplications.AddRange(filJobApplication);
                }

                return jobApplications;
            } catch (Exception ex)
            {
                Console.WriteLine("Lỗi trong quá trình FilterJobApplicationsByUserId, chi tiết: " + ex.Message);
                return null;
            }
        }

        public List<JobApplication> GetJobApplications()
        {
            try
            {
                return _context.JobApplications.Select(ja => new JobApplication
                {
                    Id = ja.Id,
                    ApplicantId = ja.ApplicantId,
                    JobPostId = ja.JobPostId,
                    Status = ja.Status,
                    CreatedAt = ja.CreatedAt,
                    ProfileName = ja.ProfileName,
                    IsSave = ja.IsSave,
                    JobPost = new JobPost
                    {
                        JobName = ja.JobPost.JobName
                    },
                    ApplicantProfile = null
                }).ToList();

            } catch (Exception ex)
            {
                Console.WriteLine("Có lỗi trong service GetJobApplications. Details: " + ex.Message);
                return null;
            }
        }

        public async Task InsertJobApplication(int appProfileId, int jobPostId)
        {
            ApplicantProfile applicantProfile = _context.ApplicantProfiles.Where(ap=>ap.Id.Equals(appProfileId)).FirstOrDefault();
            JobApplication jobAplly = new JobApplication()
            {
                ApplicantId = appProfileId,
                JobPostId = jobPostId,
                Status = false,
                CreatedAt = DateTime.Now,
                IsSave = true,
                ProfileName = "Hồ sơ ứng tuyển " + applicantProfile.JobName
            };
            _context.JobApplications.Add(jobAplly);
            _context.SaveChanges();
        }
    }
}
