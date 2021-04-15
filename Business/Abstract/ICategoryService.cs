using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
  public   interface ICategoryService
  {
      List<Category> GetAll();
      List<Category> GetByCategoryId(int id);
      void Add(Category category);
      void Delete(Category category);
      void Update(Category category);
  }
}
