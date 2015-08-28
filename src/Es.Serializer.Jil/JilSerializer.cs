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

        protected override object DeserializeCore(Type type, TextReader reader) {
            return Jil.JSON.Deserialize(reader, type, opt);
        }

        public override object DeserializeFromString(Type type, string serializedText) {
            return Jil.JSON.Deserialize(serializedText, type, opt);
        }

        public override string SerializeToString<TFrom>(TFrom from) {
            return Jil.JSON.Serialize(from, opt);
        }
    }
}