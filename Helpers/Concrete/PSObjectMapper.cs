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
using System.Globalization;
using System.Management.Automation;
using System.Reflection;
using Nfield.PowerShell.Helpers.Abstract;

namespace Nfield.PowerShell.Helpers.Concrete
{
    internal class PSObjectMapper : IPSObjectMapper
    {
        #region IPSObjectMapper Members

        public T To<T>(PSObject source) where T: class, new()
        {
            T result = source.BaseObject as T;
            if (result != null)
                return result;
            result = new T();
            bool dataEntered = false;
            foreach (var item in source.Properties)
            {
                var propertyInfo = typeof(T).GetProperty(item.Name,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.CanWrite && item.Value != null)
                {
                    if (propertyInfo.PropertyType == typeof(string) && item.Value != null &&
                            item.Value.GetType() != typeof(string))
                    {
                        propertyInfo.SetValue(result, item.Value.ToString(), null);
                        dataEntered = true;
                    }
                    else if (propertyInfo.PropertyType == typeof(int) &&
                            item.Value.GetType() != typeof(int))
                    {
                        int value;
                        if (int.TryParse(item.Value.ToString(), NumberStyles.Integer,
                                CultureInfo.InvariantCulture, out value))
                        {
                            propertyInfo.SetValue(result, value, null);
                            dataEntered = true;
                        }
                    }
                    else
                    {
                        // just try to put it in
                        try
                        {
                            propertyInfo.SetValue(result, item.Value, null);
                            dataEntered = true;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
            if (!dataEntered)
                result = null;
            return result;
        }

        public PSObject From<T>(T source) where T : class
        {
            PSObject result = new PSObject();
            bool dataEntered = false;
            foreach (var propertyInfo in source.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance))
                if (propertyInfo.CanRead)
                {
                    object value = propertyInfo.GetValue(source, null);
                    if (value != null)
                    {
                        result.Properties.Add(new PSNoteProperty(propertyInfo.Name, value));
                        dataEntered = true;
                    }
                }
            if (!dataEntered)
                result = null;
            return result;
        }

        #endregion
    }
}
