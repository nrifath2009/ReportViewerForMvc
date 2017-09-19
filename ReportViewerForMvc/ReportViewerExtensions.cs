using Microsoft.Reporting.WebForms;
using System;
using System.Linq;

namespace ReportViewerForMvc
{
    /// <summary>
    /// ReportViewerExtensions helpers for ReportViewerForMvc
    /// </summary>
    public static class ReportViewerExtensions
    {
        /// <summary>
        /// Copy the properties of the specified ReportViewer to the ReportViewer.
        /// </summary>
        /// <param name="reportViewer">The ReportViewer that this method extends.</param>
        /// <param name="properties">The ReportViewer whose properties should be copied to the ReportViewer.</param>
        public static void SetProperties(this ReportViewer reportViewer, ReportViewer properties)
        {
            if (reportViewer == null)
            {
                throw new ArgumentNullException("reportViewer", "Value cannot be null.");
            }

            CopyPropertiesHelper.Copy<ReportViewer>(ref reportViewer, properties);

            reportViewer.LocalReport.SetProperties(properties.LocalReport);
            reportViewer.ServerReport.SetProperties(properties.ServerReport);
        }

        /// <summary>
        /// Copy the properties of the specified LocalReport to the LocalReport.
        /// </summary>
        /// <param name="localReport">The LocalReport that this method extends.</param>
        /// <param name="properties">The LocalReport whose properties should be copied to the LocalReport.</param>
        public static void SetProperties(this LocalReport localReport, LocalReport properties)
        {
            if (localReport == null)
            {
                throw new ArgumentNullException("localReport", "Value cannot be null.");
            }

            CopyPropertiesHelper.Copy<LocalReport>(ref localReport, properties);

            localReport.DataSources.Add(properties.DataSources.ToList());

            try
            {
                localReport.SetParameters(properties.GetParameters());
            }
            catch (MissingReportSourceException) { } //Do nothing
        }

        /// <summary>
        /// Copy the properties of the specified ServerReport to the ServerReport.
        /// </summary>
        /// <param name="serverReport">The ServerReport that this method extends.</param>
        /// <param name="properties">The ServerReport whose properties should be copied to the ServerReport.</param>
        public static void SetProperties(this ServerReport serverReport, ServerReport properties)
        {
            if (serverReport == null)
            {
                throw new ArgumentNullException("serverReport", "Value cannot be null.");
            }

            CopyPropertiesHelper.Copy<ServerReport>(ref serverReport, properties);

            try
            {
                serverReport.SetParameters(properties.GetParameters());
            }
            catch (MissingReportSourceException) { } //Do nothing
        }
    }
}
