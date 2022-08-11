using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Models;

namespace Api.Tests;

[TestClass]
public class VideoTests
{
    [TestMethod]
    public void IsCreateSuccessful()
    {
        var video = new Video(4, "The Neverending Story");
        Assert.IsNotNull(video);
    }
}