using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ReportViewerForMvc
{
    internal static class ReportViewerHelper
    {
        internal static ReportViewer AnonymousToReportViewer(object obj)
        {
            try
            {
                return obj.ToType<ReportViewer>();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Could not convert anonymous object to type ReportViewer", ex);
            }
        }

        internal static LocalReport AnonymousToLocalReport(object obj)
        {
            try
            {
                return obj.ToType<LocalReport>();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Could not convert anonymous object to type LocalReport", ex);
            }
        }

        internal static ServerReport AnonymousToServerReport(object obj)
        {
            try
            {
                return obj.ToType<ServerReport>();
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Could not convert anonymous object to type ServerReport", ex);
            }

        }

        internal static List<ReportParameter> AnonymousToReportParameter(object obj)
        {
            List<ReportParameter> reportParameters = new List<ReportParameter>();

            foreach (KeyValuePair<string, string> keyValuePair in obj.ToDictionary())
            {
                reportParameters.Add(new ReportParameter(keyValuePair.Key, keyValuePair.Value));
            }

            return reportParameters;
        }

        internal static List<ReportDataSource> AnonymousToReportDataSourceList(object obj)
        {
            List<ReportDataSource> reportDataSourceList = new List<ReportDataSource>();

            try
            {
                if (obj.GetType().IsArray)
                {
                    foreach (var reportDataSource in (IEnumerable)obj)
                    {
                        reportDataSourceList.Add(reportDataSource.ToType<ReportDataSource>());
                    }
                }
                else
                {
                    reportDataSourceList.Add(obj.ToType<ReportDataSource>());
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Could not convert anonymous object to type ReportDataSource", ex);
            }

            return reportDataSourceList;
        }

        private static T ToType<T>(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", "Value cannot be null.");
            }

            T instance = Activator.CreateInstance<T>();

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                var property = typeof(T).GetProperty(propertyInfo.Name);
                if (property == null)
                {
                    throw new ArgumentException("An attempt was made to set the property '" + propertyInfo.Name + "' that is not found on object type '" + typeof(T).Name + "'");
                }

                property.SetValue(instance, propertyInfo.GetValue(obj));
            }

            return instance;
        }

        private static IDictionary<string, string> ToDictionary(this object obj)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();

            foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
            {
                dic.Add(propertyInfo.Name, propertyInfo.GetValue(obj).ToString());
            }

            return dic;
        }
    }
}
