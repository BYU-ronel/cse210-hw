using System;
using System.Collections.Generic;

public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }

    public override string ToString()
    {
        return $"Comment by {Name}: {Text}";
    }
}

public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumComments()
    {
        return Comments.Count;
    }

    public override string ToString()
    {
        string commentsStr = string.Join("\n", Comments);
        return $"Title: {Title}\nAuthor: {Author}\nLength: {Length} seconds\nNumber of comments: {GetNumComments()}\nComments:\n{commentsStr}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("Video 1", "Author 1", 300);
        video1.AddComment(new Comment("User 1", "This is a great video!"));
        video1.AddComment(new Comment("User 2", "I agree!"));
        video1.AddComment(new Comment("User 3", "This video is amazing!"));
        videos.Add(video1);

        Video video2 = new Video("Video 2", "Author 2", 240);
        video2.AddComment(new Comment("User 4", "This video is okay."));
        video2.AddComment(new Comment("User 5", "I didn't like it."));
        videos.Add(video2);

        Video video3 = new Video("Video 3", "Author 3", 360);
        video3.AddComment(new Comment("User 6", "This video is the best!"));
        video3.AddComment(new Comment("User 7", "I loved it!"));
        video3.AddComment(new Comment("User 8", "This video is amazing!"));
        videos.Add(video3);

        foreach (var video in videos)
        {
            Console.WriteLine(video);
            Console.WriteLine();
        }
    }
}