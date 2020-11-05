using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ProgramowanieRozproszone_Notification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramowanieRozproszone_Notification.Services
{
    public class ServiceBusConsumer
    {
        private readonly QueueClient _queueClient;
        private const string QueueName = "messages";
        public ServiceBusConsumer(IConfiguration configuration)
        {
            _queueClient = new QueueClient(
            configuration.GetConnectionString("ServiceBusConnectionString"),
            QueueName);
        }
        public void Register()
        {
            var options = new MessageHandlerOptions(e => Task.CompletedTask)
            {
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessage, options);
        }
        private async Task ProcessMessage(Message message, CancellationToken token)
        {
            var payload = JsonConvert.DeserializeObject<EmailMessage>(Encoding.UTF8.GetString(message.Body));

            if(payload.EventName == "NewUserRegistered")
            {
                EmailSender sender = new EmailSender();
                sender.SendNewUserEmail(payload);
            }

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }
    }
}
