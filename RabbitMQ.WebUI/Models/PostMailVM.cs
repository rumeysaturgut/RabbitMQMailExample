using Microsoft.Extensions.Hosting;
using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.WebUI.Models
{
    public class PostMailVM
    {
        public Post Post { get; set; }
    }
}