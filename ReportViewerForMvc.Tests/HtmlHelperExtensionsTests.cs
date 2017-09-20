using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace ReportViewerForMvc.Tests
{
    [TestClass]
    public class HtmlHelperExtensionsTests
    {
        private HtmlHelper htmlHelper = new HtmlHelper(new ViewContext(), new TestViewDataContainer());
        private TestData testData = new TestData();

        #region ControlTests

        [TestMethod]
        public void ReportViewer_WellFormedIframe()
        {
            MockHttpContext();

            HtmlString htmlString;

            htmlString = htmlHelper.ReportViewer(testData.ReportViewerTests);
            TestWellformedHtmlDefaultAttributes(htmlString);

            htmlString = htmlHelper.ReportViewer(testData.ReportViewerTests, TestData.HtmlAttributes);
            TestWellformedHtmlCustomAttributes(htmlString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportViewer_WithNullReportViewer_NoHtmlAttributes()
        {
            htmlHelper.ReportViewer(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportViewer_WithNullReportViewer()
        {
            htmlHelper.ReportViewer(null, TestData.HtmlAttributes);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportViewer_WithNullHtmlId()
        {
            htmlHelper.ReportViewer(testData.ReportViewerTests, new { id = "" });
        }

        #endregion

        #region AnonymousTests

        [TestMethod]
        public void ReportViewer_WithAnonymousLocalReport()
        {
            MockHttpContext();

            HtmlString htmlString;

            htmlString = htmlHelper.ReportViewer(
                TestData.AnonymousLocalReportViewer,
                TestData.AnonymousLocalReport,
                TestData.AnonymousLocalParameters,
                testData.AnonymousDataSource,
                null);

            TestWellformedHtmlDefaultAttributes(htmlString);

            htmlString = htmlHelper.ReportViewer(
                TestData.AnonymousLocalReportViewer,
                TestData.AnonymousLocalReport,
                TestData.AnonymousLocalParameters,
                testData.AnonymousDataSourceList,
                TestData.HtmlAttributes);

            TestWellformedHtmlCustomAttributes(htmlString);
        }

        [TestMethod]
        public void ReportViewer_WithAnonymousServerReport()
        {
            MockHttpContext();

            HtmlString htmlString;

            htmlString = htmlHelper.ReportViewer(
                TestData.AnonymousServerReportViewer,
                TestData.AnonymousServerReport,
                TestData.AnonymousServerParameters,
                null);

            TestWellformedHtmlDefaultAttributes(htmlString);

            htmlString = htmlHelper.ReportViewer(
                TestData.AnonymousServerReportViewer,
                TestData.AnonymousServerReport,
                TestData.AnonymousServerParameters,
                TestData.HtmlAttributes);

            TestWellformedHtmlCustomAttributes(htmlString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportViewer_WithNullAnonymousReportViewer()
        {
            htmlHelper.ReportViewer((object)null, TestData.AnonymousLocalReport);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReportViewer_WithNullAnonymousReport()
        {
            htmlHelper.ReportViewer(TestData.AnonymousLocalReportViewer, (object)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReportViewer_WithMalformedAnonymousReportViewer()
        {
            htmlHelper.ReportViewer(TestData.IncorrectData, TestData.AnonymousLocalReport);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReportViewer_WithMalformedAnonymousLocalReport()
        {
            htmlHelper.ReportViewer(TestData.AnonymousLocalReportViewer, TestData.IncorrectData);
        }

        [TestMethod]
        [ExpectedException(typeof(LocalProcessingException))]
        public void ReportViewer_WithMalformedAnonymousParameters()
        {
            htmlHelper.ReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport, TestData.IncorrectData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReportViewer_WithMalformedAnonymousParameters_ServerReport()
        {
            htmlHelper.ReportViewer(TestData.AnonymousServerReportViewer, TestData.AnonymousServerReport, TestData.IncorrectData, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReportViewer_WithMalformedAnonymousDataSources()
        {
            htmlHelper.ReportViewer(TestData.AnonymousLocalReportViewer, TestData.AnonymousLocalReport, null, TestData.IncorrectData, null);
        }

        #endregion

        #region PrivateMethods

        private void TestWellformedHtmlDefaultAttributes(HtmlString htmlString)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString.ToString());
            HtmlNode iframeResult = htmlDocument.DocumentNode.ChildNodes[0];

            Assert.AreEqual("iframe", htmlDocument.DocumentNode.ChildNodes[0].Name);
            Assert.AreEqual("script", htmlDocument.DocumentNode.ChildNodes[1].Name);
            Assert.AreEqual("script", htmlDocument.DocumentNode.ChildNodes[2].Name);

            TestDefaultIframeAttributes(iframeResult);
        }

        private void TestWellformedHtmlCustomAttributes(HtmlString htmlString)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString.ToString());
            HtmlNode iframeResult = htmlDocument.DocumentNode.ChildNodes[0];

            Assert.AreEqual("iframe", htmlDocument.DocumentNode.ChildNodes[0].Name);
            Assert.AreEqual("script", htmlDocument.DocumentNode.ChildNodes[1].Name);
            Assert.AreEqual("script", htmlDocument.DocumentNode.ChildNodes[2].Name);

            TestCustomIframeAttributes(iframeResult);
        }

        private void TestDefaultIframeAttributes(HtmlNode iframeResult)
        {
            Assert.AreEqual("iframe", iframeResult.Name);
            Assert.IsNotNull(iframeResult.Id);
            Assert.IsTrue(iframeResult.Id != "");
            Assert.AreEqual(TestData.DefaultSrc, iframeResult.GetAttributeValue("src", "src not found"));
        }

        private void TestCustomIframeAttributes(HtmlNode iframeResult)
        {
            foreach (var property in TestData.HtmlAttributes.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(TestData.HtmlAttributes);
                Assert.AreEqual(propertyValue, iframeResult.GetAttributeValue(property.Name, null));
            }
        }

        private void MockHttpContext()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );
        }

        #endregion
    }
}
