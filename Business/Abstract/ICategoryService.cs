using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Result;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<List<Category>> GetByCategoryId(int id);
        IResult Add(Category category);
        IResult Delete(Category category);
        IResult Update(Category category);
    }
}
