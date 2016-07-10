using System;
using System.Collections.Generic;
using System.Linq;
using AspNetMvcModelBinderSamples.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AspNetMvcModelBinderSamples.JsonConverters
{
    public class OrderConverter : JsonConverter
    {
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return
                objectType.Equals(typeof(List<Order>))
                || objectType.Equals(typeof(Order));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType.Equals(JsonToken.StartArray))
            {
                return
                    JArray.Load(reader)
                          .Cast<JObject>()
                          .Select(o =>
                          {
                              return GenerateOrderObject(o, serializer);
                          })
                          .ToList();
            }

            if (reader.TokenType.Equals(JsonToken.StartObject))
            {
                return GenerateOrderObject(JObject.Load(reader), serializer);
            }

            return null;
        }

        private Order GenerateOrderObject(JObject jobj, JsonSerializer serializer)
        {
            switch ((OrderType)(int)jobj["Type"])
            {
                case OrderType.Book: return jobj.ToObject<BookOrder>(serializer);
                case OrderType.Car: return jobj.ToObject<CarOrder>(serializer);
                default: return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}