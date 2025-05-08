//IMPORT ALL JS FILES TO THIS

const dotNetHelper = null;

function setDotNetHelper(value) {
    dotNetHelper = value;
}

function endSessionHandler() {
    await dotNetHelper.invokeMethodAsync("EndSession");
}

window.onbeforeunload = endSessionHandler;