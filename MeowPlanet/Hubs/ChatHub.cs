using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

using Microsoft.EntityFrameworkCore;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Authorization;

namespace MeowPlanet.Hubs
{
    public class ChatHub : Hub
    {
        private readonly endtermContext _context;

        public ChatHub(endtermContext context)
        {
            _context = context;
        }
        public Task SendMessage(string selfID, string message, string userId)
        {

            return Clients.User(userId).SendAsync("ReceiveMessage", message);
        }
    }
}
