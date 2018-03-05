//enable general interface
function initInterface() {
    EnablePannelCollaps();
    EnableJQresultTriggerBtn();
    initToaster();
}

//script for subscribing to pannel colaps
function EnablePannelCollaps() {
    $(document).on('click', '.panel-heading span.clickable', function (e) {
        var $this = $(this);
        if (!$this.hasClass('panel-collapsed')) {
            $this.parents('.panel').find('.panel-body').slideUp();
            $this.addClass('panel-collapsed');
            $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
        } else {
            $this.parents('.panel').find('.panel-body').slideDown();
            $this.removeClass('panel-collapsed');
            $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
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