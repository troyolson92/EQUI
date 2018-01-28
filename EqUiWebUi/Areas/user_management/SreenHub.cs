using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace EqUiWebUi.Areas.user_management
{
    public class ScreenHub : Hub
    {
        public void Announce(string message)
        {
            Clients.All.Announce(message);
        }

        public Task JoinGroup(string groupname)
        {
            return Groups.Add(Context.ConnectionId, groupname);
        }

        public Task LeaveGroup(string groupname)
        {
            return Groups.Remove(Context.ConnectionId, groupname);
        }

        //complete reload of page
        //possible to refresh a ALL CLIENTS  / Specific screenID / Specific Screennum
        public void FullRefresh(int? screenId, int? screenNum)
        {
            //full refresh all connected clients.
            if (!screenId.HasValue && !screenNum.HasValue)
            {
                Clients.All.FullRefresh();
                return;
            }

            //full refresh specific screenid
            if (screenId.HasValue)
            {
                //get all clients in group
                Clients.Group("groupname").FullRefresh();
            }

            //full refresh specific screeNum
            if (screenNum.HasValue)
            {
                //get all clients in group
                Clients.Group("groupname").FullRefresh();
            }
        }

        // reload of iframe
        //possible to refresh a ALL CLIENTS  / Specific screenID / Specific Screennum
        public void Refresh(int? screenId, int? screenNum)
        {
            //full refresh all connected clients.
            if (!screenId.HasValue && !screenNum.HasValue)
            {
                Clients.All.FullRefresh();
                return;
            }

            //full refresh specific screenid
            if (screenId.HasValue)
            {
                //get all clients in group
                Clients.Group("groupname").FullRefresh();
            }

            //full refresh specific screeNum
            if (screenNum.HasValue)
            {
                //get all clients in group
                Clients.Group("groupname").FullRefresh();
            }
        }

        //Send text message
        //possible to refresh a ALL CLIENTS  / Specific screenID / Specific Screennum
        public void DisplayMessage(int? screenId, int? screenNum, int? showtime, string message)
        {
            //if showtime is null show until user closes it
        }

        //Send text message
        //possible to refresh a ALL CLIENTS  / Specific screenID / Specific Screennum
        public void DisplayPage(int? screenId, int? screenNum, int? showtime, string url)
        {
            //if showtime is null show until user closes it
        }


    }
}