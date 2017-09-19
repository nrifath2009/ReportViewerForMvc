var ReportViewerForMvc = ReportViewerForMvc || (new function () {

    var _iframeId = {};

    var resizeIframe = function (msg) {
        var height = msg.source.document.body.scrollHeight;
        var width = msg.source.document.body.scrollWidth;

        $(ReportViewerForMvc.getIframeId()).height(height);
        $(ReportViewerForMvc.getIframeId()).width(width);
    }

    var addEvent = function (element, eventName, eventHandler) {
        if (element.addEventListener) {
            element.addEventListener(eventName, eventHandler);
        } else if (element.attachEvent) {
            element.attachEvent('on' + eventName, eventHandler);
        }
    }

    this.setIframeId = function (value) {
        _iframeId = '#' + value;
    };

    this.getIframeId = function () {
        return _iframeId;
    };

    this.setAutoSize = function () {
        addEvent(window, 'message', resizeIframe);
    }

}());

ReportViewerForMvc.setAutoSize();