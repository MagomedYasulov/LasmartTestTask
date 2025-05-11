namespace LasmartTestTask.ViewModels.Request
{
    public class UpdatePointDto
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public string ColorHEX { get; set; } = string.Empty;
        //public List<UpdateCommentDto> Comments { get; set; } = [];
    }
}
