var Settings = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7128/api/Setting", {}, "json",
            function (data) {
                console.log(data)
                $('#lnkFacebook').attr("href", data.facebookLink);
                $('#lnkGoogle').attr("href", data.googleLink);
                $('#lnkTwitter').attr("href", data.twitterLink);
                $('#lnkInstagram').attr("href", data.instgramLink);
                $('#lnkYoutube').attr("href", data.youtubeLink);

                $('#website-titel').text(data.websiteName);
            },
            function (err) {
                console.error("Failed to load settings", err);
            }
        );
    }
}

Settings.GetAll();
