using System;
using System.IO;
using ProtoBuf.Meta;

namespace Es.Serializer
{
    public class ProtoBufSerializer : ObjectSerializerBase
    {
        public override void Serialize(object value, Stream output) {
            RuntimeTypeModel.Default.Serialize(output, value);
        }

        public override object Deserialize(Stream stream, Type type) {
            return RuntimeTypeModel.Default.Deserialize(stream, null, type);
        }

        protected override void SerializeCore(object value, TextWriter writer) {
            throw new NotImplementedException();
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            throw new NotImplementedException();
        }
    }
}