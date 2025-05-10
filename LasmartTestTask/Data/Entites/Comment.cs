namespace LasmartTestTask.Data.Entites
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public string ColorHEX { get; set; } = string.Empty;

        public int PointId { get; set; }
        public Point Point { get; set; }
    }
}
