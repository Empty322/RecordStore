function GetCountries() {
    var port = window.location.port ? ":" + window.location.port : "";
    var url = window.location.protocol + "//" + window.location.hostname + port + "/api/GetCountries/"
    $.get(url, function (data) {
        var countries = JSON.parse(data);
        for (var i = 0; i < countries.length; i++) {
            $("#countries-combo").append('<option value="' + countries[i].CountryName + '">' + countries[i].CountryName + '</option>');
        }
    })
}

function GetArtists() {
    var port = window.location.port ? ":" + window.location.port : "";
    var url = window.location.protocol + "//" + window.location.hostname + port + "/api/GetArtists/"
    $.get(url, function (data) {
        var artists = JSON.parse(data);
        for (var i = 0; i < artists.length; i++) {
            $("#artists-combo").append('<option value="' + artists[i].ArtistId + '">' + artists[i].ArtistId + ' ' + artists[i].Name + ' (' + artists[i].CountryName + ')</option>');
        }
    })
}

function SetDeleteButtons() {
    $(".delete-country").click(function () {
        var countryName = $(this).attr("country-name");
        var tr = $(this).parent().parent();
        $.ajax({
            url: "/api/DeleteCountry/" + countryName,
            type: 'DELETE',
            success: function (result) {
                if (result === "success")
                    tr.remove();
            }
        });
    })

    $(".delete-artist").click(function () {
        var artistId = $(this).attr("artist-id");
        var tr = $(this).parent().parent();
        $.ajax({
            url: "/api/DeleteArtist/" + artistId,
            type: 'DELETE',
            success: function (result) {
                if (result === "success")
                    tr.remove();
            }
        });
    })

    $(".delete-record").click(function () {
        var recordId = $(this).attr("record-id");
        var tr = $(this).parent().parent();
        $.ajax({
            url: "/api/DeleteRecord/" + recordId,
            type: 'DELETE',
            success: function (result) {
                if (result === "success")
                    tr.remove();
            }
        });
    })
}