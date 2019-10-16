using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ReportViewerForMvc
{
    internal class IframeBuilder
    {
        internal static HtmlString Iframe(object htmlAttributes)
        {
            IDictionary<string, object> parsedHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var iframeID = GetId(parsedHtmlAttributes);

            string parsedIframe = CreateIframeTag(parsedHtmlAttributes, iframeID);
            parsedIframe += ReceiveMessageScript();
            parsedIframe += SetIframeIdScript(iframeID);

            return new HtmlString(parsedIframe);
        }

        public static string GetId(IDictionary<string, object> htmlAttributes)
        {
            string id;

            if (htmlAttributes["id"] == null)
            {
                id = "r" + Guid.NewGuid().ToString();
            }
            else
            {
                id = TagBuilder.CreateSanitizedId(htmlAttributes["id"].ToString());

                if (id == null)
                {
                    throw new ArgumentNullException("htmlAttributes.id", "Value cannot be null.");
                }
            }

            return id;
        }

        public static string CreateIframeTag(IDictionary<string, object> htmlAttributes, string iframeID)
        {
            string applicationPath = (HttpContext.Current.Request.ApplicationPath == "/") ? "" : HttpContext.Current.Request.ApplicationPath;

            TagBuilder tagBuilder = new TagBuilder("iframe");
            tagBuilder.GenerateId(iframeID);
            tagBuilder.MergeAttribute("src", applicationPath +"/ReportViewerWebForm.aspx?DynamicID=" + htmlAttributes["DynamicID"]);
            tagBuilder.MergeAttributes(htmlAttributes, false);
            tagBuilder.SetInnerText("iframes not supported.");

            return tagBuilder.ToString();
        }

        public static string ReceiveMessageScript()
        {
            string script = "<script src=\"" + WebResourceHelper.GetWebResourceUrl(typeof(ReportViewerForMvc), "ReportViewerForMvc.Scripts.ReceiveMessage.js") + "\"></script>";

            return script;
        }

        public static string SetIframeIdScript(string iframeID)
        {
            return "<script>ReportViewerForMvc.setIframeId('" + iframeID + "');</script>";
        }
    }
}