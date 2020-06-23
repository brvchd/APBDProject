using AdvertAPI.Controllers;
using AdvertAPI.DTOs.Responses;
using AdvertAPI.Models;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1.UnitTests
{
    public class GetCampaignsTest
    {
        [Fact]
        public void GetCampaigns_Success()
        {
            //Arrange
            var dbLayer = new Mock<IAdvertService>();
            dbLayer.Setup(d => d.GetCampains()).Returns(new List<GetCampaignsResponse>());
            var controller = new ClientsController(dbLayer.Object);
            //Act
            var responce = controller.GetCampaigns();
            //Assert
            var result = responce as OkObjectResult;
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
