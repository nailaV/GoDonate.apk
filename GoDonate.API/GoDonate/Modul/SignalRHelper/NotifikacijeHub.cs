using Microsoft.AspNetCore.SignalR;

namespace GoDonate.Modul.SignalRHelper
{
    public class NotifikacijeHub : Hub
    {
        public async Task PosaljiPoruke (string poruka)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("PosaljiPoruke", poruka);
        }
    }
}
