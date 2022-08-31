using MongoDB.Driver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Repositories;
using System.Collections.Generic;
using Models;
using MongoDB.Driver.Linq;
using System.Linq;
using Database;
namespace Api.Tests.Repositories;

[TestClass]
public class RepositoriesTests
{
    private List<Product> _products = new List<Product>
    {
        new Product
        {
            Id = "1",
            Name = "Product 1",
            StockCount = 5
        }
    };

    private Api.Tests.Database.MongoQueryable<Product> _productsQueryableMock = new Api.Tests.Database.MongoQueryable<Product>();

    private Products _sut;

    [TestInitialize]
    public void Setup()
    {
        var databaseControllerMock = new Mock<IDatabaseController>();

        _productsQueryableMock = new Api.Tests.Database.MongoQueryable<Product>();
        _productsQueryableMock.MockData = _products;

        databaseControllerMock.Setup(x => x.GetQueryableCollection<Product>()).Returns(_productsQueryableMock);
        databaseControllerMock.Setup(x => x.GetById<Product>(It.IsAny<string>())).Returns((string id) => _products.First(x => x.Id == id));
        var databaseController = databaseControllerMock.Object;

        _sut = new Products(databaseController);
    }

    [TestMethod]
    public void Get()
    {
        //Arrange
        var expectedProduct = _products.First();

        //Act
        var actualProduct = _sut.Get(expectedProduct.Id); 

        //Assert
        Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
        Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
        Assert.AreEqual(expectedProduct.StockCount, actualProduct.StockCount);
    }
}