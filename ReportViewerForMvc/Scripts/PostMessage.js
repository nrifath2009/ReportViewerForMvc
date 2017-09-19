Sys.Application.add_load(function () {
    $find("ReportViewer1").add_propertyChanged(viewerPropertyChanged);
});

function viewerPropertyChanged(sender, e) {
    if (e.get_propertyName() === "isLoading") {
        top.postMessage("", '*'); //Trigger resize.
    }
}

//TODO: Get control ID dynamically.