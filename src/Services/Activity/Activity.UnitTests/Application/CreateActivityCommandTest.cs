using DotNetCore.CAP;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Commands;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.Activity.Infrastructure.Data;
using Together.Extensions.Claims;
using Xunit;

namespace Activity.UnitTests.Application
{
    public class CreateActivityCommandTest
    {
        private readonly Mock<IActivityRepository> _activityRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ActivityDbContext> _contextMock;
        private readonly Mock<ICapPublisher> _publisherMock;
        public CreateActivityCommandTest()
        {
            _activityRepositoryMock = new Mock<IActivityRepository>();
            _mediatorMock = new Mock<IMediator>();
            _contextMock = new Mock<ActivityDbContext>();
            _publisherMock = new Mock<ICapPublisher>();
        }

        [Fact]
        public async Task Handle_create_activity_success()
        {
            // Arrange
            var fackUser = FakeUser();
            var fakeActivityCmd = FakeActivityRequestWithUser(fackUser);

            _activityRepositoryMock.Setup(x => x.UnitOfWork.SaveEntitiesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(true));

            // Act
            var handler = new CreateActivityCommandHandler(_activityRepositoryMock.Object, _mediatorMock.Object, _publisherMock.Object);
            var cltToken = new CancellationToken();
            var result = await handler.Handle(fakeActivityCmd, cltToken);

            // Assert
            Assert.True(result);
        }

        private CurrentUserInfo FakeUser() => new CurrentUserInfo
        {
            UserId = "123456",
            Avatar = "",
            Nickname = "Fake",
            Gender = 1
        };

        private CreateActivityCommand FakeActivityRequestWithUser(CurrentUserInfo user)
        {
            return new CreateActivityCommand();
        }
    }
}
