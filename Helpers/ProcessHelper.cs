using System;
using System.Collections;
using System.Management.Automation;

namespace Nfield.PowerShell.Helpers
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
