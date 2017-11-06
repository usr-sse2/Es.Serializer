using System;
using System.IO;

namespace Es.Serializer
{
    public class JilSerializer : ObjectSerializerBase
    {
        private Jil.Options _options;

        /// <summary>
        /// JilSerializer Instance
        /// </summary>
        public static JilSerializer Instance = new JilSerializer();

        public JilSerializer(Jil.Options options) {
            _options = options;
        }

        public JilSerializer() : this(new Jil.Options(
               prettyPrint: false,
               excludeNulls: false,
               dateFormat: Jil.DateTimeFormat.ISO8601)) {
        }

        protected override void SerializeCore(object value, TextWriter writer) {
            Jil.JSON.Serialize(value, writer, _options);
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            return Jil.JSON.Deserialize(reader, type, _options);
        }

        public override object DeserializeFromString(string serializedText, Type type) {
            return Jil.JSON.Deserialize(serializedText, type, _options);
        }

        public override string SerializeToString(object value) {
            return Jil.JSON.Serialize(value, _options);
        }
    }
}