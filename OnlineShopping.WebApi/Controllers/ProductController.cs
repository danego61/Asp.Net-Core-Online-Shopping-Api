using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Entities.Concrete;
using OnlineShopping.WebApi.Models;

namespace OnlineShopping.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult GetProducts(string category)
        {
            List<Product> products = productService.GetAllProducts(category);
            return Ok(products.Select(product => new ProductModel
            {
                CategoryName = product.Category.CategoryName,
                Name = product.Name,
                DiscountedPrice = product.DiscountedPrice,
                Explanation = product.Explanation,
                Image = product.Image,
                Price = product.Price,
                ProductID = product.ProductID,
                IsDisabled = product.ProductStatus == Entities.ProductStatus.Deleted,
                CategoryID = product.CategoryID
            }));
        }

        [HttpGet("{productName}")]
        public IActionResult GetProduct(string productName)
        {
            Product product = productService.GetProduct(productName);
            if (product == null)
                return NotFound();
            return Ok(new ProductModel
            {
                CategoryName = product.Category.CategoryName,
                Name = product.Name,
                DiscountedPrice = product.DiscountedPrice,
                Explanation = product.Explanation,
                Image = product.Image,
                Price = product.Price,
                ProductID = product.ProductID,
                IsDisabled = product.ProductStatus == Entities.ProductStatus.Deleted,
                CategoryID = product.CategoryID
            });
        }

        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            Product pr = productService.AddProduct(new Product
            {
                CategoryID = model.CategoryID,
                Name = model.Name,
                DiscountedPrice = model.DiscountedPrice,
                Explanation = model.Explanation,
                Image = model.Image,
                Price = model.Price,
                ProductStatus = Entities.ProductStatus.Active
            });
            model.ProductID = pr.ProductID;
            return CreatedAtAction(nameof(GetProduct), new { productName = model.Name }, model);
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductModel model)
        {
            productService.UpdateProduct(new Product
            {
                CategoryID = model.CategoryID,
                Name = model.Name,
                DiscountedPrice = model.DiscountedPrice,
                Explanation = model.Explanation,
                Image = model.Image,
                Price = model.Price,
                ProductStatus = model.IsDisabled ? Entities.ProductStatus.Deleted : Entities.ProductStatus.Active,
                ProductID = model.ProductID
            });
            return CreatedAtAction(nameof(GetProduct), new { productName = model.Name }, model);
        }

    }

}
