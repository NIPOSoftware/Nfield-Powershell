using System;
using System.Globalization;
using System.Management.Automation;
using System.Reflection;

namespace Nfield.PowerShell.Helpers
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
