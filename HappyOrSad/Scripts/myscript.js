$(document).ready(function () {
    
    /*----------------------Image------------------*/

    $('img').width(100);
    $('img').height(100);

    $('img').mousedown(function () {
        $(this).width(120);
        $(this).height(120);
    });

    $('img').mouseover(function () {
        $(this).width(120);
        $(this).height(120);
    });

    $('img').mouseout(function () {
        $(this).width(100);
        $(this).height(100);
    });

    
    $('img').bind('click', function (e) {
        
        var value = $(this).attr("id");
        console.log('Value', value);
        callResponseEmotion(value);
        setTimeout(function () {
            $('#myModal').modal("hide");

        }, 3000);
    })

    /* Rise event for touch screen */
    $('img').bind('touchstart', function () {
        $(this).width(120);
        $(this).height(120);
    })
    
    $('img').bind('touchend', function () {
        $(this).width(100);
        $(this).height(100);
    })

    $("#sortable").so

    // Adding menu effect
    $('li>a>span').mouseover(function () {
        if(!$(this).hasClass("label"))
            $(this).addClass("menuHover");
    }).mouseout(function () {
        var selected = $(this).hasClass("selected");
        if(!selected)
            $(this).removeClass("menuHover");
    });

   

    function callResponseEmotion(score) {
        var questionId = $("#questionId").val();
        var userId = $("#userId").val();
     
        console.log('UserId: ', userId);
        var now = new Date($.now());
        var dateString = now.toLocaleDateString() + " " + now.toLocaleTimeString();
        console.log("DateTime Now", dateString);
        
        $.ajax({
            //url: 'https://mlcvillagefeedback-dev.azurewebsites.net/QuestionDisplay/ResponseEmotion',
            url: '/QuestionDisplay/ResponseEmotion',
            cache: false,
            dataType: 'json',
            contentType: 'application/json',
            
            data: {
                score: score,
                questionId: questionId,
                userId: userId,
                dateString: dateString,
            },
            type: "GET",
            success: function () {
                
                console.log("Message", "Ok")
            }
        });
    }
})


function editQuestion(questionId) {
    window.location.href = "/Questions/Edit/" + questionId;
}

function deleteQuestion(questionId) {
    window.location.href = "/Questions/Delete/" + questionId;
}


/*
$(function () {
    $("#sortable").sortable();
    $("#sortable").disableSelection();
});
*/

