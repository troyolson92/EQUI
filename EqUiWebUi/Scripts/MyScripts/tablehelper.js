﻿//*****************************
//table set fontsize
//*****************************
function SetTableFont(fontsize) {
    $(function () {
        var myElements = document.querySelectorAll(".table");
        for (var i = 0; i < myElements.length; i++) {
            myElements[i].style.fontSize = fontsize + "px";
        }
    });
}

//*****************************
//table popover modal section
//*****************************
function ListenToModal(modalid) {
    $(function () {
        $(".modal-link").click(function (event) {
            //event.preventDefault();
            //$(modalid).modal({ remote: $(this).attr("href") });
        });

        //slideInDown
        //slideOutUp

        $(modalid).on('hidden.bs.modal', function () {
            bInterlockModal = false;
            console.log("reset modal interlock");
        });


    })
}


//*****************************
//table datarefresh polling
//*****************************
function CheckRefreshTable(polinterval, urlDataCheckaction, gridId) {
    $(function () {
        setInterval(CheckForData, 1000 * polinterval);
    });
    //get last data timestamp from model
    var Datatimestamp = '1900-01-01 00:00:00';
    //interlock for ajax request
    var bInterlock = new Boolean();
    bInterlock = false;

    function CheckForData() {
        //if modal open no check
        if ($('.modal').hasClass('in')) {
            console.log("Modal open detected check for new data: " + urlDataCheckaction + " halted");
            return;
        } else {
            console.log("check for new data: " + urlDataCheckaction);
        }
        // if an other request running no check
        if (bInterlock) {
            console.log("INTERLOCK check for new data: " + urlDataCheckaction + " halted");
            return;
        } else {
            bInterlock = true;
        }
        //Ajax request
        $.ajax({
            async: true,
            type: 'GET',
            url: urlDataCheckaction, //need to pas query string here for filters ! 
            dataType: 'json',
            data: { dataTimestamp: Datatimestamp },

            success: function (response) {
                if (!($("element").data('bs.modal') || {}).isShown ) { // NOK 
                    if (response.doReload) {
                        //reload grid
                        $(gridId).mvcgrid({
                            reload: true,
                        });
                        Datatimestamp = response.dataTimestamp;
                        console.log("Reloaded grid: " + gridId) + console.log(response);
                    }
                }
                else {
                    console.log("Reload SKIPED a modal is open");
                }
                bInterlock = false;
                $("#PushInfo").html("Last check: " + response.lastCheck + " Last push: " + response.dataTimestamp);
            },

            error: function (ex) {
                console.log("Modal open detected check for new data: " + urlDataCheckaction) + console.log(ex);
                bInterlock = false;
            }
        });

    }
}

//*****************************
//set query string of a grid
//*****************************
function SetAjaxGridFilter(GridID, QueryString) {
    $('#' + GridID).mvcgrid({
        query: QueryString,
        reload: true,
    });
}

//*****************************
//show hide navbar
//*****************************
function Navbar(show) {
    if (show == true) {
        $(".navbar-fixed-top").autoHidingNavbar().show();
        $("#allcontent").animate({ "margin-top": "60px" }, "fast");
    }
    else {
        $(".navbar-fixed-top").autoHidingNavbar().hide();
        $("#allcontent").animate({ "margin-top": "0px" }, "fast");
    }
}