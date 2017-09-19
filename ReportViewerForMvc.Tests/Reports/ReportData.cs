using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportViewerForMvc.Tests.Reports
{
    public class ReportData
    {
    }

    public class ReportTable : DataTable
    {
        public ReportTable()
        {
            Columns.Add("Column1");
            Columns.Add("Column2");
            Columns.Add("Column3");
        }
    }
}

