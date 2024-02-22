using System.Drawing;

namespace viecLam24hFE.Service.Constant.URL_API
{
    public class URL
    {
        //Ung tuyen
        public const string ALL_JOB_POST = "http://localhost:5000/api/UngTuyen/homepage/AllJobPost";

        /// <summary>
        /// profileName=1&jobType=h&jobLocation=1
        /// </summary>
        public const string SEARCH_JOB_POST = "http://localhost:5000/api/UngTuyen/homepage/SearchJobPost?";

        /// <summary>
        /// exp_level=1&salary=1&jobType=1&jobLocation=1
        /// </summary>
        public const string FILTER_JOB_POST = "http://localhost:5000/api/UngTuyen/searchpage/FilterJobPost?";

        public const string ALL_JOB_TYPE = "http://localhost:5000/api/JobTypes/getAllJobTypes";

        public const string GETDETAIL_JOB_POST = "http://localhost:5000/api/UngTuyen/Detail/GetDetailJobPost?id=";

        public const string SAVE_CV = "http://localhost:5000/api/UngTuyen/textPDF_123?file_name=";

        /// <summary>
        /// userid=312&file_name=1231&jobpostId=213321
        /// </summary>
        public const string SUBMIT_SV = "http://localhost:5000/api/UngTuyen/AddNewApplicantProfile?";

        public const string TAO_HO_SO = "http://localhost:5000/api/UngTuyen/TaoHoSo";

        public const string LIST_HO_SO = "http://localhost:5000/api/UngTuyen/listHoSo?userid=";

        public const string DETAIL_HO_SO = "http://localhost:5000/api/UngTuyen/HoSoDetail?id=";

        public const string XOA_HO_SO = "http://localhost:5000/api/UngTuyen/DeleteHoSo?id=";

        /// <summary>
        /// appProfileId=4&jobPostId=11
        /// </summary>
        public const string APPLY_JOB_HO_SO_SAN = "http://localhost:5000/api/JobApplicant/InsertNewJobApplicant?";

        public const string UPDATE_HO_SO = "http://localhost:5000/api/ApplicantProfile/UpdateApplicantProfiles?applicantProfile=";

        public const string LIST_LOCATION = "http://localhost:5000/api/UngTuyen/ListLocation";
    }
}
