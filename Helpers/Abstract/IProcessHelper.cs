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

namespace Nfield.PowerShell.Helpers.Abstract
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
