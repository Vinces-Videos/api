using Microsoft.VisualStudio.TestTools.UnitTesting;
using Api.Models;

namespace Api.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var video = new Video{
            Id = 4,
            Name = "Oblivion"
        };
        Console.WriteLine(Name);
        Assert.IsTrue(video.Id == 4);
    }
}