$('document').ready(function () {
    $("#formCidade").submit(function (e) {
        var nome = $("#nome").val();
        var url = $("#url-action").val();                

        $.ajax({
            type: "Post",
            url: url,
            data: {
                "nome": nome,
            },
            beforeSend: function () {
                if ($("#erro").text() != "") {
                    $("#erro").fadeOut("fast", function () {
                        $("#erro").empty();
                    });
                }
            },
            success: function (data) {                
                if (data.success == false) {
                    $("#erro").show(function () {
                        $("#erro").append(data.message).hide().fadeIn();
                    });
                }
                else {
                    $("#ok").show(function () {
                        $("#ok").append(data.message).hide().fadeIn();
                    });
                    window.location.replace("/");
                }
            },
            error: function (ajaxOptions, thrownError) {
                alert(thrownError);
            },
        });
        return false;
    });
});