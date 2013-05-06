using System;
using System.Management.Automation;

namespace Nfield.PowerShell
{
    /// <summary>
    /// Process helpers
    /// </summary>
    public interface IProcessHelper
    {
        /// <summary>
        /// Process arguments that can be of different types
        /// The specified nfield type, PSObject or an ArrayList of these object are allowed
        /// </summary>
        /// <typeparam name="T">the expected nfield type</typeparam>
        /// <param name="input">the input object</param>
        /// <param name="nativeAction">action for nfield object</param>
        void ProcessNfieldOrPs<T>(PSObject input, Action<T> nfieldAction) where T : class, new();
        
        /// <summary>
        /// Process arguments
        /// </summary>
        /// <typeparam name="T">the expected nfield type</typeparam>
        /// <param name="input">the input object</param>
        /// <param name="nativeAction">action for object</param>
        void ProcessItem<T>(object input, Action<T> nfieldAction) where T : class, new();
    }
}
