//enable general interface
function initInterface() {
    EnablePannelCollaps();
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

//get part from querystring
function qs(key) {
    key = key.replace(/[*+?^$.\[\]{}()|\\\/]/g, "\\$&"); // escape RegEx meta chars
    var match = location.search.match(new RegExp("[?&]" + key + "=([^&]+)(&|$)"));
    return match && decodeURIComponent(match[1].replace(/\+/g, " "));
}

//for calling a blind controller action from button click
function ActionOnClick(ButtonId,action) {
    $("#" + ButtonId).click(function (e) {
        e.preventDefault();
        $("#" + ButtonId).removeClass("btn-warning");
        $("#" + ButtonId).removeClass("btn-success");
        $.ajax({
            url: action,
            success: function () {
                console.log(ButtonId + " Succes")
                $("#" + ButtonId).addClass("btn-success");
            },
            error: function (ex) {
                console.log(ButtonId + " ERROR") + console.log(ex);
                $("#" + ButtonId).addClass("btn-warning");
            }
        });
    });
}
