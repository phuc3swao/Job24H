namespace viecLam24hBE.Services
{
    public class HardValue
    {
        public static readonly List<Tuple<string, string>> EXP_Level = new List<Tuple<string, string>>
        {
            Tuple.Create("Tất cả kinh nghiệm", "All"),
            Tuple.Create("Chưa có kinh nghiệm", "0"),
            Tuple.Create("Dưới 1 năm", "<1"),
            Tuple.Create("1 năm", "1"),
            Tuple.Create("2 năm", "2"),
            Tuple.Create("3 năm", "3"),
            Tuple.Create("4 năm", "4"),
            Tuple.Create("5 năm", "5"),
            Tuple.Create("Hơn 5 năm", ">5")
        };

        public static readonly List<Tuple<string, double, double>> SALARY_Level = new List<Tuple<string, double, double>>
        {
            Tuple.Create("Tất cả mức lương", 0.0, double.MaxValue),
            Tuple.Create("1-5 triệu", 1000000.0, 5000000.0),
            Tuple.Create("5-10 triệu", 5000000.0, 10000000.0),
            Tuple.Create("10-15 triệu", 10000000.0, 15000000.0),
            Tuple.Create("15-25 triệu", 15000000.0, 25000000.0),
            Tuple.Create("25-40 triệu", 25000000.0, 40000000.0),
            Tuple.Create("Trên 40 triệu", 40000000.0, double.MaxValue),
            //trong db nếu salary = 0 thì coi như là thõa thuận
            Tuple.Create("Thỏa thuận", 0.0, 0.0)
        };


    }
}


