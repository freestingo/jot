namespace Jot.Database.Entities
{
    public class Content
    {
        public string ContentId { get; set; } = string.Empty;
        public string? ChapterIdBookmarked { get; set; }
        public string? BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? Title { get; set; }
        public string? Attribution { get; set; }
    }
}
