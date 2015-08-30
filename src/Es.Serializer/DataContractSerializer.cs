using System;
using System.IO;
using System.Xml;

namespace Es.Serializer
{
    public class DataContractSerializer : ObjectSerializerBase
    {
        public override void Serialize(object value, Stream output) {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(value.GetType());
            serializer.WriteObject(output, value);
        }

        public override object Deserialize(Stream stream, Type type) {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(type);
            return serializer.ReadObject(stream);
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