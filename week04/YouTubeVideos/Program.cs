using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video(
            "Learning C# Basics",
            "John Smith",
            420
        );

        video1._comments.Add(
            new Comment("Mary", "This video was very helpful!")
        );

        video1._comments.Add(
            new Comment("David", "Great explanation of C#.")
        );

        video1._comments.Add(
            new Comment("Sarah", "I finally understand the basics.")
        );

        Video video2 = new Video(
            "Understanding Object-Oriented Programming",
            "Jane Doe",
            615
        );

        video2._comments.Add(
            new Comment("Peter", "This made OOP much easier to understand.")
        );

        video2._comments.Add(
            new Comment("Grace", "I really liked the examples.")
        );

        video2._comments.Add(
            new Comment("Michael", "Very clear explanation.")
        );

        Video video3 = new Video(
            "C# Classes and Objects",
            "Daniel Brown",
            530
        );

        video3._comments.Add(
            new Comment("Emma", "Classes make more sense to me now.")
        );

        video3._comments.Add(
            new Comment("James", "Thanks for explaining objects clearly.")
        );

        video3._comments.Add(
            new Comment("Sophia", "This helped me with my assignment.")
        );

        Video video4 = new Video(
            "Introduction to Abstraction",
            "Rachel Green",
            480
        );

        video4._comments.Add(
            new Comment("Samuel", "Abstraction is much clearer now.")
        );

        video4._comments.Add(
            new Comment("Elizabeth", "Great example and explanation.")
        );

        video4._comments.Add(
            new Comment("Daniel", "This was easy to follow.")
        );

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);
        videos.Add(video4);

        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video._title}");
            Console.WriteLine($"Author: {video._author}");
            Console.WriteLine($"Length: {video._length} seconds");
            Console.WriteLine(
                $"Number of Comments: {video.GetNumberOfComments()}"
            );

            Console.WriteLine("Comments:");

            foreach (Comment comment in video._comments)
            {
                Console.WriteLine(
                    $"{comment._name}: {comment._text}"
                );
            }

            Console.WriteLine();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
        }
    }
}