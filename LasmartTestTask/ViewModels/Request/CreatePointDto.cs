using LasmartTestTask.Data.Entites;

namespace LasmartTestTask.ViewModels.Request
{
    public class CreatePointDto
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public string ColorHEX { get; set; } = string.Empty;
        public List<CreateCommentDto> Comments { get; set; } = [];
    }
}
