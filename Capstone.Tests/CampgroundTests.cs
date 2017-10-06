using System.Collections.Generic;
using System.Transactions;
using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace Capstone.Tests
{
    [TestClass]
    
    public class CampgroundTests
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Park Capstone;User ID=te_student;Password=sqlserver1";
        TransactionScope t;

        [TestInitialize]
        public void Initalize()
        {
            t = new TransactionScope();
        }

        [TestCleanup]
        public void CleanUp()
        {
            t.Dispose();
        }

        [TestMethod]
        public void GetListOfCampgounds()
        {
            CampgroundDAL dal = new CampgroundDAL(connectionString);
            List<Campground> c = dal.GetCampGroundsList(1);
            bool output = c.Count > 0;
            Assert.IsTrue(output);
        }
    }
}
