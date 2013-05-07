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
using System.Collections;
using System.Management.Automation;
using Nfield.PowerShell.Helpers.Abstract;

namespace Nfield.PowerShell.Helpers.Concrete
{
    internal class ProcessHelper : IProcessHelper
    {
        IPSObjectMapper _mapper;

        public ProcessHelper(IPSObjectMapper mapper)
        {
            _mapper = mapper;
        }

        void ProcessPsItem<T>(PSObject input, Action<T> nfieldAction) where T : class, new()
        {
            if (input.BaseObject is ArrayList)
            {
                foreach (var item in input.BaseObject as ArrayList)
                {
                    ProcessItem(item, nfieldAction);
                }
                return;
            }
            if (input.BaseObject is object[])
            {
                foreach (var item in input.BaseObject as object[])
                {
                    ProcessItem(item, nfieldAction);
                }
                return;
            }
            T nfieldObject = _mapper.To<T>(input);
            if (nfieldObject != null)
                nfieldAction(nfieldObject);
        }

        #region IProcessHelper Members

        public void ProcessItem<T>(object input, Action<T> nfieldAction) where T : class, new()
        {
            if (input is PSObject)
            {
                ProcessPsItem(input as PSObject, nfieldAction);
                return;
            }
            if (input is T)
            {
                nfieldAction(input as T);
                return;
            }
            if (input is object[])
                foreach (var item in input as object[])
                    ProcessItem(item, nfieldAction);
        }

        public void ProcessNfieldOrPs<T>(PSObject input, Action<T> nfieldAction) where T : class, new()
        {
            try
            {
                ProcessItem(input, nfieldAction);
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion
    }
}
