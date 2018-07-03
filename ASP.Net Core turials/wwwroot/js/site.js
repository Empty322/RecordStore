function GetCountries() {
    var port = window.location.port ? ":" + window.location.port : "";
    var url = window.location.protocol + "//" + window.location.hostname + port + "/api/GetCountries/"
    $.get(url, function (data) {
        alert(data);
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
        alert(data);
        var artists = JSON.parse(data);
        for (var i = 0; i < artists.length; i++) {
            $("#artists-combo").append('<option value="' + artists[i].ArtistId + '">' + artists[i].ArtistId + ' ' + artists[i].Name + ' (' + artists[i].CountryName + ')</option>');
        }
    })
}