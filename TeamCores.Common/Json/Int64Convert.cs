using Newtonsoft.Json;
using System;

namespace TeamCores.Common.Json
{
    /// <summary>
    /// 长整型在JSON中的处理
    /// </summary>
    public class Int64Convert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var type = Type.GetTypeFromHandle(objectType.TypeHandle);
            return type == typeof(Int64) || type == typeof(Nullable<Int64>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool isNullable = IsNullableType(objectType);

            Type t = isNullable ? Nullable.GetUnderlyingType(objectType) : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!IsNullableType(objectType))
                {
                    throw new Exception(string.Format("不能转换null value to {0}.", objectType));
                }

                return null;
            }

            try
            {
                long temp = 0;

                Int64.TryParse(reader.Value.ToString(), out temp);

                return temp;
            }
            catch
            {
                throw new Exception(string.Format("Error converting value {0} to type '{1}'", reader.Value, objectType));
            }
            throw new Exception(string.Format("Unexpected token {0} when parsing enum", reader.TokenType));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else if (value is Int64 || value is Nullable<Int64>)
            {
                writer.WriteValue(value.ToString());
            }
        }

        public bool IsNullableType(Type t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("t");
            }
            return (t.FullName == "System.ValueType" && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}
