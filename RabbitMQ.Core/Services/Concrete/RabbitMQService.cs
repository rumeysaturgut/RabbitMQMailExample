using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Services.Concrete
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IRabbitMQConfiguration _configuration;

        public RabbitMQService(IRabbitMQConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IConnection GetConnection()
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration.HostName,
                    UserName = _configuration.UserName,
                    Password = _configuration.Password,
                };

                factory.AutomaticRecoveryEnabled = true; // Otomatik bağlantı kurmayı etkinleştirebilmek için
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(10); // Her 10 snde bir tekrar bağlantı toparlanmaya çalışır
                factory.TopologyRecoveryEnabled = false; // Sunucudan bağlantısı kesildikten sonra kuyruktaki mesaj tüketimini sürdürmez çünkü false set edilmiş

                return factory.CreateConnection();
            }
            catch (BrokerUnreachableException)
            {
                Thread.Sleep(5000); // loglama işlemi yapabiliriz
                return GetConnection(); // farklı businessta yapılabilir, ancak biz tekrar bağlantı kurmayı deneyeceğiz
            }
        }

        public IModel GetModel(IConnection connection)
        {
            return connection.CreateModel();
        }
    }
}
