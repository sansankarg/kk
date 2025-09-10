using InventoryManager.Constants;
using InventoryManager.Models;
using Xunit;

namespace InventoryManager.Tests.Models.Test;

public class ProductListTest
{
    [Fact]
    public void Add_ShouldAdd_WhenValidInput()
    {
        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        productList.Add(product);
        Assert.Equal(1, productList.Count());
        Assert.Equal(product, productList.Get()[0]);
    }

    [Fact]
    public void Get_ShouldGet_AddedProducts()
    {
        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        List<Product> productsExpected = productList.Get();
        productsExpected.Add(product);
        productList.Add(product);
        List<Product> products = productList.Get();
        Assert.Equal(products, productsExpected);
    }

    [Fact]
    public void Edit_ShouldEdit_SpecifiedProduct()
    {
        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        List<Product> productsExpected = productList.Get();
        productsExpected.Add(product);

        productList.Edit(0, ProductFieldNames.Name, "Arthur");

        Assert.Equal(productList.Get()[0][ProductFieldNames.Name], "Arthur");
    }

    [Fact]
    public void Delete_ShouldDelete_SpecifiedProduct()
    {
        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        productList.Add(product);
        productList.Delete(0);
        Assert.Equal(0, productList.Count());
    }

    [Fact]
    public void Search_ShouldReturnMatchingList_SpecifiedKeyword()
    {
        ProductList productList = new ProductList();
        Product product1 = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        Product product2 = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Arthur" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        productList.Add(product1);
        productList.Add(product2);

        List<Product> result = productList.Search("Arthur");
        List<Product> expected = new List<Product>();
        expected.Add(product2);
        Assert.Equal(result, expected);
    }

    [Theory]
    [InlineData(ProductFieldNames.Id, "1234567890")]
    [InlineData(ProductFieldNames.Name, "Sankar")]
    [InlineData(ProductFieldNames.Price, 100)]
    [InlineData(ProductFieldNames.Quantity, 200.12)]
    public void HasDuplicate_ShouldReturnTrue_WhenDuplicateExists(string field, object value)
    {
        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { field, value },
            });
        productList.Add(product);

        bool result = productList.HasDuplicate(field, value);
        Assert.True(result);
    }

    [Theory]
    [InlineData(ProductFieldNames.Id, "1234567890", "1111111111")]
    [InlineData(ProductFieldNames.Name, "Sankar", "Arthur")]
    [InlineData(ProductFieldNames.Price, 100, 200)]
    [InlineData(ProductFieldNames.Quantity, 200.12, 100.12)]
    public void HasDuplicate_ShouldReturnFalse_WhenDuplicateNotExists(string field, object value, object checkValue)
    {
        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { field, value },
            });
        productList.Add(product);

        bool result = productList.HasDuplicate(field, checkValue);
        Assert.False(result);
    }

    [Fact]
    public void Count_ShouldReturnCountOfCurrentCountOfProductList()
    {
        ProductList productList = new ProductList();
        Product product = new Product(
            new Dictionary<string, object>
            {
                { ProductFieldNames.Id, "1234567890" },
                { ProductFieldNames.Name, "Sankar" },
                { ProductFieldNames.Price, 2000.21M },
                { ProductFieldNames.Quantity, 21 },
            });
        productList.Add(product);
        Assert.Equal(1, productList.Count());
    }
}
