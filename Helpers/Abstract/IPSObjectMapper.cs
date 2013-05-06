using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace Nfield.PowerShell.Helpers.Abstract
{
    /// <summary>
    /// Map PSObjects from and to our types
    /// </summary>
    public interface IPSObjectMapper
    {
        /// <summary>
        /// Map PSObject to our type
        /// </summary>
        /// <typeparam name="T">our type</typeparam>
        /// <param name="source">input object</param>
        /// <returns>source as our type</returns>
        T To<T>(PSObject source) where T : class, new();

        /// <summary>
        /// Map our object to PSObject
        /// </summary>
        /// <typeparam name="T">our type</typeparam>
        /// <param name="source">our object</param>
        /// <returns>mapped object</returns>
        PSObject From<T>(T source) where T : class;
    }
}
