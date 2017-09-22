# ReportViewer for MVC

ReportViewer for MVC is a simple library that makes it possible to use an ASP.NET ReportViewer control in an ASP.NET MVC application.

It provides a set of HTML Helpers and all of it's dependencies for displaying a report. Local or server, it handles all.

## Why do I need this?

Server controls (like ReportViewer) cannot be used within Razor views. In order to use a control, you would need to add an ASPX view page and all of it's configurations, as well as, work through the code.

This library will setup all of that work for you, and will provide easy access to display your report. Also, it will auto-resize the report on your webpage to get the desired display.

## Where can I get it?

Download & install from [NuGet](https://www.nuget.org/packages/ReportViewerForMvc/).

```PowerShell
PM> Install-Package ReportViewerForMvc
```

## How do I use it?

After installing, the simplest solution is to setup the report on the controller and render it on the view.

The example below, will configure a report on localhost and auto-resize it. Check more details on the [Getting Started](https://github.com/armanio123/ReportViewerForMvc/wiki/Getting-Started) page.

Controller:

```C#
var reportViewer = new ReportViewer()
{
    ProcessingMode = ProcessingMode.Remote,
    SizeToReportContent = true,
    Width = Unit.Percentage(100),
    Height = Unit.Percentage(100),
};            

reportViewer.ServerReport.ReportPath = "/ReportFolder/SampleReport";
reportViewer.ServerReport.ReportServerUrl = new Uri("http://localhost/ReportServer/");

ViewBag.ReportViewer = reportViewer;
```

View:

```C#
@Html.ReportViewer(
    ViewBag.ReportViewer as Microsoft.Reporting.WebForms.ReportViewer, 
    new { scrolling = "no" })
```

## More info

Check the [Wiki](https://github.com/armanio123/ReportViewerForMvc/wiki) for the project.