using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Consts
{
    public class RabbitMQConsts
    {
        public static int MessagesTTL { get; set; } = 1000 * 60 * 60 * 2;  //yaşam süresi

        public static ushort ParallelThreadsCount { get; set; } = 3;

        public enum RabbitMqConstsList
        {
            [Description("QueueNameEmail")]
            QueueNameEmail = 1,
            [Description("QueueNameSms")]
            QueueNameSms = 2
        }
    }
}
