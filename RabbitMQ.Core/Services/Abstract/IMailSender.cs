using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Services.Abstract
{
    public interface IMailSender
    {
        Task<MailSendResult> SendMailAsync(MailMessageData emailMessage);
    }
}
