using System;
using Newtonsoft.Json;

namespace TeamCores.Common.Json
{
	/// <summary>
	/// 字符串在JSON中的处理
	/// </summary>
	public class StringConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var type = Type.GetTypeFromHandle(objectType.TypeHandle);

            return type == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (string)reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                string val = value.ToString();
                
                val = val.Replace("\t", "    ");
                val = val.Replace(@"\", @"\\");

                writer.WriteValue(val);
            }
        }
    }
}
