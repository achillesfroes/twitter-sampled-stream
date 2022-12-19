using Azure.Storage.Queues;
using System.Text;
using Twitter.Sampled.Stream.ConfigModels;

namespace Twitter.Sampled.Infrastructure.Services
{
    public class QueueService : IQueueService
    {
        private readonly QueueClient queueClient;

        public QueueService(QueueStorageSettings queueStorageSettings)
        {

            QueueClient queueClient = new QueueClient(queueStorageSettings.ConnectionString, queueStorageSettings.QueueName, new QueueClientOptions
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            queueClient.CreateIfNotExists();

            this.queueClient = queueClient;
        }

        public bool Exists()
        {
            return queueClient.Exists();
        }

        public async Task SendMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            queueClient.SendMessage(Convert.ToBase64String(bytes));
        }
    }
}
