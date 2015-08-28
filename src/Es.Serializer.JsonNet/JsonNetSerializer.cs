using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Es.Serializer
{
    public class JsonNetSerializer : ObjectSerializerBase
    {
#if DEBUG
        private readonly Formatting _format = Formatting.Indented;
#else
        private readonly Formatting _format = Formatting.None;
#endif

        protected override void SerializeCore(object value, TextWriter writer) {
            JsonSerializer serializer = JsonSerializer.Create(
             new JsonSerializerSettings
             {
                 Converters = new JsonConverter[] { new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" } },
                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                 NullValueHandling = NullValueHandling.Ignore,
                 Formatting = _format
             }
          );

            serializer.Serialize(writer, value);
        }

        protected override object DeserializeCore(Type type, TextReader reader) {
            JsonSerializer serializer = JsonSerializer.Create();
            return serializer.Deserialize(reader, type);
        }
    }
}