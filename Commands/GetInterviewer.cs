using System;
using System.Management.Automation;
using Nfield.Infrastructure;
using Nfield.Models;

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
