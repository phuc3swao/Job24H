namespace viecLam24hBE.Commons
{
    public class Enums
    {
        public static readonly int TUYEN_DUNG_ROLE = 1;
        public static readonly int UNG_TUYEN_ROLE = 2;
        public static readonly int ADMIN_ROLE = 3;
        public static readonly string SESSION_KEY_USER = "_User";
        public static readonly string SESSION_KEY_USER_UNG_TUYEN = "_UserUngTuyen";
        public static readonly string SESSION_KEY_ROLE = "_Role";
        public static readonly string SESSION_KEY_JOBPOST = "_JobPost";

        public Enums()
        {

        }

        public static string GetValueFromKey(List<Tuple<string, string>> lst, string item1)
        {
            string val = "";
            Tuple<string, string> find = lst.FirstOrDefault(i => i.Item1.Equals(item1));
            if (find != null)
            {
                val = find.Item2;
            }
            return val;
        }

        public static readonly List<Tuple<string, string>> SexEnums = new List<Tuple<string, string>>
        {
            Tuple.Create("2", "Không yêu cầu"),
            Tuple.Create("0", "Nam"),
            Tuple.Create("1", "Nữ"),
        };

        public static readonly List<Tuple<string, string>> WorkingFormEnums = new List<Tuple<string, string>>
        {
            Tuple.Create("0", "Toàn thời gian cố định"),
            Tuple.Create("1", "Toàn thời gian tạm thời"),
            Tuple.Create("2", "Bán thời gian cố định"),
            Tuple.Create("3", "Bán thời gian tạm thời"),
            Tuple.Create("4", "Theo hợp đồng tư vấn"),
            Tuple.Create("5", "Thực tập"),
            Tuple.Create("6", "Khác"),
        };

        public static readonly List<Tuple<string, string>> DegreeEnums = new List<Tuple<string, string>>
        {
            Tuple.Create("0", "Trên đại học"),
            Tuple.Create("1", "Toàn thời gian tạm thời"),
            Tuple.Create("2", "Đại học"),
            Tuple.Create("3", "Cao đẳng"),
            Tuple.Create("4", "Trung cấp"),
            Tuple.Create("5", "Trung học"),
            Tuple.Create("6", "Chứng chỉ"),
            Tuple.Create("7", "Không yêu cầu"),
        };

        public static readonly List<Tuple<string, string>> ExperenceEnums = new List<Tuple<string, string>>
        {
            Tuple.Create("0", "Chưa có kinh nghiệm"),
            Tuple.Create("1", "Dưới 1 năm"),
            Tuple.Create("2", "1 năm"),
            Tuple.Create("3", "2 năm"),
            Tuple.Create("4", "3 năm"),
            Tuple.Create("5", "4 năm"),
            Tuple.Create("6", "5 năm"),
            Tuple.Create("7", "Hơn 5 năm"),
        };

        public static readonly List<Tuple<string, string>> TimeSubmitEnums = new List<Tuple<string, string>>
        {
            Tuple.Create("3 day", "3 ngày"),
            Tuple.Create("1 week", "1 tuần"),
            Tuple.Create("1 month", "1 tháng"),
            Tuple.Create("2 month", "2 tháng"),
            Tuple.Create("3 month", "3 tháng"),
            Tuple.Create("4 month", "4 tháng"),
            Tuple.Create("5 month", "5 tháng"),
            Tuple.Create("6 month", "6 tháng"),
        };
    }

   
}
