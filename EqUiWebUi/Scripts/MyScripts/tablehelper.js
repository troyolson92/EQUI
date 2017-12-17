//*****************************
//table row formatting section
//*****************************


//script for setting row colors based on there values
function tableFormatLogtype(colnumLogType) {
    $('tbody > tr').each(function (index) {
        if ($(this).children('td:nth-child(' + colnumLogType + ')').text() == "SHIFTBOOK") { $(this).children('td').css("background-color", "#d6d613"); }

        else if ($(this).children('td:nth-child(' + colnumLogType + ')').text() == "WARNING") { $(this).children('td').css("background-color", "#127bf1"); }

        else if ($(this).children('td:nth-child(' + colnumLogType + ')').text() == "LIVE") { $(this).children('td').css("background-color", "#f21a1a"); }

        else if ($(this).children('td:nth-child(' + colnumLogType + ')').text() == "BREAKDOWN") { $(this).children('td').css("background-color", "#f2af13"); }

        else if ($(this).children('td:nth-child(' + colnumLogType + ')').text() == "TIMELINE") { $(this).children('td').css("background-color", "#16dd76"); }

        else { }
    });
}

//script for blinking rows
function tableFormatBlink(logtypecolnum, subgroupcolnum) {
    //flash row 
    $(function () {
        setInterval(flashRow, 750);
    });
    var bFlip = new Boolean(false);
    function flashRow() {
        bFlip = !bFlip;
        $('tbody > tr').each(function (index) {
            //blink for slowspeed
                    if ($(this).children('td:nth-child(' + logtypecolnum + ')').text() == 'SLOWSpeed') {
                if (bFlip) {
                    $(this).children('td').css("background-color", "#fc2fd0");
                } else {
                    $(this).children('td').css("background-color", "#ffffff");
                }
            }
            //link live hardware fault
                    if (($(this).children('td:nth-child(' + logtypecolnum + ')').text() == 'LIVE')
                && ($(this).children('td:nth-child(' + subgroupcolnum + ')').text() == 'Hardware')) {
                if (bFlip) {
                    $(this).children('td').css("background-color", "#f21a1a");
                } else {
                    $(this).children('td').css("background-color", "#ffffff");
                }
            }
        });
    }
}


//*****************************
//table popover modal section
//*****************************
function ListenToModal(modalid) {
    $(function () {
        $(".modal-link").click(function (event) {
            event.preventDefault();
            bModalOpen = true;
            $(modalid).modal({ remote: $(this).attr("href") });
        });

        $(modalid).on('hidden.bs.modal', function () {
            // reload page
            window.location.reload();
        })
    })
}



//*****************************
//table datarefresh polling
//*****************************
//'@Html.Raw(ViewBag.DataTimestamp)'
//@Url.Action("PloegRapportcheckNewData")
function PolFordata(polinterval, datatimestamp, urlaction) {
    $(function () {
        //check every 5 seconds
        setInterval(CheckForData, 1000 * polinterval);
    });
    //get last data timestamp from model
    var Datatimestamp = datatimestamp;
    console.log("Datatimestamp: " + Datatimestamp);
    //interlock for ajax request
    var bInterlock = new Boolean();
    bInterlock = false;

    function CheckForData() {
        console.log("check for data");
        if (bInterlock) {
            return;
        } else {
            bInterlock = true;
        }
        $.ajax({
            async: true,
            type: 'GET',
            url: urlaction,
            dataType: 'json',
            data: { dataTimestamp: Datatimestamp },

            success: function (response) {
                bInterlock = false;
                //succes = reload needed
                if (!bModalOpen) { //only reaload when modal is not open
                    window.location.reload();
                }
            },

            error: function (ex) {
                bInterlock = false;
                //error = no reload
            }
        });

    }
}