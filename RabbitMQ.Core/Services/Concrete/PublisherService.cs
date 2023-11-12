using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Core.Consts;
using RabbitMQ.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Services.Concrete
{
    public class PublisherService : IPublisherService
    {
        private readonly IRabbitMQService _rabbitmqService;

        public PublisherService(IRabbitMQService rabbitmqService)
        {
            _rabbitmqService = rabbitmqService;
        }

        public void Enqueue<T>(IEnumerable<T> queueDataModels, string queueName) where T : class, new()
        {
            try
            {
                using (var connection = _rabbitmqService.GetConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                        durable: true,  // in-memory mi yoksa fiziksel olarak mı saklanacağı belirlenir.
                                        exclusive: false,  // Yalnızca bir bağlantı tarafından kullanılır ve bu bağlantı kapandığında sıra silinir — özel olarak işaretlenirse silinmez 
                                        autoDelete: false,  // En son bir abonelik iptal edildiğinde en az bir müşteriye sahip olan kuyruk silinir
                                        arguments: null);   //İsteğe bağlı; eklentiler tarafından kullanılır ve TTL mesajı, kuyruk uzunluğu sınırı, vb. özellikler tanımlanır.
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.Expiration = RabbitMQConsts.MessagesTTL.ToString();

                    foreach (var queueDataModel in queueDataModels)
                    {
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(queueDataModel));
                        channel.BasicPublish(exchange: "",
                                            routingKey: queueName,
                                            basicProperties: properties,
                                            body: body);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message.ToString());
            }
        }
    }
}
