::nuget pack ..\ReportViewerForMvc\ReportViewerForMvc.csproj -Properties Configuration=Release
nuget pack ReportViewerForMvc.nuspec -Exclude *.bat
pause