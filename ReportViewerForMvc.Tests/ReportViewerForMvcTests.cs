using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Collections.Specialized;
using System.Linq;
using System.Collections;

namespace ReportViewerForMvc.Tests
{
    [TestClass]
    public class ReportViewerForMvcTests
    {
        TestData testData = new TestData();

        [TestMethod]
        public void AnonymousReportViewer_WithLocalReport()
        {
            ReportViewer reportViewer;

            reportViewer = ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport);

            TestObjects(TestData.AnonymousLocalReportViewer, reportViewer);
            TestObjects(TestData.AnonymousLocalReport, reportViewer.LocalReport);

            reportViewer = ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport, TestData.AnonymousLocalParameters);

            TestObjects(TestData.AnonymousLocalReportViewer, reportViewer);
            TestObjects(TestData.AnonymousLocalReport, reportViewer.LocalReport);
            TestParameters(TestData.AnonymousLocalParameters, reportViewer.LocalReport.GetParameters());

            reportViewer = ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport, TestData.AnonymousLocalParameters, testData.AnonymousDataSource);

            TestObjects(TestData.AnonymousLocalReportViewer, reportViewer);
            TestObjects(TestData.AnonymousLocalReport, reportViewer.LocalReport);
            TestParameters(TestData.AnonymousLocalParameters, reportViewer.LocalReport.GetParameters());
            TestDataSources(testData.AnonymousDataSource, reportViewer.LocalReport.DataSources);

            reportViewer = ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport, TestData.AnonymousLocalParameters, testData.AnonymousDataSourceList);

            TestObjects(TestData.AnonymousLocalReportViewer, reportViewer);
            TestObjects(TestData.AnonymousLocalReport, reportViewer.LocalReport);
            TestParameters(TestData.AnonymousLocalParameters, reportViewer.LocalReport.GetParameters());
            TestDataSources(testData.AnonymousDataSourceList, reportViewer.LocalReport.DataSources);
        }

        [TestMethod]
        public void AnonymousReportViewer_WithServerReport()
        {
            ReportViewer reportViewer;

            reportViewer = ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousServerReportViewer, TestData.AnonymousServerReport);

            TestObjects(TestData.AnonymousServerReportViewer, reportViewer);
            TestObjects(TestData.AnonymousServerReport, reportViewer.ServerReport);

            reportViewer = ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousServerReportViewer, TestData.AnonymousServerReport, TestData.AnonymousServerParameters);

            TestObjects(TestData.AnonymousServerReportViewer, reportViewer);
            TestObjects(TestData.AnonymousServerReport, reportViewer.ServerReport);
            TestParameters(TestData.AnonymousServerParameters, reportViewer.ServerReport.GetParameters());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AnonymousReportViewer_WithNullReportViewer()
        {
            ReportViewerForMvc.AnonymousReportViewer(null, TestData.AnonymousLocalReport);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AnonymousReportViewer_WithNullLocalReport()
        {
            ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AnonymousReportViewer_WithNullServerReport()
        {
            ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousServerReportViewer, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AnonymousReportViewer_WithMalformedReportViewer()
        {
            ReportViewerForMvc.AnonymousReportViewer(TestData.IncorrectData, TestData.AnonymousLocalReport);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AnonymousReportViewerr_WithMalformedLocalReport()
        {
            ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, TestData.IncorrectData);
        }

        [TestMethod]
        [ExpectedException(typeof(LocalProcessingException))]
        public void AnonymousReportViewer_WithMalformedParameters()
        {
            ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport, TestData.IncorrectData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AnonymousReportViewer_WithMalformedParameters_ServerReport()
        {
            ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousServerReportViewer, TestData.AnonymousServerReport, TestData.IncorrectData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AnonymousReportViewer_WithMalformedDataSources()
        {
            ReportViewerForMvc.AnonymousReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport, null, TestData.IncorrectData);
        }

        #region privateMethods

        private void TestObjects(object expected, object actual)
        {
            foreach (PropertyInfo propertyInfo in expected.GetType().GetProperties())
            {
                var expectedValue = expected.GetType().GetProperty(propertyInfo.Name).GetValue(expected);
                var actualValue = actual.GetType().GetProperty(propertyInfo.Name).GetValue(actual);

                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        private void TestParameters(object expected, ReportParameterInfoCollection actual)
        {
            int count = 0;

            foreach (ReportParameterInfo actualReportParameterInfo in actual)
            {
                PropertyInfo expectedPropertyInfo = expected.GetType().GetProperty(actualReportParameterInfo.Name);

                if (expectedPropertyInfo != null)
                {
                    count++;

                    Assert.AreEqual(expectedPropertyInfo.Name, actualReportParameterInfo.Name);

                    string expectedValue = expectedPropertyInfo.GetValue(expected).ToString();
                    if (String.IsNullOrEmpty(expectedValue))
                    {
                        Assert.IsTrue(actualReportParameterInfo.Values.Count == 0);
                    }
                    else
                    {
                        Assert.AreEqual(expectedValue, actualReportParameterInfo.Values[0]);
                    }
                }
            }

            Assert.IsTrue(expected.GetType().GetProperties().LongLength == count);
        }

        private void TestDataSources(object expected, ReportDataSourceCollection actual)
        {
            foreach (ReportDataSource actualReportDataSource in actual)
            {
                if (expected.GetType().IsArray)
                {
                    bool isDataSourceFound = false;
                    foreach (var expectedReportDataSource in (IEnumerable)expected)
                    {
                        if (expectedReportDataSource.GetType().GetProperty("Name").GetValue(expectedReportDataSource).ToString() == actualReportDataSource.Name)
                        {
                            isDataSourceFound = true;
                            TestObjects(expectedReportDataSource, actualReportDataSource);
                            break;
                        }
                    }

                    if(!isDataSourceFound)
                    {
                        Assert.Fail();
                    }
                }
                else
                {
                    TestObjects(expected, actualReportDataSource);
                }
            }
        }

        #endregion
    }
}
