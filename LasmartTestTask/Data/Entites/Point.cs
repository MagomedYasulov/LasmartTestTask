namespace LasmartTestTask.Data.Entites
{
    public class Point : BaseEntity
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }
        public string ColorRGB { get; set; } = string.Empty;
        public List<Comment> Comments { get; set; } = [];
    }
}
