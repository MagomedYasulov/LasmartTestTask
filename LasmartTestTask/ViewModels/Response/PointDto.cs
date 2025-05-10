using LasmartTestTask.Data.Entites;

namespace LasmartTestTask.ViewModels.Response
{
    public class PointDto 
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public string ColorHEX { get; set; } = string.Empty;
        public List<CommentDto> Comments { get; set; } = [];
    }
}
