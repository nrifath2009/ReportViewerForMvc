using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ReportViewerForMvc.Tests
{
    public class TestViewDataContainer : IViewDataContainer
    {
        private ViewDataDictionary viewData = new ViewDataDictionary();
        public ViewDataDictionary ViewData
        {
            get
            {
                return this.viewData;
            }
            set
            {
                this.viewData = value;
            }
        }
    }
}
