using DevFramework.Northwind.DataAccess.Concrete.EntityFramework;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate;
using DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Tests.NHibernateTests
{
    [TestClass]
    public class NHibernateTest
    {
        [TestMethod]
        public void Get_All_Returns_All_Products()
        {
            NHProductDal nhProductDal = new NHProductDal(new SqlServerHelper());

            var result = nhProductDal.GetAll();

            Assert.AreEqual(77, result.Count);
        }

        [TestMethod]
        public void Get_All_Returns_All_Products_With_Filter()
        {
            NHProductDal nhProductDal = new NHProductDal(new SqlServerHelper());

            var result = nhProductDal.GetAll(p => p.ProductName.Contains("ab"));

            Assert.AreEqual(4, result.Count);
        }
    }
}
