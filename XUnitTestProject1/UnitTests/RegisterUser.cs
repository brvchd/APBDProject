using AdvertAPI.Controllers;
using AdvertAPI.DTOs.Requests;
using AdvertAPI.DTOs.Responses;
using AdvertAPI.Models;
using AdvertAPI.Services;
using AdvertAPI.Services.Implementation;
using AdvertAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class RegisterUser
    {

        [Fact]
        public void RegisterUser_Success()
        {
            //Arrange
            var dbLayer = new Mock<IAdvertService>();
            var request = new RegisterUserRequest()
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "mynewmail@gmail.com",
                Phone = "111-111-111",
                Login = "newlogin",
                Password = "qwerty123"
            };
            dbLayer.Setup(d => d.RegisterUser(request)).Returns(new RegisterUserResponse() { AccessToken = "alamakota", RefreshToken = "rerfsefefefS" });
            var controller = new ClientsController(dbLayer.Object);
            //Act
            var response = controller.RegisterUser(request);
            //Assert
            var result = response as CreatedAtActionResult;
            Assert.NotNull(response);
            Assert.IsType<CreatedAtActionResult>(result);

        }
    }
}
