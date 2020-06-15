
src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"
src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.11.1/jquery.validate.min.js"
src="http://ajax.aspnetcdn.com/ajax/mvc/4.0/jquery.validate.unobtrusive.min.js"


var openCards = [];

$(document).ready(function () {
       
    $("#hit").click(function (e) {
        $("#hit").addClass("disabled");
        e.preventDefault();
        //get card for player
        getCard("#player");
        return false;
    });

    $("#hit").addClass("disabled");
    getCard("#player");

	$("#stand").click(function (e) {
        e.preventDefault();
        $("#stand").addClass("disabled");
        $("#hit").addClass("disabled");
        //get card for player
        $("#ai").find(".card").remove();
        getCard("#ai");
        return false;
    });


	function getCard(target) {
        $.ajax({
            type: "POST",
            dataType: "json",
            contentType: 'application/json',
            url: '@Url.Action("Hit","Home")',
            data: JSON.stringify({ cards: openCards }),
            success: function (openCard) {
                openCards.push({ RankId: openCard.RankId, SuitId: openCard.SuitId });
                drawCardAndRecalculate(target, openCard);
            }
        });
    }

	function drawCardAndRecalculate(id, openCard) {
		var card = $("<div />").addClass("card").addClass(openCard.SuitDisplay.toLowerCase());
        card = card.append(openCard.RankDisplay);
		card = card.append("<span class=\"suit\"/>");
        $(id).append(card);
		if (id == "#player") {
            $("#hit").removeClass("disabled");
        }
        increaseWeight(id, openCard);
    }

    function increaseWeight(id, openCard) {
        var w = parseInt($(id).find(".weight").text());
        w = w + openCard.Weight;
        $(id).find(".weight").text(w);

        if (id == "#player") {
            if (w > 21) {
                lose(id);
            }
            if (w == 21) {
                $("#stand").click();
            }
        } else {
            if (w <= 18) {
                setTimeout(function () { getCard("#ai"); }, 1);
            } else {
                if (w > 21) {
                    win();
                } else {//compare
                    var aiW = parseInt($("#ai").find(".weight").text());
        var playerW = parseInt($("#player").find(".weight").text());
        if (playerW > aiW) win();
        else if (playerW == aiW) draw();
        else lose();
    }
}


}
}

	function lose() {
					var bet = parseInt($("#Bet").val());
        var stack = parseInt($("#Stack").val());
        $("#Stack").val(stack - bet);

        $("#player").find(".weight").addClass("lose");
        $("#hit").addClass("disabled");
        $("#stand").addClass("disabled");
        $("#again").show();
        $(".message").text("You Lose!!!");
    }

	function win() {
					var bet = parseInt($("#Bet").val());
        var stack = parseInt($("#Stack").val());
        $("#Stack").val(stack + bet);

        $("#player").find(".weight").addClass("win");
        $("#hit").addClass("disabled");
        $("#stand").addClass("disabled");
        $("#again").show();
        $(".message").text("You Won!!!");
    }

	function draw() {
            $("#hit").addClass("disabled");
        $("#stand").addClass("disabled");
        $("#again").show();
        $(".message").text("Draw!!!");
    }
			});