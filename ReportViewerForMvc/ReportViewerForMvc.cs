using Microsoft.Reporting.WebForms;
using System;
using System.Web;

namespace ReportViewerForMvc
{
    /// <summary>
    /// Encapsulates the methods and properties used for the ReportViewerForMvc extension. 
    /// </summary>
    public static class ReportViewerForMvc
    {
        internal static string IframeId { get; set; }

        private static ReportViewer reportViewer;
        internal static ReportViewer ReportViewer
        {
            get { return reportViewer; }
            set
            {
                //TODO: Implement dynamic ID
                reportViewer = value;
                reportViewer.ID = "ReportViewer1";
            }
        }

        internal static HtmlString GetIframe(ReportViewer reportViewer, object htmlAttributes)
        {
            if (reportViewer == null)
            {
                throw new ArgumentNullException("reportViewer", "Value cannot be null.");
            }

            ReportViewerForMvc.ReportViewer = reportViewer;
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
