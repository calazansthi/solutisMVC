function SetIdCidade(id) {
    $("#idCidade").val(id);
}

function Delete() {
    var id = $("#idCidade").val();
    var url = $("#delete-action").val();

    $.ajax({
        type: "Post",
        url: url,
        data: {
            "id": id,
        },
        success: function () {
            $("#linha-" + id + "").fadeOut(function () {
                $("#linha-" + id + "").remove();

                var linhasTabela = $("#table tr").length;
                if (linhasTabela == 1) {
                    $("#table").fadeOut(function () {
                        $("#table").remove();
                        var semCidade = $("<h4>Não há cidades cadastradas</h4>").hide().fadeIn();
                        $("#info").append(semCidade);
                    });
                }
            });
        },
        error: function (ajaxOptions, thrownError) {
        },
    });
}