var databaseBookedChair = ["A1", "A2", "A3"];
var getchair = document.querySelectorAll('.chair-box');

for (var i = 0; i < databaseBookedChair.length; i++) {
    if (databaseBookedChair[i] === getchair[i].getAttribute('title')) {
        console.log(getchair[i].getAttribute('title'));
        getchair[i].setAttribute('class', 'chair-box successColor')
    }
}