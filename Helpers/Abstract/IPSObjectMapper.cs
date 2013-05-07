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
