using Moq;
using Testing_Basics;
using static Testing_Basics.Shopping_Cart;

public class ShoppingCartTest
{
    //public class DbServiceMock : IDbService
    //{
    //    public bool ProcessResult { get; set; }
    //    public Product ? ProductBeingProcessed { get; set; }
    //    public int ProductIdBeingProcessed { get; set; }

    //    public bool RemoveShoppingCartItem(int? id)
    //    {
    //        if (id != null)
    //            ProductIdBeingProcessed = Convert.ToInt32(id);
    //        return ProcessResult;
    //    }

    //    public bool SaveShoppingCartItem(Product prod)
    //    {
    //        ProductBeingProcessed = prod;
    //        return ProcessResult;
    //    }
    //}

    //[Fact]
    //public void AddProduct_Success()
    //{
    //    var dbMock = new DbServiceMock();
    //    dbMock.ProcessResult = true;
    //    // Arrange
    //    Shopping_Cart shoppingCart = new(dbMock);

    //    // Act
    //    var product = new Product(1, "Shoes", 200);
    //    var result = shoppingCart.AddProduct(product);

    //    // Assert
    //    Assert.True(result);
    //    Assert.Equal(product, dbMock.ProductBeingProcessed);
    //}

    //[Fact]
    //public void AddProduct_Failure_InvalidPayload()
    //{
    //    var dbMock = new DbServiceMock();
    //    dbMock.ProcessResult = false;

    //    // Arrange
    //    Shopping_Cart shoppingCart = new(dbMock);

    //    // Act
    //    var result = shoppingCart.AddProduct(null);

    //    // Assert
    //    Assert.False(result);
    //}

    //[Fact]
    //public void RemoveProduct_Success()
    //{
    //    var dbMock = new DbServiceMock();
    //    dbMock.ProcessResult = true;
    //    // Arrange
    //    Shopping_Cart shoppingCart = new(dbMock);

    //    // Act
    //    var product = new Product(1, "Shoes", 200);
    //    var result = shoppingCart.DeleteProduct(product.Id);

    //    // Assert
    //    Assert.True(result);
    //    Assert.Equal(product.Id, dbMock.ProductIdBeingProcessed);
    //}

    //[Fact]
    //public void RemoveProduct_Failed()
    //{
    //    var dbMock = new DbServiceMock();
    //    dbMock.ProcessResult = false;
    //    // Arrange
    //    Shopping_Cart shoppingCart = new(dbMock);

    //    // Act
    //    var result = shoppingCart.DeleteProduct(null);

    //    // Assert
    //    Assert.False(result);
    //}

    //[Fact]
    //public void RemoveProduct_Failed_InvalidId()
    //{
    //    var dbMock = new DbServiceMock();
    //    dbMock.ProcessResult = false;
    //    // Arrange
    //    Shopping_Cart shoppingCart = new(dbMock);

    //    // Act
    //    var result = shoppingCart.DeleteProduct(0);

    //    // Assert
    //    Assert.False(result);
    //}

    public readonly Mock<IDbService> _dbServiceMock = new();

    [Fact]
    public void AddProduct_Success()
    {
        var product = new Product(1, "Shoes", 200);
        _dbServiceMock.Setup(x => x.SaveShoppingCartItem(product)).Returns(true);
        // Arrange
        Shopping_Cart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.AddProduct(product);

        // Assert
        Assert.True(result);
        _dbServiceMock.Verify(x => x.SaveShoppingCartItem(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public void AddProduct_Failure_InvalidPayload()
    {

        // Arrange
        Shopping_Cart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.AddProduct(null);

        // Assert
        Assert.False(result);
        _dbServiceMock.Verify(x => x.SaveShoppingCartItem(It.IsAny<Product>()), Times.Never);
    }

    [Fact]
    public void RemoveProduct_Success()
    {
        var product = new Product(1, "Shoes", 200);
        _dbServiceMock.Setup(x => x.RemoveShoppingCartItem(product.Id)).Returns(true);

        // Arrange
        Shopping_Cart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.DeleteProduct(product.Id);

        // Assert
        Assert.True(result);
        _dbServiceMock.Verify(x => x.RemoveShoppingCartItem(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void RemoveProduct_Failed()
    {
        _dbServiceMock.Setup(x => x.RemoveShoppingCartItem(null)).Returns(false);

        // Arrange
        Shopping_Cart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.DeleteProduct(null);

        // Assert
        Assert.False(result);
        _dbServiceMock.Verify(x => x.RemoveShoppingCartItem(null), Times.Never);
    }

    [Fact]
    public void RemoveProduct_Failed_InvalidId()
    {
        _dbServiceMock.Setup(x => x.RemoveShoppingCartItem(null)).Returns(false);

        // Arrange
        Shopping_Cart shoppingCart = new(_dbServiceMock.Object);

        // Act
        var result = shoppingCart.DeleteProduct(0);

        // Assert
        Assert.False(result);
        _dbServiceMock.Verify(x => x.RemoveShoppingCartItem(null), Times.Never);
    }
}