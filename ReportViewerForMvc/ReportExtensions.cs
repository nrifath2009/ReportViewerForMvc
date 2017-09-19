using Microsoft.Reporting.WebForms;
using System;
using System.Linq;

namespace ReportViewerForMvc
{
    /// <summary>
    /// ReportExtensions helpers for ReportViewerForMvc
    /// </summary>
    public static class ReportExtensions
    {
        /// <summary>
        /// Set the ReportParameters of the specified ReportParameterInfoCollection.
        /// </summary>
        /// <param name="report">The Report that this method extends.</param>
        /// <param name="collection">The collection whose ReportParameters should be added to the Report.</param>
        public static void SetParameters(this Report report, ReportParameterInfoCollection collection)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report", "Value cannot be null.");
            }
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "Value cannot be null.");
            }

            foreach (ReportParameterInfo reportParameterInfo in collection)
            {
                report.SetParameters(reportParameterInfo);
            }
        }

        /// <summary>
        /// Set the ReportParameter of the specified ReportParameterInfo.
        /// </summary>
        /// <param name="report">The Report that this method extends.</param>
        /// <param name="reportParameterInfo">The ReportParameterInfor whose parameter should be added to the Report.</param>
        public static void SetParameters(this Report report, ReportParameterInfo reportParameterInfo)
        {
            if (report == null)
            {
                throw new ArgumentNullException("report", "Value cannot be null.");
            }

            if (reportParameterInfo == null)
            {
                throw new ArgumentNullException("reportParameterInfo", "Value cannot be null.");
            }

            ReportParameter reportParameter = new ReportParameter(
                    reportParameterInfo.Name,
                    reportParameterInfo.Values.ToArray(),
                    reportParameterInfo.Visible);

            report.SetParameters(reportParameter);
        }
    }
}
