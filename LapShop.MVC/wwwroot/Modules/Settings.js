var Settings = {
    GetAll: function () {
        Helper.AjaxCallGet("https://localhost:7128/api/Setting", {}, "json",
            function (data) {

                $('.logo').attr("src", `/Uploads/Settings/${data.logo}`);

                $('#website-titel').text(data.websiteName);


                $('#lnkFacebook').attr("href", data.facebookLink);
                $('#lnkGoogle').attr("href", data.googleLink);
                $('#lnkTwitter').attr("href", data.twitterLink);
                $('#lnkInstagram').attr("href", data.instgramLink);
                $('#lnkYoutube').attr("href", data.youtubeLink);

                $('#middleBanner').attr("src", `/Uploads/Settings/${data.middleBanner}`);
                $('#div-middleBanner').css("background-image", `url(/Uploads/Settings/${data.middleBanner})`);

                $('#lastBanner').attr("src", `/Uploads/Settings/${data.lastBanner}`);


            },
            function (err) {
                console.error("Failed to load settings", err);
            }
        );
    }
}

Settings.GetAll();
