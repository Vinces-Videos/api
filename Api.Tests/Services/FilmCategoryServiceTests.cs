using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Moq;
using Repositories;
using Models;
using System.Collections.Generic;
using System.Linq;
using Database.Mongo;
using Database;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Tests.Services;

[TestClass]
public class FilmCategoryServiceTests
{
    private FilmCategoryService _sut;
    private Mock<IDatabaseItemRepository<FilmCategory>> _databaseItemRepositoryMock;

    private List<FilmCategory> _filmCategoriesTestData;

    [TestInitialize]
    public void Setup()
    {
        var databaseContextMock = new Mock<IDatabaseContext>();
        var memoryCacheOptions = new MemoryCacheOptions 
        {
            SizeLimit = 1000,
            ExpirationScanFrequency = System.TimeSpan.FromSeconds(10)
        };
        _databaseItemRepositoryMock = new Mock<IDatabaseItemRepository<FilmCategory>>();
        _filmCategoriesTestData = new List<FilmCategory>
        {
            new FilmCategory
            {
                Id = "1",
                Name = FilmCategories.NewRelease,
                RatePerDay = 1.5M
            },
            new FilmCategory
            {
                Id = "2",
                Name = FilmCategories.Childrens,
                RatePerDay = 0.5M
            },
            new FilmCategory
            {
                Id = "3",
                Name = FilmCategories.Regular,
                RatePerDay = 1.0M
            }
        };

        _databaseItemRepositoryMock.Setup(x => x.Get(It.Is<string>(s => s.Equals(_filmCategoriesTestData[0].Id)))).Returns(_filmCategoriesTestData[0]);
        _databaseItemRepositoryMock.Setup(x => x.Get(It.Is<string>(s => s.Equals(_filmCategoriesTestData[1].Id)))).Returns(_filmCategoriesTestData[1]);
        _databaseItemRepositoryMock.Setup(x => x.Get(It.Is<string>(s => s.Equals(_filmCategoriesTestData[2].Id)))).Returns(_filmCategoriesTestData[2]);
    }


    [TestMethod]
    public void GetByCategoryIdTest()
    {
        // Arrange
        var testId = "1";
        var expected = _filmCategoriesTestData.First(x => x.Id == testId);
        _sut = new FilmCategoryService(_databaseItemRepositoryMock.Object);

        // Act
        var actual = _sut.Get(testId);

        // Assert
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected.Id, actual.Id);
        Assert.AreEqual(expected.Name, actual.Name);
        Assert.AreEqual(expected.RatePerDay, actual.RatePerDay);
    }
}

public static class FilmCategories
{
    public static string NewRelease = "New Release";
    public static string Regular = "Regular";
    public static string Childrens = "Childrens";
}