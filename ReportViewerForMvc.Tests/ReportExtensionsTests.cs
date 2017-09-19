using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Linq;

namespace ReportViewerForMvc.Tests
{
    [TestClass]
    public class ReportExtensionsTests
    {
        TestData testData = new TestData();

        [TestMethod]
        public void SetParameters_WithReportParameterInfo()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = TestData.LocalReportPath;

            foreach (ReportParameterInfo rpi in testData.ReportViewerTests.LocalReport.GetParameters())
            {
                report.SetParameters(rpi);
            }

            TestLocalReportParameters(testData.ReportViewerTests.LocalReport, report);
        }

        [TestMethod]
        public void SetParameters_WithReportParameterInfoCollection()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = TestData.LocalReportPath;

            report.SetParameters(testData.ReportViewerTests.LocalReport.GetParameters());

            TestLocalReportParameters(testData.ReportViewerTests.LocalReport, report);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetParameters_WithNullInstance_ReportParameterInfo()
        {
            ReportExtensions.SetParameters(null, testData.ReportViewerTests.LocalReport.GetParameters()[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetParameters_WithNullInstance_ReportParameterInfoCollection()
        {
            ReportExtensions.SetParameters(null, testData.ReportViewerTests.LocalReport.GetParameters());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetParameters_WithNullReportParameterInfo()
        {
            testData.ReportViewerTests.LocalReport.SetParameters(null as ReportParameterInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetParameters_WithNullReportParameterInfoCollection()
        {
            testData.ReportViewerTests.LocalReport.SetParameters(null as ReportParameterInfoCollection);
        }

        private void TestLocalReportParameters(LocalReport expected, LocalReport actual)
        {
            ReportParameterInfoCollection collection = actual.GetParameters();

            IEnumerator<ReportParameterInfo> expectedEnum = expected.GetParameters().GetEnumerator();
            IEnumerator<ReportParameterInfo> actualEnum = actual.GetParameters().GetEnumerator();

            while (expectedEnum.MoveNext() && actualEnum.MoveNext())
            {
                Assert.AreEqual(expectedEnum.Current.Name, actualEnum.Current.Name);
                Assert.AreEqual(expectedEnum.Current.Visible, actualEnum.Current.Visible);
                Assert.IsTrue(expectedEnum.Current.Values.SequenceEqual(actualEnum.Current.Values));
            }
        }
    }
}
