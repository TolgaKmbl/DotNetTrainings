using DevFramework.Core.Aspects.Postsharp;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [CacheAspect(typeof(MemCacheManager))]
        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId);
        }

        [CacheRemoveAspect(typeof(MemCacheManager))]
        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Insert(Product product)
        {
            return _productDal.Add(product);
        }

        [TransactionalAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            _productDal.Add(product2);
            //using (TransactionScope scope = new TransactionScope()) 
            //{
            //    try 
            //    {
            //        _productDal.Add(product1);
            //        _productDal.Add(product2);
            //        scope.Complete();
            //    } catch (Exception ex) 
            //    {
            //        scope.Dispose();
            //    }
            //}
        }

        [FluentValidationAspect(typeof(ProductValidator))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
    }
}
