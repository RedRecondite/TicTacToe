function requestStatusUpdate() {
	"use strict";
	$.getJSON("/GameState", function(data) {
		setTimeout(function() {
			// Update status of game!
			$("#box1").html( data.boxes[0] );
			$("#box2").html( data.boxes[1] );
			$("#box3").html( data.boxes[2] );
			$("#box4").html( data.boxes[3] );
			$("#box5").html( data.boxes[4] );
			$("#box6").html( data.boxes[5] );
			$("#box7").html( data.boxes[6] );
			$("#box8").html( data.boxes[7] );
			$("#box9").html( data.boxes[8] );
			$("#status").html( data.status );

			//requestStatusUpdate();
		}, 1000 );
	} );
}

$("#watcher").tap(function() {
	"use strict";
	requestStatusUpdate();
	$.mobile.changePage( "#game" );
});

$("#player").tap(function() {
	"use strict";
	// See if a spot is available
	// Ask for player's name
	// See if a spot is still available
	// Start game
	$.mobile.changePage( "#game" );
});

$("#signinbutton").tap(function() {
	"use strict";
	$.getJSON("/SignIn?username=" + $("#username").val(), function(data) {
		// TODO
	} );
});