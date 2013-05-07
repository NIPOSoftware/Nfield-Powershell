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
using System.Management.Automation;
using Nfield.Infrastructure;
using Nfield.Models;
using Nfield.PowerShell.Managers.Abstract;
using Nfield.PowerShell.State;

namespace Nfield.PowerShell.Commands
{
    /// <summary>
    /// This class implements the open-domain cmdlet.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "Interviewer",
            DefaultParameterSetName = ParameterSetNameName)]
    public class GetInterviewer : Cmdlet
    {
        public GetInterviewer()
        {
        }

        const string ParameterSetNameName = "Name";
        const string ParameterSetNameId = "Id";
        const string ParameterSetNameClientId = "ClientId";

        [Parameter(Mandatory = true)]
        public Domain Domain { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNameName)]
        public string UserName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNameId)]
        public string Id { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = ParameterSetNameClientId)]
        public string ClientId { get; set; }

        protected override void ProcessRecord()
        {
            try
            {
                Interviewer interviewer;
                var manager = DependencyResolver.Current.Resolve<IInterviewerManager>();
                if (!string.IsNullOrEmpty(UserName))
                    interviewer = manager.GetByUserName(Domain.Connection, UserName);
                else if (!string.IsNullOrEmpty(Id))
                    interviewer = manager.GetById(Domain.Connection, Id);
                else 
                    interviewer = manager.GetByClientId(Domain.Connection, ClientId);
                WriteObject(interviewer);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
