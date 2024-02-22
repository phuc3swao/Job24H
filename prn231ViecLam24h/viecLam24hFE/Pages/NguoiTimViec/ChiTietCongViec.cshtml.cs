using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using viecLam24hBE.Models;
using viecLam24hFE.Service.CallAPI;
using viecLam24hFE.Service.Constant.URL_API;

namespace viecLam24hFE.Pages.NguoiTimViec
{
    public class ChiTietCongViecModel : PageModel
    {
        [BindProperty]
        public JobPost jobPost { get; set; }

        public static int jobPostId { get; set; }

        public async Task OnGet(int id)
        {
            jobPostId = id;
            await LoadData();
        }

        public async Task LoadData()
        {
            jobPost = await ApiHelper.GetAsync<JobPost>(URL.GETDETAIL_JOB_POST + jobPostId);
        }
    }
}
