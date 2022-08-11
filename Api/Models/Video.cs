namespace Api.Models;

public class Video
{
    public int Id { get; }
    public string Name { get; }

    public Video(int id, string name)
    {
        Id = id;
        Name = name;
    }
}