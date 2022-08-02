using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

using Microsoft.EntityFrameworkCore;
using MeowPlanet.Models;
using Microsoft.AspNetCore.Authorization;

namespace MeowPlanet.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string userName, string userPhoto, string message, string receiveId)
        {
            return Clients.User(receiveId).SendAsync("ReceiveMessage", userName, userPhoto, message);
        }
    }
}
