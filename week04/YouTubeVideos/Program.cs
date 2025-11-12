using System;
using System.Collections.Generic;

// Comment class - stores a name and text
public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

// Video class - stores video info and its comments
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; }
    private List<Comment> Comments = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    public void Display()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (Comment c in Comments)
        {
            Console.WriteLine($" - {c.Name}: {c.Text}");
        }
        Console.WriteLine();
    }
}

// Main program - creates and displays videos
class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video v1 = new Video("C# Basics", "CodeAcademy", 600);
        v1.AddComment(new Comment("Alice", "Great explanation!"));
        v1.AddComment(new Comment("Bob", "Very clear and helpful."));
        v1.AddComment(new Comment("Charlie", "Thanks for the tips!"));
        videos.Add(v1);

        Video v2 = new Video("Understanding Abstraction", "DevSimplified", 480);
        v2.AddComment(new Comment("Diana", "Nice example of classes."));
        v2.AddComment(new Comment("Evan", "Short and sweet."));
        v2.AddComment(new Comment("Faith", "Loved this tutorial."));
        videos.Add(v2);

        Video v3 = new Video("OOP in C#", "TechWorld", 720);
        v3.AddComment(new Comment("George", "Helped me with my assignment."));
        v3.AddComment(new Comment("Hannah", "Could you cover inheritance next?"));
        videos.Add(v3);

        // Display all videos
        foreach (Video v in videos)
        {
            v.Display();
        }
    }
}
