using Ide.Business.Abstract;
using Ide.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Configurations
{
    public static class BusinessExtensions
    {
        public static void AddBusinessDI(this IServiceCollection services )
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserTypeService,UserTypeService>();
            services.AddScoped<IShopingBasketService, ShoppingBasketService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
