using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.Maximo_ui.Interface
{
    //blogpost
    //http://maximodev.blogspot.se/2012/04/maximo-urls.html

    //open WO in werkorder overzicht using wo num (lijst)
    //http://maximoui.volvocars.biz/maximo/ui/maximo.jsp?event=loadapp&value=wotrack&additionalevent=sqlwhere&additionaleventvalue=WONUM%3D'10416055'
    //open WO in werkorder overzicht using wo num QBE (werkorderoverzicht)
    //http://maximoui.volvocars.biz/maximo/ui/maximo.jsp?event=loadapp&value=wotrack&additionalevent=useqbe&additionaleventvalue=wonum=10416055
    //open WO in werkorder overzicht using wo num QBE (werkorderoverzicht) + andere tab 
    //http://maximoui.volvocars.biz/maximo/ui/maximo.jsp?event=loadapp&value=wotrack&additionalevent=useqbe&additionaleventvalue=wonum=10416055&changetab=relatedrec

    //make workorder
    //http://maximoui.volvocars.biz/maximo/ui/maximo.jsp?event=loadapp&value=wotrack&additionalevent=insert&additionaleventvalue=location=99020R01|worktype=CI|description=Test|OWNERGROUP=AACFZ|CXCBDDPROD=N

    public class MaximoInterface
    {
        public string basepath { get { return "http://maximoui.volvocars.biz/maximo/ui/maximo.jsp"; } }
    }

    //start a certain maximo application 
    public class apps
    {
        public string startcntr { get { return "event=loadapp&value=startcntr"; } }
        public string role { get { return "event=loadapp&value=role"; } }
        public string person { get { return "event=loadapp&value=person"; } }
        public string asset { get { return "event=loadapp&value=asset"; } }
        public string wotrack { get { return "event=loadapp&value=wotrack"; } }
    }
}