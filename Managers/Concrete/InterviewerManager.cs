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
