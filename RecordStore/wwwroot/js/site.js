let hostname = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ":" + window.location.port : "");

async function fetchGenres() {
    let url = hostname + "/api/GetGenres/";
    let genres = [];
    let response = await fetch(url);
    if (response.ok) {
        let json = await response.json();
        let data = JSON.parse(json);
        for (item of data) {
            genres.push(item);
        }
    }
    else {
        console.log("Fetching genres failed. Network response was not ok");
    }
    return genres;
}

async function fetchCountries() {
    let url = hostname + "/api/GetCountries/";
    let countries = [];
    let response = await fetch(url);
    if (response.ok) {
        let json = await response.json();
        let data = JSON.parse(json);
        for (item of data) {
            countries.push(item);
        }
    }
    else {
        console.log("Fetching countries failed. Network response was not ok");
    }
    return countries;
}

async function fetchArtists() {
    let url = hostname + "/api/GetArtists/";
    let artists = [];
    let response = await fetch(url);
    if (response.ok) {
        let json = await response.json();
        let data = JSON.parse(json);
        for (item of data) {
            artists.push(item);
        }
    }
    else {
        console.log("Fetching artists failed. Network response was not ok");
    }
    return artists;
}

async function fetchRecords() {
    let url = hostname + "/api/GetRecords/";
    let records = [];
    let response = await fetch(url);
    if (response.ok) {
        let json = await response.json();
        let data = JSON.parse(json);
        for (item of data) {
            records.push(item);
        }
    }
    else {
        console.log("Fetching records failed. Network response was not ok");
    }
    return records;
}

if (document.getElementById("admin")) {
    var admin = new Vue({
        el: "#admin",
        data: {
            genres: [],
            countries: [],
            artists: [],
            records: []
        },
        created() {
            fetchGenres().then(result => this.genres = result);
            fetchCountries().then(result => this.countries = result);
            fetchArtists().then(result => this.artists = result);
            fetchRecords().then(result => this.records = result);
        },
        methods: {
            deleteGenre: function (genreToDelete) {
                let url = hostname + "/api/DeleteGenre/" + genreToDelete;
                fetch(url, { method: "DELETE" })
                    .then(response => {
                        if (response.ok)
                            return response.text();
                        else
                            throw new Error("Deletion genre failed. Network response was not ok");
                    })
                    .then((text) => {
                        if (text === "success") {
                            let index = this.genres.findIndex(genre => genre === genreToDelete);
                            this.genres.splice(index, 1);
                            this.deleteRecordFromMass(record => record.GenreId === genreToDelete);
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            deleteCountry: function (countryToDelete) {
                let url = hostname + "/api/DeleteCountry/" + countryToDelete;
                fetch(url, {method: "DELETE"})
                    .then(response => {
                        if (response.ok)
                            return response.text();
                        else
                            throw new Error("Deletion country failed. Network response was not ok");
                    })
                    .then((text) => {
                        if (text === "success") {
                            let index = this.countries.findIndex(country => country === countryToDelete);
                            this.countries.splice(index, 1);
                            this.deleteArtistFromMass(artist => artist.CountryName === countryToDelete);
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            deleteArtistFromMass: function (predicate) {
                let index = this.artists.findIndex(predicate);
                while (index !== -1) {
                    this.deleteRecordFromMass(record => record.ArtistId === this.artists[index].ArtistId);
                    this.artists.splice(index, 1);
                    index = this.artists.findIndex(predicate);
                }
            },
            deleteArtist: function (artistIdToDelete) {
                let url = hostname + "/api/DeleteArtist/" + artistIdToDelete;
                fetch(url, { method: "DELETE" })
                    .then(response => {
                        if (response.ok)
                            return response.text();
                        else
                            throw new Error("Deletion artist failed. Network response was not ok");
                    })
                    .then((text) => {
                        if (text === "success") {
                            this.deleteArtistFromMass(artist => artist.ArtistId === artistIdToDelete);
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            },
            deleteRecordFromMass: function (predicate) {
                index = this.records.findIndex(predicate);
                while (index !== -1) {
                    this.records.splice(index, 1);
                    index = this.records.findIndex(predicate);
                }
            },
            deleteRecord: function (recordIdToDelete) {
                let url = hostname + "/api/DeleteRecord/" + recordIdToDelete;
                fetch(url, { method: "DELETE" })
                    .then(response => {
                        if (response.ok)
                            return response.text();
                        else
                            throw new Error("Deletion record failed. Network response was not ok");
                    })
                    .then((text) => {
                        if (text === "success") {
                            this.deleteRecordFromMass(record => record.RecordId === recordIdToDelete);
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            }
        }
    });
}

if (document.getElementById("cart")) {
    new Vue({
        el: "#cart",
        data: {
            products: []
        },
        computed: {
            total: function () {
                let sum = 0;
                for (item of this.products)
                    sum += item.Record.Price * item.Amount;
                return sum;
            }
        },
        created() {
            let url = hostname + "/api/GetProductsInCart/";
            fetch(url)
                .then((response) => {
                    if (response.ok)
                        return response.json();
                    else
                        throw new Error("Network response was not ok");
                })
                .then((json) => {
                    this.products.push(...JSON.parse(json));
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        methods: {
            deleteRecord: function (recordId) {
                let url = hostname + "/api/DeleteItem/" + recordId;
                fetch(url, { method: "DELETE" })
                    .then((response) => {
                        if (response.ok)
                            return response.text();
                        else
                            throw new Error("Deletion failed. Network response was not ok");
                    })
                    .then((text) => {
                        if (text === "success") {
                            let index = this.products.findIndex(item => item.RecordId === recordId);
                            this.products.splice(index, 1);
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            }
        }
    });
}

if (document.getElementById("record-list")) {
    new Vue({
        el: "#record-list",
        data: {
            recordId: 0
        },
        methods: {
            setRecordId(id) {
                this.recordId = id;
            }
        }
    });
}

if (document.getElementById("countries-combo")) {
    new Vue({
        el: "#countries-combo",
        data: {
            countries: []
        },
        created() {
            fetchCountries().then(result => this.countries = result);
        }
    });
}

if (document.getElementById("artists-combo")) {
    new Vue({
        el: "#artists-combo",
        data: {
            artists: []
        },
        created() {
            let url = hostname + "/api/GetArtists/";
            fetch(url)
                .then((response) => {
                    if (response.ok)
                        return response.json();
                    else
                        throw new Error("Network response was not ok");
                })
                .then((json) => {
                    let data = JSON.parse(json);
                    for (item of data) {
                        this.artists.push(item);
                    }
                })
                .catch((error) => {
                    console.log(error);
                });
        }
    });
}

if (document.getElementById("genres-combo")) {
    new Vue({
        el: "#genres-combo",
        data: {
            genres: []
        },
        created() {
            fetchGenres().then(result => this.genres = result);
        }
    });
}

if (document.getElementById("side-menu-genres")) {
    new Vue({
        el: "#side-menu-genres",
        data: {
            genres: []
        },
        created() {
            fetchGenres().then(result => this.genres = result);
        }
    });
}