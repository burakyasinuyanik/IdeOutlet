using Ide.Business.Abstract;
using Ide.Models;
using Ide.Repository.Shared.Abstract;
using Ide.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public async Task ExcelAddProduct(IFormCollection form)
        {
            string filePath = "";
            var file = form.Files["file"];
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Tmp");
            if (file != null && file.Length > 0)
            {

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fileName = Helper.RandomStringGenerator(3) + "-" + file.FileName.TextClean();
                // Save the file as needed, for example:
                filePath = Path.Combine(path, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                List<Product> newProducts = new List<Product>();
                string excelDosyaYolu = path;
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelWorkbook workBook = package.Workbook;
                    if (workBook != null)
                    {

                        if (workBook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

                            for (int i = 2; i <= currentWorksheet.Dimension.End.Row; i++)
                            {

                                Product product = new Product();
                                product.ProductNo= currentWorksheet.Cells[i, 2].Text.ToString();
                                product.Barcode= currentWorksheet.Cells[i, 3].Text.ToString();
                                product.Name= currentWorksheet.Cells[i, 6].Text.ToString();
                                product.Description= currentWorksheet.Cells[i, 7].Text.ToString() == "" ? currentWorksheet.Cells[i, 1].Text.ToString(): currentWorksheet.Cells[i, 7].Text.ToString();
                                product.Stock = currentWorksheet.Cells[i, 10].Text.ToInt();
                                product.RemainingStock = product.Stock;                           
                                product.Price = double.Parse(currentWorksheet.Cells[i, 11].Value.ToString());
                                product.Picture = currentWorksheet.Cells[i, 2].Text.ToString() + ".png";
                                product.IsActive = true;
                                newProducts.Add(product);

                            }


                        }
                        unitOfWork.Products.AddRange(newProducts);
                        unitOfWork.Save();
                    }
                }
            }
        }

        public IQueryable GetAll()
        {
          return  unitOfWork.Products.GetAll();
        }
      
        public IQueryable GetAllCustomer(int page,string search)
        {
            int skip = 0;
            int take = 20;

            if (page > 1)
            {
                skip = take * (page-1);
            }
           
            return unitOfWork.Products.GetAll(u=>u.IsActive==true && search != null ? u.Name.ToLower().Contains(search) : true).OrderByDescending(o=>o.RemainingStock).Skip(skip).Take(take);
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
