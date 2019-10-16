using Microsoft.Reporting.WebForms;
using System;
using System.Web.UI.WebControls;
using Report = Microsoft.ReportingServices.Interfaces.Report;

namespace ReportViewerForMvc
{
    /// <summary>
    /// The Web Form used for rendering a ReportViewer control.
    /// </summary>
    public partial class ReportViewerWebForm : System.Web.UI.Page
    {
        private string DynamicID;

        protected void Page_Load(object sender, EventArgs e)
        {
            BuildReportViewer();
            // register eventhandler to remove the reportviewer
            ReportViewer1.Disposed += ReportViewerDisposingEventHandler;
        }

        private void BuildReportViewer()
        {
            if (!IsPostBack)
            {
                DynamicID = Request.QueryString["DynamicID"];
                ReportViewerForMvc.ReportViewer(DynamicID).ID = ReportViewer1.ID;

                ReportViewer1.SetProperties(ReportViewerForMvc.ReportViewer(DynamicID));
            }
        }

        protected void ReportViewerDisposingEventHandler(object sender, EventArgs e)
        {
            var reportViewer = ReportViewerForMvc.Remove(DynamicID);
            if (reportViewer != null)
                reportViewer.Dispose();
        }
    }
}