using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web;

namespace ReportViewerForMvc
{
    /// <summary>
    /// Encapsulates the methods and properties used for the ReportViewerForMvc extension. 
    /// </summary>
    public static class ReportViewerForMvc
    {
        private static ConcurrentDictionary<string,ReportViewer> reportViewerDictionary = new ConcurrentDictionary<string, ReportViewer>();
        private const string _staticKey = "static";
        internal static ReportViewer ReportViewer(string ID)
        {
            return reportViewerDictionary[ID ?? _staticKey];
        }
        internal static void ReportViewer(string ID, ReportViewer reportViewer)
        {
            reportViewerDictionary.TryAdd(ID ?? _staticKey, reportViewer);
        }

        internal static ReportViewer Remove(string ID )
        {
            ReportViewer reportViewer;

            if (reportViewerDictionary.TryRemove(ID ?? _staticKey, out reportViewer))
                return reportViewer;
            return null;
        }


        internal static HtmlString GetIframe(ReportViewer reportViewer, object htmlAttributes)
        {
            if (reportViewer == null)
            {
                throw new ArgumentNullException("reportViewer", "Value cannot be null.");
            }

            var dynamicID = (string)htmlAttributes.GetType().GetProperty("DynamicID").GetValue(htmlAttributes, null);

            ReportViewer(dynamicID, reportViewer);
            return IframeBuilder.Iframe(htmlAttributes);
        }

        /// <summary>
        /// Constructs a ReporViewer
        /// </summary>
        /// <param name="reportViewer">The object containing the ReportViewer properties.</param>
        /// <param name="report">An object containing the LocalReport/ServerReport properties.</param>
        /// <returns>An instance of ReportViewer with the specified properties</returns>
        public static ReportViewer AnonymousReportViewer(object reportViewer, object report)
        {
            return AnonymousReportViewer(reportViewer, report, null, null);
        }

        /// <summary>
        /// Constructs a ReporViewer with parameters
        /// </summary>
        /// <param name="reportViewer">The object containing the ReportViewer properties.</param>
        /// <param name="report">An object containing the LocalReport/ServerReport properties.</param>
        /// <param name="parameters">Object that contains the parameters for a report.</param>
        /// <returns>An instance of ReportViewer with the specified properties</returns>
        public static ReportViewer AnonymousReportViewer(object reportViewer, object report, object parameters)
        {
            return AnonymousReportViewer(reportViewer, report, parameters, null);
        }

        /// <summary>
        /// Constructs a ReporViewer with parameters and data sources.
        /// </summary>
        /// <param name="reportViewer">The object containing the ReportViewer properties.</param>
        /// <param name="report">An object containing the LocalReport/ServerReport properties.</param>
        /// <param name="parameters">Object that contains the parameters for a report.</param>
        /// <param name="dataSources">The data sources to be added to the report.</param>
        /// <returns>An instance of ReportViewer with the specified properties</returns>
        public static ReportViewer AnonymousReportViewer(object reportViewer, object report, object parameters, object dataSources)
        {
            if (reportViewer == null)
            {
                throw new ArgumentNullException("reportViewer", "Value cannot be null.");
            }
            if (report == null)
            {
                throw new ArgumentNullException("report", "Value cannot be null.");
            }

            ReportViewer reportViewerControl = ReportViewerHelper.AnonymousToReportViewer(reportViewer);

            if (reportViewerControl.ProcessingMode == ProcessingMode.Local)
            {
                reportViewerControl.LocalReport.SetProperties(ReportViewerHelper.AnonymousToLocalReport(report));

                if (parameters != null)
                {
                    reportViewerControl.LocalReport.SetParameters(ReportViewerHelper.AnonymousToReportParameter(parameters));
                }
                if (dataSources != null)
                {
                    reportViewerControl.LocalReport.DataSources.Add(ReportViewerHelper.AnonymousToReportDataSourceList(dataSources));
                }
            }
            else if (reportViewerControl.ProcessingMode == ProcessingMode.Remote)
            {
                reportViewerControl.ServerReport.SetProperties(ReportViewerHelper.AnonymousToServerReport(report));

                if (parameters != null)
                {
                    reportViewerControl.ServerReport.SetParameters(ReportViewerHelper.AnonymousToReportParameter(parameters));
                }
            }

            return reportViewerControl;
        }
    }
}
