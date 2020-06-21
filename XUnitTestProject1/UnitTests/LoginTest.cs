using AdvertAPI.Controllers;
using AdvertAPI.DTOs.Requests;
using AdvertAPI.DTOs.Responses;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1.UnitTests
{
    public class LoginTest
    {  
        [Fact]
        public void LoginSuccess()
        {
            //Arrange
            var dbLayer = new Mock<IAdvertService>();
            var request = new LoginRequest()
            {
                Login = "newlogin",
                Password = "qwerty123"
            };
            dbLayer.Setup(d => d.Login(request)).Returns(new LoginResponse() { AccessToken = "alamakota", RefreshToken = "rerfsefefefS" });
            var controller = new ClientsController(dbLayer.Object);
            //Act
            var responce = controller.Login(request);
            //Assert
            var result = responce as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
