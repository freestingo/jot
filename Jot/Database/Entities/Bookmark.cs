namespace Jot.Database.Entities
{
    public class Bookmark
    {
        public Guid BookmarkId { get; set; }
        public string VolumeId { get; set; } = string.Empty;
        public string ContentId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Text { get; set; }
        public string? Annotation { get; set; }
        public string? ContextString { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
