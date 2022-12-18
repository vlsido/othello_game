"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();

document.getElementsByClassName("game-table-cell").disabled = true;


connection.on("RefreshPage",
    function () {
        location.reload();
    });

connection.start().then(function() {
    document.getElementsByClassName("game-table-cell").disabled = false;
}).catch(function(err) {
    return console.error(err.toString());
});

/*var tableCells = document.getElementsByClassName("valid-piece");*/

function sleep(milliseconds) {
    return new Promise(resolve => setTimeout(resolve, milliseconds));
}  

function refresh(event) {
    sleep(2000);
    connection.invoke("RefreshTask").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
};


//for (var i = 0; i < tableCells.length; i++) {
//    tableCells[i].addEventListener("click",
//        function (event) {
//            connection.invoke("RefreshTask").catch(function (err) {
//                return console.error(err.toString());
//            });
//            event.preventDefault();
//        });
//}
