using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Capstone.DAL;
using Capstone.Models;


namespace Capstone.Tests
{
    [TestClass]
    public class ParkDalTests
    {
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Park Capstone;User ID=te_student;Password=sqlserver1";
        TransactionScope t;

        [TestInitialize]
        public void Initialize()
        {
            t = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            t.Dispose();
        }

        [TestMethod]
        public void GetListOfParksTest()
        {
            ParkDAL dal = new ParkDAL(connectionString);
            List<Parks> p = dal.GetListOfParks();
            bool output = p.Count > 0;
            Assert.IsTrue(output);
        }
    }
}
