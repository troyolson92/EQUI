//enable general interface
function initInterface() {
    EnablePannelCollaps();
    EnableJQresultTriggerBtn();
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
        $(caller).removeClass('PulseBlueRepeat');
        $(caller).removeClass('PulseGreenOnce');
        $(caller).removeClass('PulseRedRepeat');
        $(caller).addClass('PulseBlueOnce');
        $.ajax({
            type: "GET",
            url: this.href,
            success: function (result) {
                console.log("SUCCES");
                //console.log(result);
                $(caller).removeClass('PulseBlueRepeat');
                $(caller).addClass('PulseGreenOnce');
            },
            error: function (result) {
                console.log("FAIL");
                //console.log(result);
                $(caller).removeClass('PulseBlueRepeat');
                $(caller).addClass('PulseRedRepeat');
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