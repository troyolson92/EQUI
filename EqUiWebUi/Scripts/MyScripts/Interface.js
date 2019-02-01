//enable general interface
function initInterface() {
    //navbar
    $("#allcontent").animate({ "margin-top": "60px" }, "fast");
    $("#navbar").autoHidingNavbar();

    //for fullscreen mode
    $("#fullscreenNav").unbind().click(function () {
        $("#allcontent").removeClass("body-content");
        $("#allcontent").removeClass("container");

        $("#navbar").autoHidingNavbar('setDisableAutohide', true); //disable autohide
        $("#navbar").autoHidingNavbar('hide'); //hide navbar

        $("#allcontent").animate({ "margin-top": "0px" }, "fast");
        $("#fullscreenBody").removeClass("d-none");
        $("#footer").addClass("d-none");
    });
    //out of full screen mode
    $("#fullscreenBody").unbind().click(function () {

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
    };

    //if zoomlevel request in url do it.
    if (qs("Zoomlevel")) {
        console.log("Set zoom level");
        //this works for Chrome, Safari, IE ref:http://jsfiddle.net/q6kebgbh/4/
        $("#allcontent").css('zoom', qs("Zoomlevel"));
    }

    //initpopovers
    //on click ignore default action
    $(".MyPopovers").unbind().click(function (e) {
        console.log('default action prevented (.Mypopovers)');
        e.preventDefault();
        return false;
    });

    //get content using ajax call
    $(".MyPopovers").popover({
        html: true,
        content: function () {
            return $.ajax({
                url: $(this).attr('href'),
                dataType: 'html',
                async: false
            }).responseText;
        }
    }).on('shown.bs.popover', function () {
        EnableInterfaceEvents();
        $('[data-dismiss="popover"]').on('click', function () {
            $('.popover').popover('hide');
        });

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

    //script for subscribing to panel colaps
    $(document).on('click', '.card-header span.clickable', function (e) {
        if ($(this).closest('.card').find('.card-body').css('display') === 'block') {
            $(this).addClass('fa-rotate-180');
            $(this).closest('.card').find('.card-body').slideUp();
        } else {
            $(this).removeClass('fa-rotate-180');
            $(this).closest('.card').find('.card-body').slideDown();
        }
    });

    //table event for grid that gets loaded without ajax
    $('.mvc-grid').mvcgrid();
    //enable events like tooltips ...
    EnableInterfaceEvents();
}

//script that must be called to enable interface events
function EnableInterfaceEvents() {
    //for new window option
    $('.OpenNewWindow').unbind().click(function (e) {
        console.log('default action prevented (.OpenNewWindow)');
        e.preventDefault();
        console.log("Opening new window");
        window.open($(this).attr('href'), 'EQUI', 'window settings');
        return false;
    });

    //for blind fired buttons with feedback. the unbind cancels any active event on this object
    $(".JQresultTriggerBtn").unbind().click(function (e) {
        console.log('default action prevented (.JQresultTriggerBtn)');
        e.preventDefault();
        $.toaster({ title: 'JQTriggerBtn', priority: 'info', message: 'Fired: ' + $(this).attr('href') });
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

//above here we used UNBIND! on both so be aware that normal stuff that creates events should go below them.

    //for toaster
    $.toaster({
        settings: {
            'timeout': 5000, //set autodismis timeout to 5 seconds
            'donotdismiss': ['danger'] //disble autodismis for these types
        }
    });

    //enable tooltips everywhere
    $('[data-toggle="tooltip"]').tooltip();
    $('[data-toggle="popover"]').tooltip();

    //select drop downs that are form control and make this use bootstrap select. (don't just do on all select this will break things like mvc gird pager)
    $('select.form-control').selectpicker();


    //tablesaw
    //enable select box mode switch and minimap
    $('.tablesaw-on').attr("data-tablesaw-mode-switch", "");
    $('.tablesaw-on').attr("data-tablesaw-mode-exclude", "stack");
    $('.tablesaw-on').attr("data-tablesaw-mode", "columntoggle");
    $('.tablesaw-on').attr("data-tablesaw-minimap", "");
    //this must be on all table thead 
    $('.tablesaw-on > thead > tr > th').attr("scope", "col");
    //set all by default to prio 1 (must be done to make tool populate)
    $('.tablesaw-on > thead > tr > th').attr("data-tablesaw-priority", "1"); //value from 1 to 6 depending on viewport
    //normally attributes are added to the table headers to set prio to the table.
    //because gid-mvc does not support attr in constuctor we add the CSS class in the gird-mvc constructor.
    //then we check here if it has that class and add the attr. 
    $('.tablesaw-on > thead > tr > .tablesaw-priority-persist').attr("data-tablesaw-priority", "persist");
    $('.tablesaw-on > thead > tr > .tablesaw-priority-0').attr("data-tablesaw-priority", "0");
    $('.tablesaw-on > thead > tr > .tablesaw-priority-1').attr("data-tablesaw-priority", "1");
    $('.tablesaw-on > thead > tr > .tablesaw-priority-2').attr("data-tablesaw-priority", "2");
    $('.tablesaw-on > thead > tr > .tablesaw-priority-3').attr("data-tablesaw-priority", "3");
    $('.tablesaw-on > thead > tr > .tablesaw-priority-4').attr("data-tablesaw-priority", "4");
    $('.tablesaw-on > thead > tr > .tablesaw-priority-5').attr("data-tablesaw-priority", "5");
    $('.tablesaw-on > thead > tr > .tablesaw-priority-6').attr("data-tablesaw-priority", "6");
    Tablesaw.init();
}

//get part from querystring
function qs(key) {
    key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
    var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
    return match && decodeURIComponent(match[1].replace(/\+/g, " "));
}
