using Newtonsoft.Json;

namespace TeamCores.Common.Json
{
    /// <summary>
    /// Json序列化全局配置
    /// </summary>
    public class Settings
    {
        public static readonly JsonSerializerSettings SerializerSettings;

        static Settings()
        {
            if (SerializerSettings == null)
                SerializerSettings = new JsonSerializerSettings();

            JsonConvert.DefaultSettings = () =>
            {
                //日期类型默认格式化处理
                SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

                //空值处理
                //SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

                SerializerSettings.Converters.Add(new Int64Convert());

                SerializerSettings.Converters.Add(new StringConvert());

                return SerializerSettings;
            };
        }
    }
}