using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Models;

namespace Api.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void CheckVideoExists()
    {
        var video = new Video{
            Id = 4,
            Name = "Oblivion"
        };
        Assert.IsTrue(video.Id == 4);
    }
}