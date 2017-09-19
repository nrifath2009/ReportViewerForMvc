using System;
using System.Reflection;

namespace ReportViewerForMvc
{
    internal static class CopyPropertiesHelper
    {
        internal static void Copy<T>(ref T obj, T properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties", "Value cannot be null.");
            }

            Copy<T, T>(ref obj, properties);
        }

        internal static void Copy<T1, T2>(ref T1 obj, T2 properties)
        {
            Type objType = obj.GetType();
            Type propertiesType = properties.GetType();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            foreach (PropertyInfo propertyInfo in propertiesType.GetProperties(bindingFlags))
            {
                try
                {
                    if (propertyInfo.CanRead)
                    {
                        var valueToCopy = propertyInfo.GetValue(properties);
                        var objProperty = objType.GetProperty(propertyInfo.Name);

                        if (objProperty.CanWrite)
                        {
                            objProperty.SetValue(obj, valueToCopy);
                        }
                    }
                }
                catch (NullReferenceException ex)
                {
                    throw ex;
                }
                catch (TargetInvocationException) { } //Do nothing, just like my boss.
            }
        }
    }
}
