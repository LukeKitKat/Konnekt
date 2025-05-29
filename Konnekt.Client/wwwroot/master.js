//IMPORT ALL JS FILES TO THIS

// ===== Add all window declarations here =====
window.onbeforeunload = endSessionHandler;
// ============================================


var dotNetHelper = null;

function setDotNetHelper(value) {
    dotNetHelper = value;
}

function endSessionHandler() {
    if (dotNetHelper !== undefined)
        dotNetHelper.invokeMethodAsync("EndSession", performance.navigation.type);
}

function scrollToBottom() {
    var messageBox = document.getElementById("jsMessageBox")

    if (messageBox != null)
        messageBox.scrollTo({ top: messageBox.scrollHeight, behavior: "instant" });
}