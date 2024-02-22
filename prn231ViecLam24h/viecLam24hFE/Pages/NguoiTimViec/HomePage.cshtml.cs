using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using viecLam24hBE.Models;
using viecLam24hFE.Service.CallAPI;
using viecLam24hFE.Service.Constant.URL_API;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class TimViecModel : PageModel
    {
        public static List<JobPost> jobPosts_DB { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<JobPost> jobPost_Show { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<JobType> listJobtype { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Tuple<string, int>> listLocation { get; set; }

        /// <summary>
        /// search
        /// </summary>
        /// <returns></returns>
        [BindProperty(SupportsGet = true)]
        public string jobName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string jobLocation { get; set; }
        [BindProperty(SupportsGet = true)]
        public string jobType { get; set; }

        public TimViecModel()
        {

        }

        public async Task OnGet()
        {
            jobPosts_DB = await ApiHelper.GetAsync<List<JobPost>>(URL.ALL_JOB_POST);
            await LoadData();
        }

        public async Task OnGetByJobType(string type)
        {
            jobPosts_DB = await ApiHelper.GetAsync<List<JobPost>>(URL.SEARCH_JOB_POST + "jobType=" + type);
            await LoadData();
        }

        public async Task OnGetByJobLocation(string joblocation)
        {
            jobPosts_DB = await ApiHelper.GetAsync<List<JobPost>>(URL.SEARCH_JOB_POST + "jobLocation=" + joblocation);
            await LoadData();
        }

        public async Task OnGetRecent(int option)
        {
            jobPosts_DB = await ApiHelper.GetAsync<List<JobPost>>(URL.ALL_JOB_POST);

            switch (option)
            {
                case 1:
                    jobPosts_DB = jobPosts_DB.OrderByDescending(jb => jb.CreatedDate).ToList();
                    break;
                case 2:
                    jobPosts_DB = jobPosts_DB.Where(jb => jb.WorkingForm.ToUpper().Contains("Fulltime".ToUpper())).ToList();
                    break;
                case 3:
                    jobPosts_DB = jobPosts_DB.Where(jb => jb.Experence.ToUpper().Contains("Intern".ToUpper())).ToList();
                    break;
                case 4:
                    jobPosts_DB = jobPosts_DB.Where(jb => jb.WorkingForm.ToUpper().Contains("Parttime".ToUpper())).ToList();
                    break;
            }

            await LoadData();
        }

        public async Task OnPostSearch()
        {
            var url = URL.SEARCH_JOB_POST + "profileName=" + jobName + "&jobType=" + jobType + "&jobLocation=" + jobLocation;
            jobPosts_DB = await ApiHelper.GetAsync<List<JobPost>>(url);
            await LoadData();
        }


        public async Task LoadData()
        {
            jobPost_Show = jobPosts_DB.Where(jb=>jb.Status==true).Take(8).ToList();
            await SubLoad();
        }

        public async Task SubLoad()
        {
            listLocation = await ApiHelper.GetAsync<List<Tuple<string, int>>>(URL.LIST_LOCATION);

            listJobtype = await ApiHelper.GetAsync<List<JobType>>(URL.ALL_JOB_TYPE);
        }

        public async Task OnGetLoadMore(int size)
        {
            jobPost_Show = jobPosts_DB.Where(jb=>jb.Status==true).Take(size+5).ToList();
            await SubLoad();
        }
    }
}
