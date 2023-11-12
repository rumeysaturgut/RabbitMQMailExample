using Newtonsoft.Json;
using RabbitMQ.Core.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RabbitMQ.Core.Services.Concrete
{
    public class ObjectConvertFormat : IObjectConvertFormat
    {
        // RabbitMQ queue da veriyi byte[] tipinde saklamaktadır. Kuyrukta (Queue) bu veriyi gönderebilmemiz için tip dönüşümüne gerek duyarız.

        public T JsonToObject<T>(string jsonString) where T : class, new()
        {
            var objectData = JsonConvert.DeserializeObject<T>(jsonString);
            return objectData;
        }

        public string ObjectToJson<T>(T entityObject) where T : class, new()
        {
            var jsonString = JsonConvert.SerializeObject(entityObject);
            return jsonString;
        }

        public T ParseObjectDataArray<T>(byte[] rawBytes) where T : class, new()
        {
            var stringData = Encoding.UTF8.GetString(rawBytes);
            return JsonToObject<T>(stringData);
        }
    }
}
