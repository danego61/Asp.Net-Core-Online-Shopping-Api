using OnlineShopping.Business.Abstract;
using OnlineShopping.Core.Aspects.Postsharp.AuthorizationAspects;
using OnlineShopping.DataAccess.Abstract;
using OnlineShopping.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {

        private readonly IProductDal productDal;

        public ProductManager(IProductDal productDal)
        {
            this.productDal = productDal;
        }

        [SecuredOperation("Admin")]
        public Product AddProduct(Product product)
        {
            productDal.Add(product);
            productDal.SaveChanges();
            return product;
        }

        public List<Product> GetAllProducts(string category = null)
        {
            return category == null ? productDal.GetList() : productDal.GetList(category);
        }

        public Product GetProduct(string productName)
        {
            return productDal.Get(x => x.Name == productName);
        }

        public Product GetProduct(int productID)
        {
            return productDal.Get(x => x.ProductID == productID);
        }

        [SecuredOperation("Admin")]
        public Product UpdateProduct(Product product)
        {
            productDal.Update(product);
            productDal.SaveChanges();
            return product;
        }
    }
}
