using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Abstract
{
    public interface IShopingBasketService
    {
        IQueryable GetAll(string mail);
        public void ProductDown(string mail, int id);
        bool ProductAdd(string mail, int id);
        bool BasketNull(string mail);
    }
}
