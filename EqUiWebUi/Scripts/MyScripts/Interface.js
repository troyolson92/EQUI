//enable general interface
function initInterface() {
    EnablePannelCollaps();
    EnableJQresultTriggerBtn();
    initToaster();
    initUrlNewWindow();
}

//script for subscribing to pannel colaps
function EnablePannelCollaps() {
    $(document).on('click', '.card-header span.clickable', function (e) {
        var $this = $(this);
        if ($this.parents('.card').find('.card-body').css('display') == 'block') {
            $this.addClass('fa-rotate-180')
            $this.parents('.card').find('.card-body').slideUp()
        } else {
            $this.removeClass('fa-rotate-180')
            $this.parents('.card').find('.card-body').slideDown()
        }
    })
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