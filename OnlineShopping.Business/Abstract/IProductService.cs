using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Abstract
{
    public interface IProductService
    {

        Product GetProduct(string productName);

        Product GetProduct(int productID);

        Product AddProduct(Product product);

        Product UpdateProduct(Product product);

        List<Product> GetAllProducts(string category = null);

    }
}
