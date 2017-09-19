using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Reporting.WebForms;
using System.Linq;
using System.Collections.Generic;

namespace ReportViewerForMvc.Tests
{
    [TestClass]
    public class ReportViewerExtensionsTests
    {
        TestData testData = new TestData();

        [TestMethod]
        public void SetProperties_WithReportViewer()
        {
            ReportViewer reportViewer = new ReportViewer();
            reportViewer.SetProperties(testData.ReportViewerTests);

            CompareReportViewer(testData.ReportViewerTests, reportViewer);
        }

        [TestMethod]
        public void SetProperties_WithLocalReport()
        {
            LocalReport localReport = new LocalReport();
            localReport.SetProperties(testData.ReportViewerTests.LocalReport);

            CompareLocalReport(testData.ReportViewerTests.LocalReport, localReport);
        }

        [TestMethod]
        public void SetProperties_WithServerReport()
        {
            ServerReport serverReport = new ServerReport();
            serverReport.SetProperties(testData.ReportViewerTests.ServerReport);

            CompareServerReport(testData.ReportViewerTests.ServerReport, serverReport);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProperties_WithNullReportViewerIntance()
        {
            ReportViewerExtensions.SetProperties(null, testData.ReportViewerTests);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProperties_WithNullLocalReportIntance()
        {
            ReportViewerExtensions.SetProperties(null, testData.ReportViewerTests.LocalReport);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProperties_WithNullServerReportIntance()
        {
            ReportViewerExtensions.SetProperties(null, testData.ReportViewerTests.ServerReport);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProperties_WithNullReportViewer()
        {
            testData.ReportViewerTests.SetProperties(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProperties_WithNullLocalReport()
        {
            testData.ReportViewerTests.LocalReport.SetProperties(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProperties_WithNullServerReport()
        {
            testData.ReportViewerTests.ServerReport.SetProperties(null);
        }


        private void CompareReportViewer(ReportViewer expected, ReportViewer current)
        {
            Assert.AreEqual(expected.ProcessingMode, current.ProcessingMode);
            Assert.AreEqual(expected.SizeToReportContent, current.SizeToReportContent);
            Assert.AreEqual(expected.Width, current.Width);
            Assert.AreEqual(expected.Height, current.Height);

            CompareLocalReport(expected.LocalReport, current.LocalReport);
            CompareServerReport(expected.ServerReport, current.ServerReport);
        }

        private void CompareLocalReport(LocalReport expected, LocalReport current)
        {
            Assert.AreEqual(expected.ReportPath, current.ReportPath);

            TestParameters(expected.GetParameters().ToList(), current.GetParameters().ToList());
            TestDataSource(expected.DataSources.ToList(), current.DataSources.ToList());
        }

        private void CompareServerReport(ServerReport expected, ServerReport current)
        {
            Assert.AreEqual(expected.ReportPath, current.ReportPath);
            Assert.AreEqual(expected.ReportServerUrl, current.ReportServerUrl);

            TestParameters(expected.GetParameters().ToList(), current.GetParameters().ToList());
        }

        private void TestParameters(List<ReportParameterInfo> expected, List<ReportParameterInfo> current)
        {
            Assert.AreEqual(expected.Count, current.Count);

            for (int a = 0; a < expected.Count; a++)
            {
                Assert.AreEqual(expected[a].Name, current[a].Name);
                Assert.AreEqual(expected[a].Values.Count, current[a].Values.Count);
                for (int b = 0; b < expected[a].Values.Count; b++)
                {
                    Assert.AreEqual(expected[a].Values[b], current[a].Values[b]);
                }
                Assert.AreEqual(expected[a].Visible, current[a].Visible);
            }
        }

        private void TestDataSource(List<ReportDataSource> expected, List<ReportDataSource> current)
        {
            Assert.AreEqual(expected.Count, current.Count);
            for (int a = 0; a < expected.Count; a++)
            {
                Assert.AreEqual(expected[a].Name, current[a].Name);
                Assert.AreEqual(expected[a].Value, current[a].Value);
            }
        }

    }
}
