using Microsoft.EntityFrameworkCore;
using viecLam24hBE.Commons;
using viecLam24hBE.Models;

namespace viecLam24hBE.Services
{
    public class JobPostServiceImpl : JobPostService
    {
        private readonly MyDbContext _context;
        private readonly JobTypeService _jobTypeService;
        private readonly UserService _userService;

        public JobPostServiceImpl(MyDbContext context, JobTypeService jobTypeService, UserService userService)
        {
            _context = context;
            _jobTypeService = jobTypeService;
            _userService = userService;
        }

        public bool AddJobPost(JobPost jobPost)
        {
            try
            {
                if (jobPost == null) return false;

                _context.JobPosts.Add(jobPost);
                _context.SaveChanges();

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public void ChangeStatus(int id)
        {
            try
            {
                var JobPost = _context.JobPosts.FirstOrDefault(j => j.Id == id);
                if (JobPost == null)
                {
                    Console.WriteLine("Function ChangeStatus, JobPost == null");
                    return;
                }
                bool newStatus = JobPost.Status? false : true;
                JobPost.Status = newStatus;
                _context.JobPosts.Update(JobPost);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thay đổi trạng thái Job Post");
            }
        }

        public void DeleteJobPost(int id)
        {
            try
            {
                var jobPost = _context.JobPosts.FirstOrDefault(j => j.Id == id);
                if (jobPost == null) return;
                _context.JobPosts.Remove(jobPost);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa Job Post");
            }
        }

        public bool EditJobPost(JobPost jobPost)
        {
            try
            {
                if (jobPost == null) return false;

                _context.JobPosts.Update(jobPost);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public JobPost GetJobPostById(int id)
        {
            try
            {
                var jobPost = _context.JobPosts.FirstOrDefault(j => j.Id == id);
                //var jobType = _jobTypeService.GetJobTypeById(jobPost.JobTypeId);
                //var user = _userService.GetUserById(jobPost.UserId);
                //jobPost.JobType = jobType;
                //jobPost.User = user;
                
                return jobPost;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<JobPost> GetJobsPosts()
        {
            try
            {
                return _context.JobPosts.ToList();
            } catch (Exception ex)
            {
                return null;
            }
        }

        public void UpdateDeadline(int id, DateTime newDealine)
        {
            try
            {
                var jobPost = _context.JobPosts.FirstOrDefault(j => j.Id == id);
                if (jobPost == null) return;
                jobPost.Deadline = newDealine;
                _context.JobPosts.Update(jobPost);
                _context.SaveChanges();

            } catch(Exception ex)
            {
                Console.WriteLine("Lỗi khi update deadline");
            }
        }
    }
}
