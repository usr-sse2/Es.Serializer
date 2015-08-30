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

        protected override void SerializeCore(object value, TextWriter writer) {
            throw new NotImplementedException();
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            throw new NotImplementedException();
        }
    }
}