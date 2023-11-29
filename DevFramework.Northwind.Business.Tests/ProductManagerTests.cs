using AutoMapper;
using DevFramework.Northwind.Business.Concrete.Managers;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace DevFramework.Northwind.Business.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void Product_Validation_Test()
        {
            Mock<IProductDal> mockProductDal = new Mock<IProductDal>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            ProductManager productManager = new ProductManager(mockProductDal.Object, mockMapper.Object);

            productManager.Insert(new Product());
        }
    }
}
