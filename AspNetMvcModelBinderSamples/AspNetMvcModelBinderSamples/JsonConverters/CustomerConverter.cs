using System;
using System.Collections.Generic;
using System.Linq;
using AspNetMvcModelBinderSamples.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AspNetMvcModelBinderSamples.JsonConverters
{
    public class CustomerConverter : JsonConverter
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
                objectType.Equals(typeof(List<Customer>))
                || objectType.Equals(typeof(Customer));
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
                              return GenerateCustomerObject(o, serializer);
                          })
                          .ToList();
            }

            if (reader.TokenType.Equals(JsonToken.StartObject))
            {
                return GenerateCustomerObject(JObject.Load(reader), serializer);
            }

            return null;
        }

        private Customer GenerateCustomerObject(JObject jobj, JsonSerializer serializer)
        {
            switch ((CustomerType)(int)jobj["Type"])
            {
                case CustomerType.Taiwan: return jobj.ToObject<TaiwanCustomer>(serializer);
                case CustomerType.Japan:
                default: return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}