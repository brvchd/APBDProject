using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvertAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using AdvertAPI.Services;

namespace AdvertAPI.Controllers.Tests
{
    [TestClass()]
    public class ClientsControllerTests
    {
        [TestMethod()]
        public void RegisterUserTest()
        {
            //Arrange
            var advertService = new AdvertService();
            var controller = new ClientsController();
        }
    }
}