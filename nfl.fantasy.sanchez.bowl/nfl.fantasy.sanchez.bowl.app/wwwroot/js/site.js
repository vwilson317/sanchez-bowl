// Write your Javascript code.
$(".player-row").click(function (x, y, z) {
    debugger;
    var thisPlayerPosition = x.target.children("#position")[0];
    var thisText = thisPlayerPosition.text;
    var positionItem = thisText.split('-')[0];

    var playerRowElements = $(".player-row");

    var text = positionItem.text;
    alert(text);
})