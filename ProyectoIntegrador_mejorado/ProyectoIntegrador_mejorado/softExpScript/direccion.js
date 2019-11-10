function getProvincia() {
    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincias.json",
        data: {},
        success: function (data) {
            var html = "";
            for (key in data) {
                html += "<option value='" + key + "'>" + data[key] + "</option>";
            }
            $('#provincia').empty();
            $("#provincia").append(html);
        }
    });
}

function getCanton() {

    var id_province = $("#provincia").val();

    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincia/" + id_province + "/cantones.json",
        data: {},
        success: function (data) {
            var html = "";
            for (key in data) {
                html += "<option value='" + key + "'>" + data[key] + "</option>";
            }
            $("#canton").empty();
            $("#canton").append(html);
        }
    });
}

function getDistrict() {

    var id_province = $("#provincia").val();
    var id_canton = $("#canton").val();

    $.ajax({
        dataType: "json",
        url: "https://ubicaciones.paginasweb.cr/provincia/" + id_province + "/canton/" + id_canton + "/distritos.json",
        data: {},
        success: function (data) {
            var html = "";
            for (key in data) {
                html += "<option value='" + key + "'>" + data[key] + "</option>";
            }
            console.log(data);
            $("#distrito").empty();
            $("#distrito").append(html);
        }
    });
}