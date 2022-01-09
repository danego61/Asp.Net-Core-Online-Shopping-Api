using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Entities.Concrete;
using OnlineShopping.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("{categoryName?}")]
        public IActionResult GetCategory(string categoryName)
        {
            if (categoryName != null)
            {
                Category category = categoryService.GetCategory(categoryName);
                if (category == null)
                    return NotFound();
                return Ok(new CategoryModel
                {
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    IsDisabled = category.CategoryStatus == Entities.CategoryStatus.Deleted
                });
            }
            return Ok(categoryService.GetAllCategories().Select(x => new CategoryModel
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
                IsDisabled = x.CategoryStatus == Entities.CategoryStatus.Deleted
            }));
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryModel model)
        {
            Category cat = categoryService.AddCategory(new Category
            {
                CategoryName = model.CategoryName,
                CategoryStatus = Entities.CategoryStatus.Active
            });
            model.CategoryID = cat.CategoryID;
            return CreatedAtAction(nameof(GetCategory), new { categoryName = model.CategoryName }, model);
        }

        [HttpPut]
        public IActionResult UpdateCategory(CategoryModel model)
        {
            categoryService.UpdateCategory(new Category
            {
                CategoryID = model.CategoryID,
                CategoryName = model.CategoryName,
                CategoryStatus = model.IsDisabled ? Entities.CategoryStatus.Deleted : Entities.CategoryStatus.Active
            });
            return Ok(model);
        }

    }

}
