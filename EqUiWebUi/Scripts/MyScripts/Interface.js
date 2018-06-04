//enable general interface
function initInterface() {
    //subscrible renderd grids to events
    $('.mvc-grid').mvcgrid();
    //navbar
    $("#allcontent").animate({ "margin-top": "60px" }, "fast");
    $("#navbar").autoHidingNavbar();
    //for user popover

    $(".userhelp").popover({
        placement: "left",
        html: true,
        content: function () {
            var output = '';
            $.ajax(
                {
                    url: '/user_management/user/_Details',
                    async: false,
                    success: function (response) {
                        output = response;
                    }
                });
            return output;
        }
    });

    //for fullscreen mode
    $("#fullscreenNav").click(function () {
        $("#allcontent").removeClass("body-content");
        $("#allcontent").removeClass("container");

        $("#navbar").autoHidingNavbar('setDisableAutohide', true); //disable autohide
        $("#navbar").autoHidingNavbar('hide'); //hide navbar

        $("#allcontent").animate({ "margin-top": "0px" }, "fast");
        $("#fullscreenBody").removeClass("d-none");
        $("#footer").addClass("d-none");
    });
    //out of full screen mode
    $("#fullscreenBody").click(function () {

        $("#allcontent").addClass("body-content");
        $("#allcontent").addClass("container");

        $("#navbar").autoHidingNavbar('setDisableAutohide', false); //enable autohide
        $("#navbar").autoHidingNavbar('show'); //show navbar

        $("#allcontent").animate({ "margin-top": "60px" }, "fast");
        $("#fullscreenBody").addClass("d-none");
        $("#footer").removeClass("d-none");
    });

    //if fullscreen request in url do it.
    //else show the navbar. (hidden by default)
    if (qs("GoFullScreen")) {
        console.log("auto Full screen");
        $("#fullscreenNav").trigger("click");
    }
    else {
        //$("#navbar").removeClass("d-none")
    }

    //if zoomlevel request in url do it.
    if (qs("Zoomlevel")) {
        console.log("Set zoom level");
        //this works for Chrome, Safari, IE ref:http://jsfiddle.net/q6kebgbh/4/
        $("#allcontent").css('zoom', qs("Zoomlevel"));
    }
    //enable tooltips everywhere
    $(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    EnablePannelCollaps();
    EnableJQresultTriggerBtn();
    initToaster();
    initUrlNewWindow();
}

//script for subscribing to pannel colaps
function EnablePannelCollaps() {
    $(document).on('click', '.card-header span.clickable', function (e) {
        var $this = $(this);
        if ($this.parents('.card').find('.card-body').css('display') === 'block') {
            $this.addClass('fa-rotate-180');
            $this.parents('.card').find('.card-body').slideUp();
        } else {
            $this.removeClass('fa-rotate-180');
            $this.parents('.card').find('.card-body').slideDown();
        }
    });
}

//for blind fired buttons with feedback.
function EnableJQresultTriggerBtn() {
    $(".JQresultTriggerBtn").click(function (e) {
        e.preventDefault();
        var caller = this;
        $.toaster({ title: 'JQresultTriggerBtn', priority: 'info', message: 'Fired: ' + $(this).attr('href')});
        $.ajax({
            type: "GET",
            url: $(this).attr('href'),
            success: function (result) {
                $.toaster({ title: 'JQresultTriggerBtn', priority: 'success', message: 'Whatever you did worked!' });
            },
            error: function (result) {
                $.toaster({ title: 'JQresultTriggerBtn', priority: 'danger', message: 'Whatever you did failed!' });
            }
        });
    });
}


//get part from querystring
function qs(key) {
    key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
    var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
    return match && decodeURIComponent(match[1].replace(/\+/g, " "));
}

//set default settings for toaster
function initToaster() {
    $.toaster({
        settings: {
            'timeout': 5000, //set autodismis timeout to 5 seconds
            'donotdismiss': ['danger'] //disble autodismis for these types
        }
    });
}

//if click link has this class open it in a new window
function initUrlNewWindow() {
    $('.OpenNewWindow').click(function () {
        console.log("hit");
        window.open($(this).attr('href'), 'Maximo', 'window settings');
        return false;
    });
    
}