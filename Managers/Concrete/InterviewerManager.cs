using System;
using System.Linq;
using Nfield.Infrastructure;
using Nfield.Models;
using Nfield.PowerShell.Managers.Abstract;
using Nfield.Services;

namespace Nfield.PowerShell.Managers.Concrete
{
    public class InterviewerManager : IInterviewerManager
    {
        public InterviewerManager()
        {
        }

        public Interviewer Add(INfieldConnection connection, Interviewer interviewer)
        {
            INfieldInterviewersService interviewersService = connection.GetService<INfieldInterviewersService>();
            var interviewerResult = interviewersService.AddAsync(interviewer);
            interviewerResult.Wait();
            return interviewerResult.Result;
        }

        public IQueryable<Interviewer> GetAll(INfieldConnection connection)
        {
            var interviewersService = connection.GetService<INfieldInterviewersService>();
            return interviewersService.QueryAsync().Result;
        }

        Interviewer Get(INfieldConnection connection, Func<Interviewer, bool> filter)
        {
            INfieldInterviewersService interviewersService = connection.GetService<INfieldInterviewersService>();
            return interviewersService.QueryAsync().Result.First(i => filter(i));
        }
        public Interviewer GetByUserName(INfieldConnection connection, string userName)
        {
            return Get(connection, (interviewer) =>
                    interviewer.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));
        }

        public Interviewer GetById(INfieldConnection connection, string id)
        {
            return Get(connection, (interviewer) =>
                    interviewer.InterviewerId.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public Interviewer GetByClientId(INfieldConnection connection, string clientId)
        {
            return Get(connection, (interviewer) =>
                    interviewer.ClientInterviewerId == clientId);
        }

        public void Remove(INfieldConnection connection, Interviewer interviewer)
        {
            INfieldInterviewersService interviewersService = connection.GetService<INfieldInterviewersService>();
            interviewersService.RemoveAsync(interviewer).Wait();
        }

        public Interviewer Update(INfieldConnection connection, Interviewer interviewer)
        {
            INfieldInterviewersService interviewersService = connection.GetService<INfieldInterviewersService>();
            var interviewerResult = interviewersService.UpdateAsync(interviewer);
            interviewerResult.Wait();
            return interviewerResult.Result;
        }
    }
}
