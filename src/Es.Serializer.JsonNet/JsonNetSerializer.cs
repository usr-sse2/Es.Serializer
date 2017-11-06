using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Es.Serializer
{
    public class JsonNetSerializer : ObjectSerializerBase
    {
#if DEBUG
        private static readonly Formatting _format = Formatting.Indented;
#else
        private static readonly Formatting _format = Formatting.None;
#endif

        /// <summary>
        /// JsonNetSerializer Instance
        /// </summary>
        public static JsonNetSerializer Instance = new JsonNetSerializer();


        private readonly JsonSerializerSettings _setting;

        public JsonNetSerializer(JsonSerializerSettings setting) {
            _setting = setting;
        }

        public JsonNetSerializer() : this(new JsonSerializerSettings
        {
            Converters = new JsonConverter[] { new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" } },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = _format
        }) {
        }

        protected override void SerializeCore(object value, TextWriter writer) {
            JsonSerializer serializer = JsonSerializer.Create(_setting);
            serializer.Serialize(writer, value);
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            JsonSerializer serializer = JsonSerializer.Create();
            return serializer.Deserialize(reader, type);
        }
    }
}