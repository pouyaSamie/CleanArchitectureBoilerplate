using Application.Common.Interfaces;
using Application.Notification.Models;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }
    }
}
