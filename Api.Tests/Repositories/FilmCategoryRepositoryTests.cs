using MongoDB.Driver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Repositories;
using System.Collections.Generic;
using Models;
using MongoDB.Driver.Linq;
using System.Linq;
using Database;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Tests.Repositories;

[TestClass]
public class FilmCategoryRepositoryTests
{
    private List<FilmCategory> _categories = new List<FilmCategory>
    {
        new FilmCategory
        {
            Id = "ABC123",
            Name = "Category One",
            RatePerDay = 1.20M
        }
    };

    private IQueryable<FilmCategory> _queryableMock;

    private DatabaseItemRepository<FilmCategory> _sut;

    [TestInitialize]
    public void Setup()
    {
        var databaseControllerMock = new Mock<IDatabaseContext>();

        _queryableMock = _categories.AsQueryable();

        databaseControllerMock.Setup(x => x.GetQueryableCollection<FilmCategory>()).Returns(_queryableMock);
        databaseControllerMock.Setup(x => x.GetById<FilmCategory>(It.IsAny<string>())).Returns((string id) => _categories.First(x => x.Id == id));
        var databaseController = databaseControllerMock.Object;

        var cacheOptions = new MemoryCacheOptions
        {
            SizeLimit = 1000, 
            ExpirationScanFrequency = System.TimeSpan.FromSeconds(10)
        };
        _sut = new DatabaseItemRepository<FilmCategory>(databaseController, cacheOptions);
    }

    [TestMethod]
    public void Get()
    {
        //Arrange
        var expectedCategory = _categories.First();

        //Act
        var actualCategory = _sut.Get(expectedCategory.Id); 

        //Assert
        Assert.AreEqual(expectedCategory.Id, actualCategory.Id);
        Assert.AreEqual(expectedCategory.Name, actualCategory.Name);
        Assert.AreEqual(expectedCategory.RatePerDay, actualCategory.RatePerDay);
    }
}