//enable general interface
function initInterface() {
    //subscrible renderd grids to events
    $('.mvc-grid').mvcgrid();
    //navbar
    $("#allcontent").animate({ "margin-top": "60px" }, "fast");
    $("#navbar").autoHidingNavbar();

    //initpopovers
    $(".MyPopovers").click(function (e) {
        e.preventDefault();
        return false;
    });
    $(".MyPopovers").popover({
        html: true,
        content: function () {
            return $.ajax({
                url: $(this).attr('href'),
                dataType: 'html',
                async: false
            }).responseText;
        }
    });

    //hide popovers on body click
    $('body').on('click', function (e) {
        $('[data-toggle="popover"]').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    });

    //for new window option
    $('.OpenNewWindow').click(function (e) {
        e.preventDefault();
        console.log("Opening new window");
        window.open($(this).attr('href'), 'EQUI', 'window settings');
        return false;
    });

    //for toaster
    $.toaster({
        settings: {
            'timeout': 5000, //set autodismis timeout to 5 seconds
            'donotdismiss': ['danger'] //disble autodismis for these types
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
}

//script for subscribing to pannel colaps
function EnablePannelCollaps() {
    $(document).on('click', '.card-header span.clickable', function (e) {
        var $this = $(this);
        if ($this.closest('.card').find('.card-body').css('display') === 'block') {
            $this.addClass('fa-rotate-180');
            $this.closest('.card').find('.card-body').slideUp();
        } else {
            $this.removeClass('fa-rotate-180');
            $this.closest('.card').find('.card-body').slideDown();
        }
    });
}

//for blind fired buttons with feedback.
function EnableJQresultTriggerBtn() {
    $(".JQresultTriggerBtn").click(function (e) {
        e.preventDefault();
        var caller = this;
        $.toaster({ title: 'JQTriggerBtn', priority: 'info', message: 'Fired: ' + $(this).attr('href')});
        $.ajax({
            type: "GET",
            url: $(this).attr('href'),
            success: function (result) {
                console.log(result);
                if (result.Msg !== null) {
                    $.toaster({ title: 'JQTriggerBtn', priority: 'success', message: result.Msg });
                }
                else {
                    $.toaster({ title: 'JQTriggerBtn', priority: 'success', message: 'Whatever you did worked!' });
                }
            },
            error: function (result) {
                console.log(result);
                if (result.statusText !== null) {
                    $.toaster({ title: 'JQTriggerBtn', priority: 'danger', message: result.statusText });
                }
                else {
                    $.toaster({ title: 'JQTriggerBtn', priority: 'danger', message: 'Whatever you did failed!' });
                }
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
