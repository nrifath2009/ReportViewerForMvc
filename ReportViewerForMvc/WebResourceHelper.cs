using System;
using System.Web.UI;

namespace ReportViewerForMvc
{
    internal static class WebResourceHelper
    {
        internal static string GetWebResourceUrl(Type type, string resourceName)
        {
            string resourceUrl = null;

            Page page = new Page();
            try
            {
                resourceUrl = page.ClientScript.GetWebResourceUrl(type, resourceName);
            }
            catch (ArgumentNullException)
            {
                //TODO: Validate if IIS is not running. Unit testing fails due basepath is null.
                //in the meanwhile do nothing ...like my boss.
            } 

            return resourceUrl;
        }
    }

}