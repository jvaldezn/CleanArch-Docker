using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging.Publisher
{
    public interface IEventPublisher
    {
        Task Publish<T>(T message) where T : class;
    }
}
