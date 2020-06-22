//Function only number
function onlyNumber(evt) {
    var keyPressed = (evt.which) ? evt.which : event.keyCode
    return !(keyPressed > 31 && (keyPressed < 48 || keyPressed > 57));
}