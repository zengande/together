using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Controllers;
using WebMVC.Infrastructure.API;
using WebMVC.Infrastructure.Dtos;
using WebMVC.Services;
using WebMVC.ViewModels.Activity;
using Xunit;

namespace Activity.UnitTests.Application
{
    public class ActivitiesControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IActivityAPI> _activityAPIMock;
        private readonly Mock<IActivityService> _activityServiceMock;
        private readonly Mock<HttpContext> _contextMock;
        private readonly Mock<IMapper> _mapperMock;
        public ActivitiesControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _activityAPIMock = new Mock<IActivityAPI>();
            _contextMock = new Mock<HttpContext>();
            _mapperMock = new Mock<IMapper>();
            _activityServiceMock = new Mock<IActivityService>();
        }

        //[Fact]
        //public async Task Create_activity_success()
        //{
        //    // Arrange
        //    var fakeActivity = FakeActivity();
        //    _activityAPIMock.Setup(x => x.CreateActivity(fakeActivity))
        //        .Returns(Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK) { }));

        //    // Act
        //    var controller = new ActivityController(_activityAPIMock.Object, _activityServiceMock.Object, _mapperMock.Object);
        //    controller.ControllerContext.HttpContext = _contextMock.Object;
        //    var actionResult = await controller.Create(fakeActivity);

        //    // Assert
        //    var viewResult = Assert.IsType<OkResult>(actionResult);
        //}

        //[Fact]
        //public async Task Get_activity_detail_success()
        //{
        //    // Arrange
        //    _activityAPIMock.Setup(x => x.GetActivityAsync(It.IsAny<int>()))
        //        .Returns(Task.FromResult(new ActivityDetails()));

        //    // Act
        //    var controller = new ActivityController(_activityAPIMock.Object, _activityServiceMock.Object, _mapperMock.Object);
        //    var actionResult = await controller.Details(1);

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(actionResult);
        //    Assert.IsAssignableFrom<ActivityDetails>(viewResult.ViewData.Model);
        //}

        //private ActivityCreateViewModel FakeActivity() => new ActivityCreateViewModel
        //{
        //    Title = "测试数据",
        //    Details = "测试数据"
        //};
    }
}
