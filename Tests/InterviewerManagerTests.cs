using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using Moq;
using Nfield.Infrastructure;
using Nfield.Models;
using Nfield.PowerShell.Implementation;
using Nfield.Services;
using Xunit;

namespace Nfield.PowerShell.Tests
{
    public class InterviewerManagerTests
    {
        [Fact]
        public void TestAdd_Interviewer_ReturnsInterviewer()
        {
            const string userName = "interviewer";
            const string clientInterviewerId = "ClientInterviewerId";

            var mockedInterviewer = new Interviewer()
                {
                    ClientInterviewerId = clientInterviewerId,
                    UserName = userName
                };
            
            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.AddAsync(It.Is<Interviewer>(i => i.ClientInterviewerId == clientInterviewerId)))
                         .Returns(Task.Factory.StartNew(() => new Interviewer
                             {
                                 ClientInterviewerId = clientInterviewerId,
                                 UserName = userName
                             }));

            var target = new InterviewerManager();
            var result = target.Add(mockedConnection.Object, mockedInterviewer);

            Assert.Equal(clientInterviewerId, result.ClientInterviewerId);

        }

        [Fact]
        public void TestGetByUserName_NamePresent_ReturnsInterviewer()
        {
            const string searchName = "Piet";

            List<Interviewer> list = new List<Interviewer>();
            list.Add(new Interviewer() { UserName = searchName });

            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.QueryAsync())
                .Returns(Task.Factory.StartNew(() => list.AsQueryable()));

            var target = new InterviewerManager();
            var result = target.GetByUserName(mockedConnection.Object, searchName);

            Assert.Equal(searchName, ((Interviewer)result).UserName);
        }

        [Fact]
        public void TestGetByUserName_NameNotPresent_ReturnsNull()
        {
            const string searchName = "Piet";

            List<Interviewer> list = new List<Interviewer>();
            list.Add(new Interviewer() { UserName = searchName });

            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.QueryAsync())
                .Returns(Task.Factory.StartNew(() => list.AsQueryable()));

            var target = new InterviewerManager();
            var result = target.GetByUserName(mockedConnection.Object, "Jan");

            Assert.Null(result);
        }

        [Fact]
        public void TestGetById_IdNotPresent_ReturnsNull()
        {
            List<Interviewer> list = new List<Interviewer>();

            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.QueryAsync())
                .Returns(Task.Factory.StartNew(() => list.AsQueryable()));

            var target = new InterviewerManager();
            var result = target.GetById(mockedConnection.Object, "NotFound");

            Assert.Null(result);
        }

        [Fact]
        public void TestGetByClientId_ClientIdPresent_ReturnsInterviewer()
        {
            const string searchClientId = "PietClientId";

            List<Interviewer> list = new List<Interviewer>();
            list.Add(new Interviewer() { ClientInterviewerId = searchClientId });

            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.QueryAsync())
                .Returns(Task.Factory.StartNew(() => list.AsQueryable()));

            var target = new InterviewerManager();
            var result = target.GetByClientId(mockedConnection.Object, searchClientId);

            Assert.Equal(searchClientId, ((Interviewer)result).ClientInterviewerId);
        }

        [Fact]
        public void TestGetAll_WhenExecuted_ReturnsInterviewers()
        {
            const string interviewerId1 = "Interviewer_1";
            const string interviewerId2 = "Interviewer_2";

            var list = new List<Interviewer>
                {
                    new Interviewer {ClientInterviewerId = interviewerId1},
                    new Interviewer {ClientInterviewerId = interviewerId2}
                };

            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.QueryAsync())
                .Returns(Task.Factory.StartNew(() => list.AsQueryable()));

            var target = new InterviewerManager();
            var result = target.GetAll(mockedConnection.Object);

            Assert.Equal(2, result.Count());
            Assert.NotNull(result.First(x => x.ClientInterviewerId == interviewerId1));
            Assert.NotNull(result.First(x => x.ClientInterviewerId == interviewerId2));
        }

        [Fact]
        public void TestRemove_RemoveInterviewer_CallsRemoveOnService()
        {
            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.RemoveAsync(It.IsAny<Interviewer>()))
                .Returns(Task.Factory.StartNew(() => {}));

            var target = new InterviewerManager();
            target.Remove(mockedConnection.Object, new Interviewer());

            mockedService.Verify(x => x.RemoveAsync(It.IsAny<Interviewer>()), Times.Once());
        }

        [Fact]
        public void TestUpdate_Interviewer_ReturnsInterviewer()
        {
            const string emailAddress = "someaddress@someprovider.com";

            var mockedInterviewer = new Interviewer()
            {
                EmailAddress = emailAddress
            };

            var mockedConnection = new Mock<INfieldConnection>();
            var mockedService = new Mock<INfieldInterviewersService>();

            mockedConnection.Setup(x => x.GetService<INfieldInterviewersService>())
                    .Returns(mockedService.Object);
            mockedService.Setup(x => x.UpdateAsync(It.IsAny<Interviewer>()))
                         .Returns(Task.Factory.StartNew(() => new Interviewer
                         {
                             EmailAddress = emailAddress
                         }));

            var target = new InterviewerManager();
            var result = target.Update(mockedConnection.Object, mockedInterviewer);

            Assert.Equal(emailAddress, result.EmailAddress);
        }

    }
}
