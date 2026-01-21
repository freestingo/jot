using Jot.Database;
using Jot.Database.Entities;
using Microsoft.EntityFrameworkCore;

using var db = new KoboContext();
string[] newlineChars = ["\r\n", "\r", "\n"];
var contents = db.Contents
    .AsNoTracking()
    .ToArray();
var bookmarks = db.Bookmarks
    .AsNoTracking()
    .Where(x => x.Type != "markup") // TODO: check if it's possible to somehow still export markups (maybe check ExtraAnnotationData field)
    .ToArray();
var books = bookmarks
    .GroupBy(GetBookTitle)
    .OrderByDescending(book => book
        .OrderByDescending(bookmark => bookmark.DateCreated)
        .First()
        .DateCreated)
    .Select(book => new
    {
        book.Key.BookTitle,
        book.Key.FileName,
        Chapters = book
            .GroupBy(GetChapterTitle)
            .OrderBy(chapterGroup => chapterGroup
                .OrderBy(bookmark => bookmark.DateCreated)
                .First()
                .DateCreated),
    })
    .ToArray();

foreach (var book in books)
{
    Console.WriteLine($"Jotting annotations for book {book.BookTitle}...");
    var outputDirectory = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), $"annotations_{DateTime.Now:yyyy-MM-dd}"));
    var filePath = Path.Combine(outputDirectory.FullName, book.FileName);
    var fileContent = @$"# {book.BookTitle}

{string.Join(
    Environment.NewLine,
    book.Chapters.Select(chapter => @$"## {chapter.Key}

{string.Join(
        $"{Environment.NewLine}{Environment.NewLine}---{Environment.NewLine}{Environment.NewLine}",
        chapter.Select(bookmark => bookmark.Type switch
            {
                "dogear" => $"Bookmarked the following context: {bookmark.ContextString}{Environment.NewLine}{Environment.NewLine}###### Annotated on {bookmark.DateCreated.ToShortDateString()}",
                "highlight" when !string.IsNullOrEmpty(bookmark.Text) => $"> {string.Join(" ", bookmark.Text.Split(newlineChars, StringSplitOptions.TrimEntries))}{Environment.NewLine}{Environment.NewLine}###### Annotated on {bookmark.DateCreated.ToShortDateString()}",
                "note" when !string.IsNullOrEmpty(bookmark.Text) => @$"> {string.Join(" ", bookmark.Text.Split(newlineChars, StringSplitOptions.TrimEntries))}{Environment.NewLine}{Environment.NewLine}{bookmark.Annotation}{Environment.NewLine}{Environment.NewLine}###### Annotated on {bookmark.DateCreated.ToShortDateString()}",
                _ => $"Unknown bookmark type (content id: {bookmark.ContentId}",
            })
        .ToArray())}
"))}";

    File.WriteAllText(filePath, fileContent);
    Console.WriteLine($"Annotations file created successfully at path {filePath}");
}

Console.WriteLine("Annotations jotted successfully! Press any key to close this program.");
Console.ReadKey();

(string BookTitle, string FileName) GetBookTitle(Bookmark bookmark)
{
    var title = contents
        .First(content => content.BookId == bookmark.VolumeId)
        .BookTitle;
    var author = contents
        .First(content => content.ContentId == bookmark.VolumeId)
        .Attribution;

    return ($"{title}, {author}", Path.ChangeExtension(bookmark.VolumeId.Split('/').Last(), "md"));
}

string GetChapterTitle(Bookmark bookmark)
{
    return CurrentChapterTitle() ?? NearestChapterTitle() ?? string.Empty;

    string? CurrentChapterTitle() => contents
        .FirstOrDefault(c => c.ChapterIdBookmarked == bookmark.ContentId)
        ?.Title;
    string? NearestChapterTitle() => contents
        .Where(c => c.BookId == bookmark.VolumeId)
        .OrderBy(c => c.ContentId)
        .TakeWhile(c => !c.ContentId.Contains(bookmark.ContentId))
        .OrderByDescending(c => c.ContentId)
        .First(c => c.Title != null && c.ChapterIdBookmarked != null)
        .Title;
}

