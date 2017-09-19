using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;

namespace ReportViewerForMvc
{
    /// <summary>
    /// ReportDataSourceCollectionExtensions helpers for ReportViewerForMvc
    /// </summary>
    public static class ReportDataSourceCollectionExtensions
    {
        /// <summary>
        /// Adds the elements of the specified collection to the end of the ReportDataSourceCollection.
        /// </summary>
        /// <param name="reportDataSourceCollection">The ReportDataSourceCollection that this method extends.</param>
        /// <param name="collection">The collection whose elements should be added to the end of the ReportDataSourceCollection.</param>
        public static void Add(this ReportDataSourceCollection reportDataSourceCollection, IEnumerable<ReportDataSource> collection)
        {
            if (reportDataSourceCollection == null)
            {
                throw new ArgumentNullException("reportDataSourceCollection", "Value cannot be null.");
            }
            if (collection == null)
            {
                throw new ArgumentNullException("collection", "Value cannot be null.");
            }

            foreach (ReportDataSource reportDataSource in collection)
            {
                reportDataSourceCollection.Add(reportDataSource);
            }
        }
    }
}
