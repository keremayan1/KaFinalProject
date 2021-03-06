using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>>  GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll()) ;
        }

        public IDataResult<List<Category>> GetByCategoryId(int id)
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(p => p.CategoryId == id)) ;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult();
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult();
        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult();
        }
    }
}
