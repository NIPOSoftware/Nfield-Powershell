using System.Collections;
using System.Management.Automation;
using Moq;
using Nfield.Models;
using Nfield.PowerShell.Helpers;
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
