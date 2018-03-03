//*****************************
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