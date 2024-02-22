namespace viecLam24hBE.Params
{
    public class FilterJobApplicationParam
    {
        public int UserId { get; set; }
        public string? JobName { get; set; } = null!;
        public int Day { get; set; } = 0;
        public int Week { get; set; } = 0;
        public int Month { get; set; } = 0;

    }
}
