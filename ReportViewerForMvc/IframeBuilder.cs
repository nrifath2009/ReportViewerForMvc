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

            ReportViewerForMvc.IframeId = GetId(parsedHtmlAttributes);

            string parsedIframe = CreateIframeTag(parsedHtmlAttributes);
            parsedIframe += ReceiveMessageScript();
            parsedIframe += SetIframeIdScript();

            return new HtmlString(parsedIframe);
        }

        private static string GetId(IDictionary<string, object> htmlAttributes)
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

        private static string CreateIframeTag(IDictionary<string, object> htmlAttributes)
        {
            string applicationPath = (HttpContext.Current.Request.ApplicationPath == "/") ? "" : HttpContext.Current.Request.ApplicationPath;

            TagBuilder tagBuilder = new TagBuilder("iframe");
            tagBuilder.GenerateId(ReportViewerForMvc.IframeId);
            tagBuilder.MergeAttribute("src", applicationPath +"/ReportViewerWebForm.aspx");
            tagBuilder.MergeAttributes(htmlAttributes, false);
            tagBuilder.SetInnerText("iframes not supported.");

            return tagBuilder.ToString();
        }

        private static string ReceiveMessageScript()
        {
            string script = "<script src=\"" + WebResourceHelper.GetWebResourceUrl(typeof(ReportViewerForMvc), "ReportViewerForMvc.Scripts.ReceiveMessage.js") + "\"></script>";

            return script;
        }

        private static string SetIframeIdScript()
        {
            string script = "<script>ReportViewerForMvc.setIframeId('" + ReportViewerForMvc.IframeId + "');</script>";

            return script;
        }
    }
}