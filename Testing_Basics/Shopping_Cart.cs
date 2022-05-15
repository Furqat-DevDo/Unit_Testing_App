using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Basics
{
    public  class Shopping_Cart
    {
        // Defining a product
        public record Product(int Id, string Name, double price);

        // Db Service
        public interface IDbService
        {
            bool SaveShoppingCartItem(Product prod);
            bool RemoveShoppingCartItem(int? id);
        }

        // Shopping Cart functionality
       
        private IDbService _dbService;

        public Shopping_Cart(IDbService dbService)
        {
            _dbService = dbService;
        }

        public bool AddProduct(Product? product)
        {
            if (product == null)
                return false;

            if (product.Id == 0)
                return false;

            _dbService.SaveShoppingCartItem(product);
            return true;
        }

        public bool DeleteProduct(int? id)
        {
            if (id == null)
                return false;

            if (id == 0)
                return false;

            _dbService.RemoveShoppingCartItem(id);
            return true;
        }
        
    }
}
