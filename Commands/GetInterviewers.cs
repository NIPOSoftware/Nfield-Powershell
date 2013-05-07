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

using Nfield.Infrastructure;
using System.Management.Automation;
using Nfield.PowerShell.Managers.Abstract;
using Nfield.PowerShell.State;

// Windows PowerShell namespace.

namespace Nfield.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "Interviewers")]
    public class GetInterviewers : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public Domain Domain { get; set; }

        protected override void ProcessRecord()
        {
            var manager = DependencyResolver.Current.Resolve<IInterviewerManager>();

            var result = manager.GetAll(Domain.Connection);

            WriteObject(result, true);
        }
    }
}
