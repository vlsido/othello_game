using Microsoft.AspNetCore.SignalR;

namespace Othello_Web.SignalR
{
    public class GameHub : Hub
    {
        public async Task RefreshTask()
        {
            await Clients.All.SendAsync("RefreshPage");
        }
    }
}
