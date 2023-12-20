using Ide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Repository.Shared.Abstract
{
    public interface IUnitOfWork
    {
        IRepository<AppUser> Users { get; }
        IRepository<Order> Orders { get; }
        IRepository<Product> Products { get; }
        IRepository<ShoppingBasket> ShoppingBaskets { get; }
        IRepository<UserType> UserTypes { get; }
        IRepository<OrderProduct> OrderProducts { get; }
        IRepository<OrderProductType> OrderProductTypes { get; }
        IRepository<OrderType> OrderTypes { get; }
      IRepository<ProductType> ProductTpes { get; }

        void Save();
    }
}
