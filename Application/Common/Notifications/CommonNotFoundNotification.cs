using MediatR;
using Application.Common.Interfaces;
using Application.Notification.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Notifications
{
    public class CommonNotFoundNotification : INotification
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string PersianName { get; set; }

        public class CommonNotFoundNotificationHandler : INotificationHandler<CommonNotFoundNotification>
        {
            private readonly INotificationService _notification;
            public CommonNotFoundNotificationHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(CommonNotFoundNotification notification, CancellationToken cancellationToken)
            {
                MessageDto message = new MessageDto()
                {
                    Subject = "هیچ موردی یافت نشد",
                    Body = $"هیچ {notification.PersianName} با کلید {notification.Key} یافت نشد"
                };

                await _notification.SendAsync(message);
            }
        }
    }
}
