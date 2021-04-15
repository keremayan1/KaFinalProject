using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.DynamicProxy.Generators;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using FluentValidation;


namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        private ICategoryService _categoryService;
      
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;

            _categoryService = categoryService;
        }
     //   [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
          
            var result = BusinessRules.Run(ProductCountOfExist(product.CategoryId),ProductNameExits(product.ProductName),CheckIfCategoryNumber());
            if (result!=null)
            {
                return new ErrorResult();
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult();
        }
        public IDataResult<List<Product>> GetAll()
        {
         
            if (DateTime.Now.Hour == 17)
            {
                return new ErrorDataResult<List<Product>>("Bakım Arası");
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), "Ürünler Listelendi");
        }
        public IDataResult<List<Product>> GetByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));

        }


        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var result = BusinessRules.Run(ProductCountOfExist(product.CategoryId),ProductNameExits(product.ProductName));
            if (result!=null)
            {
                return new ErrorResult();
            }
            _productDal.Update(product);
            return new SuccessResult();
        }


        //Business Codes
        private IResult ProductCountOfExist(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult("");
            }

            return new SuccessResult();
        }
        private IResult ProductNameExits(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult("Ayni isimden olamaz.");
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryNumber()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult();
            }

            return new SuccessResult();

        }


    }
}
