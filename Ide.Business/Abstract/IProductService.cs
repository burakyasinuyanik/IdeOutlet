﻿using Ide.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Abstract
{
    public interface IProductService
    {
        IQueryable GetAll();
        public void ProductUpdate(Product product);
        public Product ProductGetById(int productId);
        IQueryable GetAllCustomer();
        Task NewProduct(Product product);
        Task AddProductPicture(int productId,string picture);

        public void Delete(int productId);
        public int GetProductRemainingStock(string productNo);
    }
}
