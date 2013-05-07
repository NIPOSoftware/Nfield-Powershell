//    This file is part of Nfield.PowerShell.
//
//    Nfield.PowerShell is free software: you can redistribute it and/or modify
//    it under the terms of the GNU Lesser General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    Nfield.PowerShell is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public License
//    along with Nfield.PowerShell.  If not, see <http://www.gnu.org/licenses/>.

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
