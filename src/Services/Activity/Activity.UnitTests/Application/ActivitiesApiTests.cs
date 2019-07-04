using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Commands;
using Together.Activity.API.Applications.Queries;
using Together.Activity.API.Controllers;
using Together.Activity.API.Models;
using Xunit;

namespace Activity.UnitTests.Application
{
    public class ActivitiesApiTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<ActivitiesController>> _loggerMock;
        private readonly Mock<IActivityQueries> _activityQueriesMock;
        private readonly Mock<HttpContext> _contextMock;
        private readonly Mock<IMapper> _mapper;
        public ActivitiesApiTests()
        {
            _contextMock = new Mock<HttpContext>();
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<ActivitiesController>>();
            _activityQueriesMock = new Mock<IActivityQueries>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task Create_activity_success()
        {
            // Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateActivityCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(true));
            _contextMock.Setup(x => x.User)
                .Returns(new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                    new Claim("sub","123456"),
                    new Claim("nickname","Faker")
                })));

            // Act
            var controller = new ActivitiesController(_mediatorMock.Object, _activityQueriesMock.Object, _loggerMock.Object, _mapper.Object);
            controller.ControllerContext.HttpContext = _contextMock.Object;
            var actionResult = await controller.Create(FakeActivity(), Guid.NewGuid().ToString()) as OkResult;

            // Assert
            Assert.Equal(actionResult.StatusCode, (int)HttpStatusCode.OK);
        }

        private CreateActivityCommand FakeActivity() => new CreateActivityCommand();
    }
}
