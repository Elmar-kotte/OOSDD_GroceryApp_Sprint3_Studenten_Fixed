using System.Collections.ObjectModel;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Get(int id)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            throw new NotImplementedException();
        }
        
        public List<Product> FilterAvailableProducts(string stringInName, List<Product> products)
        {
            stringInName = stringInName.ToLower(); // Ensure the string is in lowecase to avoid irritation while searching
            
            // Filter through all products and create a new list with them
            products = products.Where(x => x.Name.Contains(stringInName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            return products;
        }

        public void ReplaceObservableList(List<Product> newListItems, ObservableCollection<Product> listToRemoveFrom)
        {
            listToRemoveFrom.Clear();
            foreach (var product in newListItems)
                listToRemoveFrom.Add(product);
        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}
