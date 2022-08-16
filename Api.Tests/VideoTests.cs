using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Models;

namespace Api.Tests;

[TestClass]
public class VideoTests
{
    private Video GenerateVideo() => new Video(title);

    [TestMethod]
    //Ensures the video is tagged with a valid category
    public void HasCategoryTag()
    {
        list tags = GenerateVideo().getTags();
    }

    [TestMethod]
    //Ensures the video has a valid due date
    public void HasDueDate()
    {
    }
}