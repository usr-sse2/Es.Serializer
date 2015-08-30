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

        protected override void SerializeCore(object value, TextWriter writer) {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(value.GetType());
            serializer.WriteObject(XmlWriter.Create(writer), value);
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(type);
            return serializer.ReadObject(XmlReader.Create(reader));
        }
    }
}