using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;

namespace DevFramework.Northwind.DataAccess.Tests.EntityFrameworkTests
{
    [TestClass]
    public class EntityFrameworkTest
    {
        [TestMethod]
        public void Get_All_Returns_All_Products()
        {
            EFProductDal eFProductDal = new EFProductDal();

            var result = eFProductDal.GetAll();

            Assert.AreEqual(77, result.Count);
        }

        [TestMethod]
        public void Get_All_Returns_All_Products_With_Filter()
        {
            EFProductDal eFProductDal = new EFProductDal();

            var result = eFProductDal.GetAll(p=>p.ProductName.Contains("ab"));

            Assert.AreEqual(4, result.Count);
        }
    }
}
