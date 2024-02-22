using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Reflection.Metadata.Ecma335;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public static class UngTuyenDAO
    {
        public static List<JobPost> getAllJobPosts()
        {
            using (var db = new MyDbContext())
            {
                var result = from jobpost in db.JobPosts
                             join jobtype in db.JobTypes on jobpost.JobTypeId equals jobtype.Id
                             join user in db.Users on jobpost.UserId equals user.Id
                             select new JobPost
                             {
                                 Id = jobpost.Id,
                                 UserId = jobpost.UserId,
                                 JobTypeId = jobpost.JobTypeId,
                                 CreatedDate = jobpost.CreatedDate,
                                 JobDescription = jobpost.JobDescription,
                                 JobLocation = jobpost.JobLocation,
                                 Status = jobpost.Status,
                                 Salary = jobpost.Salary,
                                 Experence = jobpost.Experence,
                                 JobName = jobpost.JobName,
                                 WorkingForm = jobpost.WorkingForm,
                                 Degree = jobpost.Degree,
                                 MinAge = jobpost.MinAge,
                                 MaxAge = jobpost.MaxAge,
                                 Sex = jobpost.Sex,
                                 Quantity = jobpost.Quantity,
                                 UserName = jobpost.UserName,
                                 Email = jobpost.Email,
                                 Phone = jobpost.Phone,
                                 Address = jobpost.Address,
                                 Deadline = jobpost.Deadline,
                                 JobType = jobtype,
                                 User = user,
                                 JobApplications = null
                             };
                return result.ToList();
            }
        }

        // Search tren home page
        public static List<JobPost> searchJobPosts(string jobName, string jobType, string jobLocation)
        {
            using (var db = new MyDbContext())
            {
                var result = (from jobpost in db.JobPosts
                              join jobtype in db.JobTypes on jobpost.JobTypeId equals jobtype.Id
                              join user in db.Users on jobpost.UserId equals user.Id
                              where jobpost.JobName.ToUpper().Contains(jobName.ToUpper())
                           && jobpost.JobType.Type.Contains(jobType)
                           && jobpost.JobLocation.Contains(jobLocation)
                              select new JobPost
                              {
                                  Id = jobpost.Id,
                                  UserId = jobpost.UserId,
                                  JobTypeId = jobpost.JobTypeId,
                                  CreatedDate = jobpost.CreatedDate,
                                  JobDescription = jobpost.JobDescription,
                                  JobLocation = jobpost.JobLocation,
                                  Status = jobpost.Status,
                                  Salary = jobpost.Salary,
                                  Experence = jobpost.Experence,
                                  JobName = jobpost.JobName,
                                  WorkingForm = jobpost.WorkingForm,
                                  Degree = jobpost.Degree,
                                  MinAge = jobpost.MinAge,
                                  MaxAge = jobpost.MaxAge,
                                  Sex = jobpost.Sex,
                                  Quantity = jobpost.Quantity,
                                  UserName = jobpost.UserName,
                                  Email = jobpost.Email,
                                  Phone = jobpost.Phone,
                                  Address = jobpost.Address,
                                  Deadline = jobpost.Deadline,
                                  JobType = jobtype,
                                  User = user,
                                  JobApplications = null
                              }).ToList();
                return result;
            }
        }

        // Cong viec can gap
        public static List<JobPost> UrgentJobs()
        {
            using (var db = new MyDbContext())
            {
                // cần thảo luận 
            }
            return null;
        }

        public static List<JobPost> jobPostsFilter(string exp_level, string salary, string jobType, string jobLocation)
        {
            using (var db = new MyDbContext())
            {
                var result = (from jobpost in db.JobPosts
                              join jobtype in db.JobTypes on jobpost.JobTypeId equals jobtype.Id
                              join user in db.Users on jobpost.UserId equals user.Id
                              where jobtype.Type.ToUpper().Contains(jobType.ToUpper())
                              && jobpost.JobLocation.ToUpper().Contains(jobLocation.ToUpper())
                              select new JobPost
                              {
                                  Id = jobpost.Id,
                                  UserId = jobpost.UserId,
                                  JobTypeId = jobpost.JobTypeId,
                                  CreatedDate = jobpost.CreatedDate,
                                  JobDescription = jobpost.JobDescription,
                                  JobLocation = jobpost.JobLocation,
                                  Status = jobpost.Status,
                                  Salary = jobpost.Salary,
                                  Experence = jobpost.Experence,
                                  JobName = jobpost.JobName,
                                  WorkingForm = jobpost.WorkingForm,
                                  Degree = jobpost.Degree,
                                  MinAge = jobpost.MinAge,
                                  MaxAge = jobpost.MaxAge,
                                  Sex = jobpost.Sex,
                                  Quantity = jobpost.Quantity,
                                  UserName = jobpost.UserName,
                                  Email = jobpost.Email,
                                  Phone = jobpost.Phone,
                                  Address = jobpost.Address,
                                  Deadline = jobpost.Deadline,
                                  JobType = jobtype,
                                  User = user,
                                  JobApplications = null
                              });
                foreach (var explevel in HardValue.EXP_Level)
                {
                    if (exp_level.Equals(explevel.Item1) && explevel.Item2.Equals("All"))
                    {
                        break;
                    }
                    else
                    {
                        result.Where(j => j.Experence.Equals(explevel.Item1));
                    }
                }
                foreach (var salarylv in HardValue.SALARY_Level)
                {
                    if (salary.Equals(salarylv.Item1))
                    {
                        result.Where(j => salarylv.Item2 <= j.Salary && j.Salary <= salarylv.Item3);
                        break;
                    }
                }
                return result.ToList();
            }
        }

        public static List<Tuple<string, string>> getListLevel() => HardValue.EXP_Level;

        public static List<Tuple<string, double, double>> getListSalary() => HardValue.SALARY_Level;

        internal static List<JobPost> searchJobPosts(object profileName, string jobType, string jobLocation)
        {
            throw new NotImplementedException();
        }

        public static JobPost GetDetailJobPost(int id)
        {
            using (var db = new MyDbContext())
            {
                var result = (from jobpost in db.JobPosts
                              join jobtype in db.JobTypes on jobpost.JobTypeId equals jobtype.Id
                              join user in db.Users on jobpost.UserId equals user.Id
                              where jobpost.Id == id
                              select new JobPost
                              {
                                  Id = jobpost.Id,
                                  UserId = jobpost.UserId,
                                  JobTypeId = jobpost.JobTypeId,
                                  CreatedDate = jobpost.CreatedDate,
                                  JobDescription = jobpost.JobDescription,
                                  JobLocation = jobpost.JobLocation,
                                  Status = jobpost.Status,
                                  Salary = jobpost.Salary,
                                  Experence = jobpost.Experence,
                                  JobName = jobpost.JobName,
                                  WorkingForm = jobpost.WorkingForm,
                                  Degree = jobpost.Degree,
                                  MinAge = jobpost.MinAge,
                                  MaxAge = jobpost.MaxAge,
                                  Sex = jobpost.Sex,
                                  Quantity = jobpost.Quantity,
                                  UserName = jobpost.UserName,
                                  Email = jobpost.Email,
                                  Phone = jobpost.Phone,
                                  Address = jobpost.Address,
                                  Deadline = jobpost.Deadline,
                                  JobType = jobtype,
                                  User = user,
                                  JobApplications = null
                              }).FirstOrDefault();
                return result;
            }
        }

        //submit CV
        public static void SubmitCv(int userid, string filename, int jobpostId)
        {
            ApplicantProfile newApplicantProfile = new ApplicantProfile()
            {
                UserId = userid,
                CV_name = filename,
            };
            using (var db = new MyDbContext())
            {
                try
                {
                    db.ApplicantProfiles.Add(newApplicantProfile);
                    db.SaveChanges();

                    string HoSoSo = (db.ApplicantProfiles.Where(u => u.UserId == userid).ToList().Count + 1).ToString();
                    JobApplication newJobApplication = new JobApplication()
                    {
                        ApplicantId = newApplicantProfile.Id,
                        JobPostId = jobpostId,
                        Status = true,
                        CreatedAt = DateTime.Now,
                        IsSave = true,
                        ProfileName = "Hồ sơ số " + HoSoSo
                    };

                    db.JobApplications.Add(newJobApplication);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }

        //Tao CV
        public static void InsertApplicantProfile(ApplicantProfile applicantProfile)
        {
            try
            {
                using (var db = new MyDbContext())
                {
                    db.ApplicantProfiles.Add(applicantProfile);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        //list Ho so
        public static List<ApplicantProfile> ListHoSo(int userid)
        {
            using (var db = new MyDbContext())
            {
                var result = db.ApplicantProfiles.Where(ur => ur.UserId == userid).ToList();
                return result;
            };
        }

        //get ho so
        public static ApplicantProfile GetHoSo(int id)
        {
            using (var db = new MyDbContext())
            {
                var result = db.ApplicantProfiles.Where(ur => ur.Id == id).FirstOrDefault();
                return result;
            };
        }

        //xoa ho so
        public static void DeleteHoSo(int id)
        {
            using (var db = new MyDbContext())
            {
                ApplicantProfile applicant = GetHoSo(id);
                if (applicant != null)
                {
                    try
                    {
                        List<JobApplication> jobApplications = GetListJobApplicantBy_IdHoSo(id);
                        if(jobApplications != null)
                        {
                            foreach(var job in jobApplications)
                            {
                                db.JobApplications.Remove(job);
                            }
                        }
                        db.ApplicantProfiles.Remove(applicant);
                        db.SaveChanges();
                    }catch(Exception ex)
                    {

                    }
                }
            }
        }

        public static List<JobApplication> GetListJobApplicantBy_IdHoSo(int applicantPrifleId)
        {
            using (var db = new MyDbContext())
            {
                var result = db.JobApplications.Where(ja => ja.ApplicantId == applicantPrifleId).ToList();
                return result;
            }
        }

        public static List<Tuple<string, int>> ListLocation()
        {
            List<Tuple<string, int>> listLocation = new List<Tuple<string, int>>();
            using(var db = new MyDbContext())
            {
                var result = (from job in db.JobPosts
                              where job.Status.Equals(true)
                              group job by job.JobLocation into locationGroup
                              orderby locationGroup.Count() descending
                              select new Tuple<string, int>(locationGroup.Key, locationGroup.Count()))
                    .ToList();
                listLocation.AddRange(result);
                return listLocation;
            }
        }
    }
}
