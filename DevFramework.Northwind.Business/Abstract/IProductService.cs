using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int productId);
        Product Insert(Product product);
        Product Update(Product product);
    }
}
