using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Nutshell.Common.Cache;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice;
using Together.Notice.Models;
using Together.Notice.Services;
using Xunit;

namespace NoticeService.UnitTests
{
    public class SendEmailTests
    {
        private readonly Mock<IOptions<EmailSettings>> _settingMock;
        private readonly Mock<ILogger<EmailSender>> _loggerMock;
        private readonly Mock<IRedisCacheService> _redisMock;
        public SendEmailTests()
        {
            _settingMock = new Mock<IOptions<EmailSettings>>();
            _loggerMock = new Mock<ILogger<EmailSender>>();
            _redisMock = new Mock<IRedisCacheService>();
        }

        [Fact]
        public async Task Send_email_success_and_record()
        {
            // Arrange

            var testRecord = new NoticeRecord
            {
                Id = 1,
                To = "835290734@qq.com",
            };
            _settingMock.Setup(x => x.Value).Returns(new EmailSettings
            {
                From = "server@zengande.cn",
                UserName = "server@zengande.cn",
                Password = "P@ssw0rd1234",
                StmpHost = "smtp.exmail.qq.com",
                StmpPort = 25
            });
            //_recordServiceMock.Setup(x=>x.RecordAsync())

            // Act
            var sender = new EmailSender(_settingMock.Object, _loggerMock.Object, _redisMock.Object);
            var result = await sender.Send("835290734@qq.com", "测试邮件", "这只是一封测试邮件");
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void test()
        {
            var list = new[] {
                new { date="2018-11-05",count=12,state=1},
                new { date="2018-11-05",count=1,state=2},
                new { date="2018-11-06",count=13,state=1},
                new { date="2018-11-07",count=5,state=2},
                new { date="2018-11-07",count=56,state=3}
            };

            var data = new[]
            {
                new { date = "2018-11-05", count = 0, state = 1 },
                new { date = "2018-11-05", count = 0, state = 2 },
                new { date = "2018-11-05", count = 0, state = 3 },
                new { date = "2018-11-06", count = 0, state = 1 },
                new { date = "2018-11-06", count = 0, state = 2 },
                new { date = "2018-11-06", count = 0, state = 3 },
                new { date = "2018-11-07", count = 0, state = 1 },
                new { date = "2018-11-07", count = 0, state = 2 },
                new { date = "2018-11-07", count = 0, state = 3 }
            };

            var result = from a in data
                         join b in list
                         on new { a.date, a.state } equals new { b.date, b.state }
                         into temp
                         from tmp in temp.DefaultIfEmpty()
                         select new
                         {
                             a.date,
                             a.state,
                             count = (tmp?.count).NullToZero()
                         };

            Assert.NotNull(result);
        }

        
    }

    public static class IntExtensions
    {
        public static int NullToZero(this int? value)
        {
            return value.HasValue ? value.Value : 0;
        }
    }
}
