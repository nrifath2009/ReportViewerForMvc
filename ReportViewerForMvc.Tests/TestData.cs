using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Data;
using ReportViewerForMvc.Tests.Reports;
using System.IO;
using System.Reflection;

namespace ReportViewerForMvc.Tests
{
    public class TestData
    {
        public const string DefaultSrc = "/ReportViewerWebForm.aspx";
        public const string DefaultStyle = "border:none; width:100%; height:100%;";
        public const string DefaultInnerText = "iframes not supported.";
        public static readonly string LocalReportPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Reports\Report1.rdlc";
        public const string ServerReportPath = "/AdventureWorks 2012/Sales_by_Region";
        public const string ReportServerUrl = "http://localhost/ReportServer/";

        public readonly ReportViewer ReportViewerTests;
        public readonly List<ReportParameter> ReportParameterList;

        #region AnonymousProperties

        #region Local

        public static readonly object AnonymousLocalReportViewer = new
        {
            ProcessingMode = ProcessingMode.Local,
            SizeToReportContent = true,
            Width = Unit.Percentage(100),
            Height = Unit.Percentage(100),
        };

        public static readonly object AnonymousLocalReport = new
        {
            ReportPath = LocalReportPath
        };

        public static readonly object AnonymousLocalParameters = new
        {
            ReportParameter1 = "",
            ReportParameter2 = "Value2"
        };

        public readonly object AnonymousDataSourceList;
        public readonly object AnonymousDataSource;

        #endregion

        #region Server

        public static readonly object AnonymousServerReportViewer = new
        {
            ProcessingMode = ProcessingMode.Remote,
            SizeToReportContent = true,
            Width = Unit.Percentage(100),
            Height = Unit.Percentage(100),
        };

        public static readonly object AnonymousServerReport = new
        {
            ReportPath = ServerReportPath,
            ReportServerUrl = new Uri(ReportServerUrl)
        };

        public static readonly object AnonymousServerParameters = new
        {
            ShowBingMaps = "Visible",
            ShowAll = "True"
        };

        #endregion

        public static readonly object HtmlAttributes = new
        {
            height = "10",
            innertText = "Changed inner text attribute.",
            name = "testName",
            seamless = "seamless",
            style = "border:1px solid red;",
            width = "20"
        };

        public static readonly object IncorrectData = new
        {
            You = "Got",
            Nothing = "Son"
        };

        #endregion

        public TestData()
        {
            //Prepare LocalReport parameters
            ReportParameterList = new List<ReportParameter>();
            ReportParameterList.Add(new ReportParameter("ReportParameter1"));
            ReportParameterList.Add(new ReportParameter("ReportParameter2", "Value2"));
            ReportParameterList.Add(new ReportParameter("ReportParameter3", new string[] { "Array1", "Array2", "Array3" }));
            ReportParameterList.Add(new ReportParameter("ReportParameter4", "Value4", true));
            ReportParameterList.Add(new ReportParameter("ReportParameter5", new string[] { "Array1", "Array2", "Array3" }, true));

            //Prepare LocalReport datasource
            List<object> dataList = new List<object>();
            for (int i = 0; i <= 10; i++)
            {
                var data = new
                {
                    Column1 = "ValueA" + i.ToString(),
                    Column2 = "ValueB" + i.ToString(),
                    Column3 = "ValueC" + i.ToString()
                };

                dataList.Add(data);
            }

            //ReportViewer properties
            ReportViewerTests = new ReportViewer();
            ReportViewerTests.ProcessingMode = ProcessingMode.Local;
            ReportViewerTests.SizeToReportContent = true;
            ReportViewerTests.Width = Unit.Percentage(100);
            ReportViewerTests.Height = Unit.Percentage(100);

            //LocalReport properties
            ReportViewerTests.LocalReport.ReportPath = LocalReportPath;
            ReportViewerTests.LocalReport.DataSources.Add(new ReportDataSource("ReportViewerForMvcTestsReport", dataList));
            ReportViewerTests.LocalReport.DataSources.Add(new ReportDataSource("ReportViewerForMvcTestsReport1", dataList));
            ReportViewerTests.LocalReport.DataSources.Add(new ReportDataSource("ReportViewerForMvcTestsReport2", dataList));
            ReportViewerTests.LocalReport.SetParameters(ReportParameterList);

            //ServerReport properties
            ReportViewerTests.ServerReport.ReportPath = ServerReportPath;
            ReportViewerTests.ServerReport.ReportServerUrl = new Uri(ReportServerUrl);
            ReportViewerTests.ServerReport.SetParameters(GetParametersServer());

            //Set anonymous DataSource
            AnonymousDataSourceList = new[]
            {
                new { Name = "ReportViewerForMvcTestsReport", Value = dataList },
                new { Name = "ReportViewerForMvcTestsReport1", Value = dataList },
                new { Name = "ReportViewerForMvcTestsReport2", Value = dataList },
            };
            AnonymousDataSource = new
            {
                Name = "ReportViewerForMvcTestsReport2",
                Value = dataList
            };
        }

        private ReportParameter[] GetParametersServer()
        {
            ReportParameter p1 = new ReportParameter("ShowBingMaps", "Visible");
            ReportParameter p2 = new ReportParameter("ShowAll", "True");
            return new ReportParameter[] { p1, p2 };
        }
    }
}