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

using System.Collections;
using System.Management.Automation;
using Moq;
using Nfield.Models;
using Nfield.PowerShell.Helpers.Abstract;
using Nfield.PowerShell.Helpers.Concrete;
using Xunit;

namespace Nfield.PowerShell.Tests
{
    public class ProcessHelperTests
    {
        [Fact]
        public void TestProcessNfieldOrPs_ObjectCanBeMapped_CallsNfieldAction()
        {
            int count = 0;

            var mockedMapper = new Mock<IPSObjectMapper>();
            var input = new PSObject();
            mockedMapper.Setup(x => x.To<Interviewer>(input))
                .Returns(new Interviewer());
            ProcessHelper target = new ProcessHelper(mockedMapper.Object);

            target.ProcessNfieldOrPs<Interviewer>(input,
                    (item) => { ++count; });

            Assert.Equal(1, count);
        }

        [Fact]
        public void TestProcessNfieldOrPs_ObjectCannotBeMapped_NotCallsNfieldAction()
        {
            int count = 0;

            var mockedMapper = new Mock<IPSObjectMapper>();
            var input = new PSObject();
            ProcessHelper target = new ProcessHelper(mockedMapper.Object);

            target.ProcessNfieldOrPs<Interviewer>(input,
                    (item) => { ++count; });

            Assert.Equal(0, count);
        }

        [Fact]
        public void TestProcessNfieldOrPs_ArrayObject_CallsCorrectActions()
        {

            ArrayList input = new ArrayList();
            PSObject arrayInput = new PSObject(input);
            PSObject psInput = new PSObject();
            input.Add(psInput);
            input.Add(psInput);

            int count = 0;

            var mockedMapper = new Mock<IPSObjectMapper>();
            mockedMapper.Setup(x => x.To<Interviewer>(psInput))
                .Returns(new Interviewer());
            ProcessHelper target = new ProcessHelper(mockedMapper.Object);

            target.ProcessNfieldOrPs<Interviewer>(arrayInput,
                    (item) => { ++count; });

            Assert.Equal(2, count);
        }

        [Fact]
        public void TestProcessNfieldOrPs_ArrayInArrayObject_CallsCorrectActions()
        {

            ArrayList input = new ArrayList();
            PSObject arrayInput = new PSObject(input);
            PSObject psInput = new PSObject();
            object[] secondArray = new object[2];
            secondArray[0] = psInput;
            secondArray[1] = psInput;
            input.Add(secondArray);

            int count = 0;

            var mockedMapper = new Mock<IPSObjectMapper>();
            mockedMapper.Setup(x => x.To<Interviewer>(psInput))
                .Returns(new Interviewer());
            ProcessHelper target = new ProcessHelper(mockedMapper.Object);

            target.ProcessNfieldOrPs<Interviewer>(arrayInput,
                    (item) => { ++count; });

            Assert.Equal(2, count);
        }
    }
}
