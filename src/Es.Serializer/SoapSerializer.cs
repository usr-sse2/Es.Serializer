using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Es.Serializer
{
    public class SoapSerializer : ObjectSerializerBase
    {
        private readonly IFormatter binaryFormatter = new SoapFormatter();

        public override void Serialize(object value, Stream output) {
            binaryFormatter.Serialize(output, value);
        }

        public override object Deserialize(Stream stream, Type type) {
            return binaryFormatter.Deserialize(stream);
        }

        public override string SerializeToString(object value) {
            using (MemoryStream mem = new MemoryStream()) {
                Serialize(value, mem);
                return Encoding.GetString(mem.ToArray());
            }
        }

        public override object DeserializeFromString(string serializedText, Type type) {
            var data = Encoding.GetBytes(serializedText);
            using (MemoryStream mem = new MemoryStream(data)) {
                return Deserialize(mem, type);
            }
        }

        protected override void SerializeCore(object value, TextWriter writer) {
            writer.Write(SerializeToString(value));
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            var serializedText = reader.ReadToEnd();
            return DeserializeFromString(serializedText, type);
        }
    }
}