using Microsoft.AspNetCore.SignalR;

namespace JobsityChat.Model
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string usuario, string mensagem)
        {
            await Clients.All.SendAsync("ReceiveMessage", usuario, mensagem);
        }
    }
}
