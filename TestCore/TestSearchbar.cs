using Grocery.Core.Interfaces.Repositories;
using Moq;
using Grocery.Core.Models;
using Grocery.Core.Services;

namespace TestCore;
public class TestSearchbar
{
    
    [SetUp]
    public void Setup()
    {
    }

    private static readonly object[] FilterCases =
    {
        new object[] { "Kaas", new List<Product> { new Product(5, "Kaas", 3) } },
        new object[] { "banaan", new List<Product> { new Product(2, "banaan", 2) } },
        new object[] { "pp", new List<Product> { new Product(1, "Appel", 1), new Product(3, "Sinasappel", 3) } }
    };
    
    [TestCaseSource(nameof(FilterCases))]
    public void TestFilterAvailableProductsReturnsFilteredList(string stringInput, List<Product> expectedOutput)
    {
        // Arrange
        var mockGroceryListItemsRepository = new Mock<IGroceryListItemsRepository>();
        var mockProductRepository = new Mock<IProductRepository>();
        
        GroceryListItemsService groceryListItemsService = new GroceryListItemsService(mockGroceryListItemsRepository.Object, mockProductRepository.Object);

        List<Product> products = new List<Product>
        {
            new Product(1, "Appel", 1),
            new Product(2, "banaan", 2),
            new Product(3, "Sinasappel", 3),
            new Product(4, "Melk", 3),
            new Product(5, "Kaas", 3),
            new Product(6, "Koekjes", 3),
        };

        // Act
        List<Product> filteredList = groceryListItemsService.FilterAvailableProducts(stringInput, products);
        filteredList = filteredList.OrderBy(p => p.Id).ToList();
        
        // Assert
        Assert.That(filteredList.Select(p => p.Id), Is.EqualTo(expectedOutput.Select(p => p.Id)));
    }
}