using System.Threading.Tasks;
using Moq;
using Nfield.Infrastructure;
using Nfield.PowerShell.Managers.Concrete;
using Xunit;

namespace Nfield.PowerShell.Tests
{
    public class DomainManagerTests
    {
        [Fact]
        public void TestConnect_Always_ReturnsDomainObjectWithCorrectName()
        {
            var target = new DomainManager();

            const string domainName = "MyDomain";
            const string userName = "MyUser";
            const string password = "MyPassword";

            var mockedConnection = new Mock<INfieldConnection>() { DefaultValue = DefaultValue.Mock };
            mockedConnection.Setup(x => x.SignInAsync(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.Factory.StartNew(() => true));

            var result = target.Connect(mockedConnection.Object, domainName,
                    userName, password);

            Assert.Equal(domainName, result.Name);
        }
    }
}
