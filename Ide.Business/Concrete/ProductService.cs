using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Ide.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ide.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork=unitOfWork;
        }

        public async Task AddProductPicture(int productId, string picture)
        {
            Product product = unitOfWork.Products.GetById(productId);

            byte[] imageBytes = Convert.FromBase64String(picture.ToString().Split(',')[1]);
            string fileName = product.Name.TextClean() + "-" + product.ProductNo + ".png";
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product"));
            }

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", fileName);

            System.IO.File.WriteAllBytes(filePath, imageBytes);

            product.Picture = fileName;

            unitOfWork.Products.Update(product);
            unitOfWork.Save();
        }

        public void Delete(int productId)
        {
            unitOfWork.Products.Delete(unitOfWork.Products.GetById(productId));
            unitOfWork.Save();
        }

        public IQueryable GetAll()
        {
          return  unitOfWork.Products.GetAll();
        }

        public IQueryable GetAllCustomer()
        {
            return unitOfWork.Products.GetAll(u=>u.IsActive==true);
        }

        public int GetProductRemainingStock(string productNo)
        {
            return unitOfWork.Products.GetFirstOrDefault(u=>u.ProductNo==productNo).RemainingStock.Value;
        }

        public async Task NewProduct(Product product)
        {
            Product product1=product;
            product1.RemainingStock = product.Stock;
                //byte[] imageBytes = Convert.FromBase64String(picture.ToString().Split(',')[1]);
                //string fileName = product1.Name.TextClean() + "-" + product1.ProductNo + ".png";
                //if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product")))
                //{
                //    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product"));
                //}

                //string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Product", fileName);

                //System.IO.File.WriteAllBytes(filePath, imageBytes);
                //product1.Picture = fileName;
                unitOfWork.Products.Add(product1);
                unitOfWork.Save();
          
           
        }

        public Product ProductGetById(int productId)
        {
            return unitOfWork.Products.GetById(productId);
        }

        public void ProductUpdate(Product product)
        {
            Product originalProduct = unitOfWork.Products.GetById(product.Id);
            originalProduct.Name = product.Name;
            originalProduct.Stock = product.Stock;
            originalProduct.Price = product.Price;
            originalProduct.RemainingStock = product.RemainingStock;
            originalProduct.Barcode = product.Barcode;
            originalProduct.ProductNo = product.ProductNo;
            originalProduct.Description = product.Description;
            originalProduct.IsActive=product.IsActive;

            if(product.RemainingStock> 0)
            {
                product.ProductTypeId = unitOfWork.ProductTpes.GetAll(u => u.Name.Contains("var")).FirstOrDefault().Id;
            }
            else
            {
                product.ProductTypeId = unitOfWork.ProductTpes.GetAll(u => u.Name.Contains("yok")).FirstOrDefault().Id;

            }
            unitOfWork.Products.Update(originalProduct);
            unitOfWork.Save();
        }
        

    }
}
