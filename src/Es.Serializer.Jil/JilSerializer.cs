using System;
using System.IO;

namespace Es.Serializer
{
    public class JilSerializer : ObjectSerializerBase
    {
        private Jil.Options opt = new Jil.Options(
               prettyPrint: false,
               excludeNulls: false,
               dateFormat: Jil.DateTimeFormat.ISO8601);

        protected override void SerializeCore(object value, TextWriter writer) {
            Jil.JSON.Serialize(value, writer, opt);
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            return Jil.JSON.Deserialize(reader, type, opt);
        }

        public override object DeserializeFromString(string serializedText, Type type) {
            return Jil.JSON.Deserialize(serializedText, type, opt);
        }

        public override string SerializeToString(object value) {
            return Jil.JSON.Serialize(value, opt);
        }
    }
}