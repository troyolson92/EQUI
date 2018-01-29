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

    }
}