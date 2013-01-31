function requestStatusUpdate() {
	"use strict";
	setInterval(function() {
		$.getJSON("/GameState", function(data) {
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
		} );
	}, 100 );
}

$("#watcher").tap(function() {
	"use strict";
	requestStatusUpdate();
	$('#game-page-state').text( "Watching" );
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
		if ( data["success"] == false )
		{
		  $('#signin-error-message').text( data["message"] );
		  $('#signin-error-popup').popup( "open" );
		}
		else
		{
		  requestStatusUpdate();
		  $('#game-page-state').text( "Playing as " + data["name"] );
		  $.mobile.changePage( "#game" );
		}
	} );
});

$(document).ready(function() 
{
	"use strict";
	var styles = { 'height': '48px', 'line-height': '48px', 'font-size': '48px' };
	$('.tic-tac-toe-button').css(styles);
});

$(".tic-tac-toe-button").tap(function() {
	"use strict";
	$.getJSON("/Move?box=" + this.id.substring( 3 ), function(data) {
	} );
});