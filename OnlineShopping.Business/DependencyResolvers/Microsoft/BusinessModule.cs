using Microsoft.Extensions.DependencyInjection;
using OnlineShopping.Business.Abstract;
using OnlineShopping.Business.Concrete.Managers;
using OnlineShopping.DataAccess.Abstract;
using OnlineShopping.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.Business.DependencyResolvers.Microsoft
{
    public static class BusinessModule
    {

        public static IServiceCollection AddBusinessModule(this IServiceCollection services)
        {

            services.AddScoped<OnlineShoppingDbContext, OnlineShoppingDbContext>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICustomerDal, EfCustomerDal>();
            services.AddScoped<IEmployeeDal, EfEmployeeDal>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<ICategoryService, CategoryManager>();

            return services;
        }

    }
}
