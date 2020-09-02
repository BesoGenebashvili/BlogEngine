using BlogEngine.Core.Data.DatabaseContexts;
using BlogEngine.Core.Data.Entities;
using BlogEngine.Core.Services.Abstractions;

namespace BlogEngine.Core.Services.Implementations
{
    public class NotificationReceiverRepository : Repository<NotificationReceiver>, INotificationReceiverRepository
    {
        public NotificationReceiverRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}