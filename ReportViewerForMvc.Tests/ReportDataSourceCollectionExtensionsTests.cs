using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Reporting.WebForms;
using System.Linq;

namespace ReportViewerForMvc.Tests
{
    [TestClass]
    public class ReportDataSourceCollectionExtensionsTests
    {
        private TestData testData = new TestData();        

        [TestMethod]
        public void Add_WithReportDataSourceList()
        {
            LocalReport localReport = new LocalReport();

            localReport.DataSources.Add(testData.ReportViewerTests.LocalReport.DataSources.ToList());

            Assert.AreEqual(localReport.DataSources.Count, testData.ReportViewerTests.LocalReport.DataSources.Count);
            foreach(var reportDataSource in localReport.DataSources)
            {
                Assert.AreEqual(testData.ReportViewerTests.LocalReport.DataSources[reportDataSource.Name], reportDataSource);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_WithNullInstance()
        {
            ReportDataSourceCollectionExtensions.Add(null, testData.ReportViewerTests.LocalReport.DataSources.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_WithNullCollection()
        {
            LocalReport localReport = new LocalReport();
            localReport.DataSources.Add(null);
        }

    }
}
